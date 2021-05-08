using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSpot.Models;

namespace BlogSpot.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Create New Blog
        [HttpGet]
        public IActionResult CreateBlog()
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

        [HttpPost]
        public IActionResult CreateBlog(BlogModel newBlog)
        {
            if (HttpContext.Session.Keys.Contains("user_id"))
            {
                if(ModelState.IsValid)
                {
                   // string currentDate= DateTime.Now.ToString("dddd, dd MMMM yyyy HH: mm:ss");
                    //newBlog.PublicationDate = System.Convert.ToDateTime(currentDate);
                    if(DBHandlerModel.AddNewBlog(newBlog))
                    {
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        #endregion


        #region ViewAllUserBlog for all blogs by the particular user

        public IActionResult ViewAllBlogs()
        {
            if(HttpContext.Session.Keys.Contains("user_id"))
            {
              
                return View(DBHandlerModel.getBlogsByUsers(HttpContext.Session.GetString("user_name"))  );
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion

       
    }
}
