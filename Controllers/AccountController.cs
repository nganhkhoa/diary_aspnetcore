using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text.RegularExpressions;

using diary.Models;
using diary.Helper;
using diary.Models.AccountViewModels;

namespace diary.Controllers
{
      public class AccountController : Controller
      {
            private UserManager<User> _usermanager;
            private readonly SignInManager<User> _signinmanager;

            public AccountController(
                  UserManager<User> usermanager,
                  SignInManager<User> signinmanager)
            {
                  _signinmanager = signinmanager;
                  _usermanager = usermanager;
                  _usermanager.PasswordHasher = new PasswordHashing();
            }

            private IActionResult RedirectToLocal(string returnUrl)
            {
                  if (Url.IsLocalUrl(returnUrl))
                  {
                        return Redirect(returnUrl);
                  }
                  else
                  {
                        DateTime today = DateTime.Now;
                        return RedirectToAction(nameof(ScheduleController.Index), "Schedule",
                              new
                              {
                                    type = "diary",
                                    day = today.Day,
                                    month = today.Month,
                                    year = today.Year
                              });
                  }
            }

            private void AddErrors(IdentityResult result)
            {
                  foreach (var error in result.Errors)
                  {
                        ModelState.AddModelError(string.Empty, error.Description);
                  }
            }

            [HttpGet]
            [AllowAnonymous]
            public async Task<IActionResult> Index(string returnUrl = null)
            {
                  // Clear the existing external cookie to ensure a clean login process
                  await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                  ViewData["ReturnUrl"] = returnUrl;
                  return View();
            }

            [HttpPost]
            [AllowAnonymous]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Index(LoginRegisterModel model, string returnUrl = null)
            {
                  ViewData["ReturnUrl"] = returnUrl;
                  LoginViewModel loginmodel = model.login;
                  RegisterViewModel registermodel = model.register;

                  if (ModelState.IsValid)
                  {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true

                        if (loginmodel != null && registermodel != null)
                        {
                              ModelState.AddModelError(string.Empty, "Both login and signup");
                              return View(model);
                        }
                        if (loginmodel == null && registermodel == null)
                        {
                              ModelState.AddModelError(string.Empty, "Empty submit");
                              return View(model);
                        }

                        if (loginmodel != null)
                        {
                              var result =
                                   await _signinmanager.PasswordSignInAsync(
                                         loginmodel.UserName,
                                         loginmodel.Password,
                                         loginmodel.RememberMe,
                                         lockoutOnFailure: false);

                              if (result.Succeeded)
                              {
                                    return RedirectToLocal(returnUrl);
                              }
                              else
                              {
                                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                                    return View(model);
                              }
                        }
                        if (registermodel != null)
                        {
                              var user = new User
                              {
                                    UserName = registermodel.UserName,
                                    Email = registermodel.Email,
                              };
                              var result = await _usermanager.CreateAsync(user, registermodel.Password);
                              if (!result.Succeeded)
                              {
                                    AddErrors(result);
                              }

                              return View(model);
                        }
                  }

                  // If we got this far, something failed, redisplay form
                  return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout()
            {
                  await _signinmanager.SignOutAsync();
                  return RedirectToAction(nameof(HomeController.Index), "Home");
            }
      }
}