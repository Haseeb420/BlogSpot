using BlogSpot.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogSpot.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.Keys.Contains("user_id"))
            {
                if(HttpContext.Session.GetInt32("user_rank")==1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "User");

                }
            }
            else
            {
                return View();
            }
        }


        #region to show all blogs

        public IActionResult ViewAllBlogs()
        {
            if (HttpContext.Session.Keys.Contains("user_id"))
            {

                return View(DBHandlerModel.GetAllBlogs());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

        #region SignIN Code here
        [HttpPost]
        public IActionResult SignIn(LoginModel loginData)
        {
            if (ModelState.IsValid)
            {
                if(DBHandlerModel.UserLogin(loginData))
                {
                    UserModel LoggedInUser = DBHandlerModel.GetUserData(loginData);

                    HttpContext.Session.SetInt32("user_id", LoggedInUser.UserID);
                    HttpContext.Session.SetInt32("user_rank", LoggedInUser.Rank);
                    HttpContext.Session.SetString("user_name", LoggedInUser.Name);
                    HttpContext.Session.SetString("user_dob", LoggedInUser.DateOfBrith.ToString());
                    HttpContext.Session.SetString("user_email", LoggedInUser.Email);
                    HttpContext.Session.SetString("user_gender", LoggedInUser.Gender);
                    if (LoggedInUser.Rank==1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "User");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region SignUp code here
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserModel newUserData)
        {
            if(ModelState.IsValid)
            {
                if(DBHandlerModel.AddNewUser(newUserData))
                {
                    int user_id = DBHandlerModel.GetUserID(newUserData.Email,newUserData.Password);
                    HttpContext.Session.SetInt32("user_id", user_id);
                    HttpContext.Session.SetInt32("user_rank", 2);
                    HttpContext.Session.SetString("user_name", newUserData.Name);
                    HttpContext.Session.SetString("user_dob", newUserData.DateOfBrith.ToString());
                    HttpContext.Session.SetString("user_email", newUserData.Email);
                    HttpContext.Session.SetString("user_gender", newUserData.Gender);
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    return View();

                }

            }
            else
            {
                return View();

            }
        }

        #endregion


        #region Profile Region
        public IActionResult Profile()
        {
            if(HttpContext.Session.Keys.Contains("user_id"))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

        #region Logout method
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_id");
            HttpContext.Session.Remove("user_name");
            HttpContext.Session.Remove("user_rank");
            HttpContext.Session.Remove("user_email");
            HttpContext.Session.Remove("user_dob");
            HttpContext.Session.Remove("user_gender");
            
            return View("Index");
        }
        #endregion
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
