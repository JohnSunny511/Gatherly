using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gatherly.Models;

namespace Gatherly.Controllers
{
    public class LoginController : Controller
    {
        GatherlyEntities dbobj = new GatherlyEntities();
        // GET: Login
        public ActionResult LoginPageLoad()
        {
            return View();
        }


        public ActionResult Login_Click(Loginclss loginobj)
        {
            if (ModelState.IsValid)
            {
                var login = dbobj.sp_login(
                loginobj.username,
                loginobj.password
                ).FirstOrDefault();

                if (login != null)
                {
                    if (login.user_type == "owner")
                    {
                        Session["owner_id"] = login.reg_id;

                        return RedirectToAction(
                            "InsertVenuePageLoad",
                            "VenueInsert"
                        );
                    }
                    else if (login.user_type == "user")
                    {
                        Session["user_id"] = login.reg_id;

                        return RedirectToAction(
                            "UserViewVenue_PageLoad",
                            "UserViewVenue"
                        );
                    }
                }

                loginobj.loginmsg = "Invalid username or password";
            }

            return View("LoginPageLoad", loginobj);
        }
    }
}