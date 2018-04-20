using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Belt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace C_Sharp_Belt.Controllers
{
    public class HomeController : Controller
    {
        private BeltExamContext _context;
        private string _controller;
        private string _action;

        public HomeController(BeltExamContext context)
        {
            _context = context;
            _controller = "User";
            _action = "Login";
        }

        [HttpGet]
        [Route("Home")]
        public IActionResult Home()
        {
            if(isLoggedIn()){
                setSessionViewData();

                List<Activities> activityInfo = _context.activites.Include( j => j.JoinedUsers ).Include( u => u.CreatedBy ).ToList();

                return View(activityInfo);
            }else{
                return RedirectToAction(_controller, _action);
            }
        }

        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            if(isLoggedIn()){
                return View(new Activities());
            }else{
                return RedirectToAction(_controller, _action);
            }
        }

        [HttpPost]
        [Route("CreateActivity")]
        public IActionResult CreateActivity(Activities activityInfo){
            if(isLoggedIn()){
                if(ModelState.IsValid){
                    setSessionViewData();

                    User user = _context.users.Include( j => j.JoinedActivities ).ThenInclude( p => p.ActivityInfo ).SingleOrDefault(u => u.UserId == (int)ViewData["UserId"]);
                
                    activityInfo.CreatedBy = user;
                    _context.activites.Add(activityInfo);
                    _context.user_activity.Add(new UserActivity{ ActivityId = activityInfo.ActivityId, ActivityInfo = activityInfo, JoinedUserId = user.UserId, JoinedUser = user });
                    _context.SaveChanges();

                    return RedirectToAction("ActivityDetails", new { ActivityId = activityInfo.ActivityId });
                }
            }else{
                return RedirectToAction(_controller, _action);
            }

            return View("New", activityInfo);
        }

        [HttpGet]
        [Route("ActivityDetails/{ActivityId}")]
        public IActionResult ActivityDetails(int ActivityId){
            if(isLoggedIn()){
                setSessionViewData();

                Activities activityInfo = _context.activites.Include( u => u.JoinedUsers ).ThenInclude( g => g.JoinedUser ).SingleOrDefault(u => u.ActivityId == ActivityId);
            
                foreach(UserActivity user in activityInfo.JoinedUsers){
                    Console.WriteLine(user.JoinedUser.FirstName);
                }

                if(activityInfo != null){
                    return View(activityInfo);
                }else{
                    return RedirectToAction("Home");
                }
            }else{
                return RedirectToAction(_controller, _action);
            }
        }

        [HttpGet]
        [Route("Delete/{ActivityId}")]
        public IActionResult Delete(int ActivityId){
            if(isLoggedIn()){
                Activities activityInfo = _context.activites.SingleOrDefault(u => u.ActivityId == ActivityId);

                if(activityInfo != null){
                    _context.activites.Remove(activityInfo);
                }

                _context.SaveChanges();

                return RedirectToAction("Home");
            }else{
                return RedirectToAction(_controller, _action);
            }
        }

        [HttpGet]
        [Route("JoinLeave/{ActivityId}/{location}")]
        public IActionResult JoinLeave(int ActivityId, string location)
        {
            setSessionViewData();

            UserActivity userActivity = _context.user_activity.Where(p => p.ActivityId == ActivityId).SingleOrDefault(u => u.JoinedUserId == (int)ViewData["UserId"]);

            if(userActivity != null){
                _context.user_activity.Remove(userActivity);
            }else{
                _context.user_activity.Add(new UserActivity{ ActivityId = ActivityId, JoinedUserId = (int)ViewData["UserId"] });
            }

            _context.SaveChanges();

            return RedirectToAction(location);
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
