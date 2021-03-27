using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<HomeController> _logger;
        Repo _repo;
        public HomeController(ILogger<HomeController> logger, Repo repo, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _repo = repo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var logged = HttpContext.Session.GetString("userId");
            if (logged == null)
            {
                return RedirectToAction("login");
            }
            else
            {
                return View();
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult login()
        {
        
            return View();
        }
        [HttpPost]
        public JsonResult login(Users model)
        {
            var login = _repo.GetUserbyEmailAndPassword(model);
            if (login != null)
            {
                HttpContext.Session.SetString("userId", login.UserId.ToString());
                return Json(new {status = true,data = login });
            }
            else
            {
                return Json(new { status = false});
            }
            
        }
        [HttpPost]
        public JsonResult SignUp(UsersViewModel model)
        {
            try
            {
                Helper help = new Helper(_webHostEnvironment);


                Users user = new Users() {
                    Email = model.Email,
                    ImageUrl = help.Uploadlogo(model.Image),
                    Password = model.Password,
                    UserName = model.UserName
            };
                var signup = _repo.AddUser(user);
                return Json(new { status = true, data = signup });
            }
            catch (Exception ex)
            {

                return Json(new { status = false }); 
            }
            
           
          

        }
        [HttpPost]
        public JsonResult GetInbox()
        {
            try
            {
               
             
                return Json(new { status = true, data = _repo.RetriveMessages() });
            }
            catch (Exception ex)
            {

                return Json(new { status = false });
            }




        }
    }
}
