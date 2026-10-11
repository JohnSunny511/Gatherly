
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

                        // Get owner name
                        var owner = dbobj.owners
                            .FirstOrDefault(x => x.owner_id == login.reg_id);

                        if (owner != null)
                        {
                            Session["owner_name"] = owner.owner_name;
                        }

                        return RedirectToAction(
                            "InsertVenuePageLoad",
                            "VenueInsert"
                        );
                    }
                    else if (login.user_type == "user")
                    {
                        Session["user_id"] = login.reg_id;

                        // linq query to get username 
                        var user = dbobj.users.FirstOrDefault(x => x.user_id == login.reg_id);

                        if (user != null)
                        {
                            Session["user_name"] = user.user_name;
                        }

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

        public ActionResult Logout_Click()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("LoginPageLoad", "Login");
        }
    }
}
