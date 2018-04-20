using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Belt.Models;
using Microsoft.AspNetCore.Http;

namespace C_Sharp_Belt.Controllers
{
    public class BeltController : Controller
    {
        private BeltExamContext _context;
        private string _controller;
        private string _action;

        public BeltController(BeltExamContext context)
        {
            _context = context;
            _controller = "User";
            _action = "Login";
        }

        [HttpGet]
        [Route("Belt")]
        public IActionResult Belt()
        {
            if(isLoggedIn()){
                setSessionViewData();

                return View();
            }else{
                return RedirectToAction(_controller, _action);
            }
        }

        private void setSessionViewData()
        {
            ViewData["Username"] = HttpContext.Session.GetString("UserName");
            ViewData["UserId"] = (int)HttpContext.Session.GetInt32("UserId");
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

    }
}
