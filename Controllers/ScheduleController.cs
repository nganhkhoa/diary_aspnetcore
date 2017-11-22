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
            static DateTime Exdatetime;


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
                  ScheduleController.Exdatetime =
                        DateTime.ParseExact(strDatetime, "dd/MM/yyyy", null);

                  ModelCollection model = new ModelCollection();

                  // EntryContext entryContext =
                  // HttpContext.RequestServices
                  // .GetService(typeof(diary.Models.EntryContext)) as EntryContext;

                  // model.entryList = entryContext.GetAllEntry();
                  model.UserName = _usermanager.GetUserName(User);

                  var entries = _context.Entries
                        .Where(u => u.User.UserName == model.UserName)
                        .Where(d => d.Date.Date == Exdatetime.Date)
                        .ToList();

                  var events = _context.Events
                        .Where(u => u.User.UserName == model.UserName)
                        .ToList();

                  model.entryList = entries;
                  model.eventList = events;
                  model.Date = Exdatetime;

                  return View(model);
            }


            public async Task<IActionResult> EntryProcess(ModelCollection model)
            {
                  UploadDatetimeModel modeltemp = model.udModel;

                  if (modeltemp.strUpload == "" || modeltemp.strUpload == null)
                  {
                        return RedirectToAction("Index");
                  }

                  DateTime today = DateTime.Now;
                  var user = await _usermanager.GetUserAsync(User);
                  var entry = new Entry()
                  {
                        ID = _context.Entries.ToList().Count() + 1,
                        Content = modeltemp.strUpload,
                        Date = today,
                        User = user
                  };

                  _context.Entries.Add(entry);
                  _context.SaveChanges();

                  return RedirectToAction(nameof(ScheduleController.Index), "Schedule",
                        new
                        {
                              day = today.Day,
                              month = today.Month,
                              year = today.Year
                        });
            }


            public async Task<IActionResult> EventProcess(ModelCollection model)
            {
                  NewEventModel modeltemp = model.evModel;

                  if (modeltemp.Info == "" || modeltemp.Info == null)
                  {
                        return RedirectToAction("Index");
                  }

                  var user = await _usermanager.GetUserAsync(User);
                  var e = new Event()
                  {
                        ID = _context.Events.ToList().Count() + 1,
                        Info = modeltemp.Info,
                        StartDate = Exdatetime,
                        EndDate = Exdatetime,
                        User = user
                  };

                  _context.Events.Add(e);
                  _context.SaveChanges();

                  return RedirectToAction(nameof(ScheduleController.Index), "Schedule",
                        new
                        {
                              day = ScheduleController.Exdatetime.Day,
                              month = ScheduleController.Exdatetime.Month,
                              year = ScheduleController.Exdatetime.Year
                        });
            }
      }
}