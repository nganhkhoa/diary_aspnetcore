using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using diary.Data;
using diary.Models;

namespace diary.Controllers
{
      public class ScheduleController : Controller
      {
            diaryContext _context;
            SignInManager<User> _signinmanager;

            public ScheduleController(
                  diaryContext context,
                  SignInManager<User> signinmanager)
            {
                  _context = context;
                  _signinmanager = signinmanager;
            }

            public IActionResult Index(int day, int month, int year)
            {
                  if (!_signinmanager.IsSignedIn(User))
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                  return View();
            }
      }
}