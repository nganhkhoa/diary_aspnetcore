﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace diary.Controllers
{
    public class SignInSignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}