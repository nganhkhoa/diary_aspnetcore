using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using diary.Models;

namespace diary.Controllers
{
      public class HomeController : Controller
      {
            SignInManager<User> _signinmanager;

            public HomeController(SignInManager<User> signinmanager)
            {
                  _signinmanager = signinmanager;
            }

            public IActionResult Index()
            {
                  DateTime today = DateTime.Now;

                  if (_signinmanager.IsSignedIn(User))
                        return RedirectToAction(nameof(ScheduleController.Index), "Schedule",
                              new
                              {
                                    type = "diary",
                                    day = today.Day,
                                    month = today.Month,
                                    year = today.Year
                              });

                  return View();
            }

            public IActionResult About()
            {
                  ViewData["Message"] = "Your application description page.";

                  return View();
            }

            public IActionResult Contact()
            {
                  ViewData["Message"] = "Your contact page.";

                  return View();
            }

            public IActionResult Error()
            {
                  return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
      }
}
