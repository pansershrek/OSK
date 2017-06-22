using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OSK.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Ty()
        {
            return View();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateReCaptchaAttribute))]
        public IActionResult Index(Statement st)
        {
            if (ModelState.IsValid)
            {
                //Отправляем запрос на Google календарь
                Authoriz calend = new Authoriz();
                calend.cal_event(st);
                return LocalRedirectPermanent("~/Home/Ty");
            }
            else
            {
                @ViewData["Message"] = "Invalid!!!";
                return View();
            }

            
        }

    }
}
