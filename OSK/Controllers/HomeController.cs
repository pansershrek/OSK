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
            Authoriz calend = new Authoriz();
            if (ModelState.IsValid && calend.cal_event(st)==0)
            {
                
                return LocalRedirectPermanent("~/Home/Ty");
            }
            else
            {
                @ViewData["Message"] = "Invalid!!!";
                return View();
            }

            
        }
        /*public string Index(Statement st)
        {
            return st.Day + "kek" + st.Event_Begin;
        }*/
    }
}
