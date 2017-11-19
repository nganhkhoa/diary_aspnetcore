using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using diary.Data;

namespace diary.Controllers
{
      public class ScheduleController : Controller
      {
            diaryContext _context;
            public ScheduleController(diaryContext context)
            {
                  _context = context;
            }

            public IActionResult Index()
            {
                  return View();
            }
      }
}