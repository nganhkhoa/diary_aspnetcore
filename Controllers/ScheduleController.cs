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


        private async Task<User> GetCurrentUser()
        {
            return await _usermanager.GetUserAsync(HttpContext.User);
        }


        public async Task<IActionResult> Index(int day, int month, int year)
        {
            if (!_signinmanager.IsSignedIn(User))
            {
                var strDatetime = day + "/" + month + "/" + year;
                Exdatetime = DateTime.Parse(strDatetime);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ModelCollection model = new ModelCollection();

            EntryContext entryContext = HttpContext.RequestServices.GetService(typeof(diary.Models.EntryContext)) as EntryContext;         
            model.entryList = entryContext.GetAllEntry();

            User user = new User();
            user = await GetCurrentUser();
            model.userId = _usermanager.GetUserId(User);

            return View(model);
        }


        public async Task<IActionResult> EntryProcess(ModelCollection model)
        {

            UploadDatetimeModel modeltemp = model.udModel;

            if (!_signinmanager.IsSignedIn(User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (modeltemp.strUpload == "" || modeltemp.strUpload == null)
            {
                return RedirectToAction("Index");
            }
            var entry = new Entry();
            entry.Content = modeltemp.strUpload;
            entry.Date = Exdatetime;
            entry.User = await _usermanager.GetUserAsync(User);
            _context.Entries.Add(entry);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}