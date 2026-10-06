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
                var getregid = dbobj.sp_login(
                    loginobj.username,
                    loginobj.password
                ).FirstOrDefault();

                if (getregid != null)
                {
                    int regid = Convert.ToInt32(getregid);

                    Session["reg_id"] = regid;
                    Session["owner_id"] = regid;

                    return RedirectToAction(
                        "InsertVenuePageLoad",
                        "VenueInsert"
                    );
                }

                loginobj.loginmsg = "Invalid username or password";
            }

            return View("LoginPageLoad", loginobj);
        }
    }
}