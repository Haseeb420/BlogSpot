using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogSpot.Models;


namespace BlogSpot.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllUsers()
        {
            if(HttpContext.Session.Keys.Contains("user_id"))
            {
                return View(DBHandlerModel.GetAllUsers());

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
   
        public IActionResult DeleteUser(int id)
        {
            if(HttpContext.Session.Keys.Contains("user_id"))
            {
                if(HttpContext.Session.GetInt32("user_rank")==1)
                {
                   if( DBHandlerModel.deleteUserById(id))
                    {
                        return RedirectToAction("ViewAllUsers", "Admin");

                    }
                    else
                    {
                        return RedirectToAction("ViewAllUsers", "Admin");

                    }
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
    }

}
