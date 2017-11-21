using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using diary.Data;
using diary.Models;
using diary.Models.ScheduleViewModels;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace diary.Controllers
{
      public class ScheduleController : Controller
      {
            diaryContext _context;
            SignInManager<User> _signinmanager;
            UserManager<User> _usermanager;
            DateTime Exdatetime;


            public ScheduleController(
                  diaryContext context,
                  SignInManager<User> signinmanager,
                  UserManager<User> usermanager)
            {
                  _context = context;
                  _signinmanager = signinmanager;
                  _usermanager = usermanager;
            }

            public IActionResult Index(int day, int month, int year)
            {
                  if (!_signinmanager.IsSignedIn(User))
                  {
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                  }

                  string strDatetime = day + "/" + month + "/" + year;
                  Exdatetime = DateTime.ParseExact(strDatetime, "dd/MM/yyyy", null);

                  ModelCollection model = new ModelCollection();

                  // EntryContext entryContext =
                  // HttpContext.RequestServices
                  // .GetService(typeof(diary.Models.EntryContext)) as EntryContext;

                  // model.entryList = entryContext.GetAllEntry();
                  model.UserName = _usermanager.GetUserName(User);

                  var entries = _context.Entries
                        .Where(u => u.User.UserName == _usermanager.GetUserName(User))
                        .Where(d => d.Date.Date == Exdatetime.Date)
                        .ToList();

                  model.entryList = entries;
                  return View(model);

            }


            public async Task<IActionResult> EntryProcess(ModelCollection model)
            {
                  UploadDatetimeModel modeltemp = model.udModel;

                  if (modeltemp.strUpload == "" || modeltemp.strUpload == null)
                  {
                        return RedirectToAction("Index");
                  }

                  var entry = new Entry();
                  var user = await _usermanager.GetUserAsync(User);

                  entry.Content = modeltemp.strUpload;
                  entry.Date = Exdatetime;
                  entry.User = user;

                  // user.Entries.Add(entry);

                  // _context.Entries.Add(entry);
                  // _context.SaveChanges();


                  return RedirectToAction(nameof(ScheduleController.Index), "Schedule",
                        new { day = Exdatetime.Day, month = Exdatetime.Month, year = Exdatetime.Year });
            }
      }
}