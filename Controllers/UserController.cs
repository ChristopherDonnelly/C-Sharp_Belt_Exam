using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Belt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace C_Sharp_Belt.Controllers
{
    public class UserController : Controller
    {
        private BeltExamContext _context;
        private string _controller;
        private string _action;

        public UserController(BeltExamContext context)
        {
            _context = context;
            _controller = "Belt";
            _action = "Belt";
        }

        [HttpGet]
        [Route("")]
        [Route("Login")]
        public IActionResult Login()
        {
            if(isLoggedIn()){
                return RedirectToAction(_controller, _action);
            }else{
                return View();
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel model)
        {
			User user = _context.users.SingleOrDefault(u => u.Email == model.Email);
            model.Found = (user==null)?1:0;

            if(model.Found == 0){
                if(model.Password==null) model.Password=" ";

                var Hasher = new PasswordHasher<User>();
                model.PasswordConfirmation = (Hasher.VerifyHashedPassword(user, user.Password, model.Password) != 0)?0:1;
            }

            TryValidateModel(model);

            if(ModelState.IsValid)
            {
                saveSession(user);
                return RedirectToAction(_controller, _action);
            }

            return View("Login");
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            if(isLoggedIn()){
                return RedirectToAction(_controller, _action);
            }else{
                return View();
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.Unique = _context.users.Count(u => u.Email == model.Email);
                TryValidateModel(model);

                if(!ModelState.IsValid){
                    return View("Register");
                }else{
                    saveSession(model.createUser(_context));
                    return RedirectToAction(_controller, _action);
                }
            }

            return View("Register");
        }

        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }

        public void saveSession(User user){
            HttpContext.Session.SetInt32("UserId", (int)user.UserId);
            HttpContext.Session.SetString("UserName", (string)user.FirstName);
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

    }
}
