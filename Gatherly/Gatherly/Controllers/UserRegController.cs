using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gatherly.Models;

namespace Gatherly.Controllers
{
    public class UserRegController : Controller
    {
        GatherlyEntities dbobj = new GatherlyEntities();
        // GET: UserReg
        public ActionResult InsertUserPageLoad()
        {
            return View();
        }

        public ActionResult InsertUser_Click(UserRegclss userRegclssobj, HttpPostedFileBase file, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                if (file.ContentLength > 0)
                {
                    //for inserting to project folder
                    string fname = Path.GetFileName(file.FileName);
                    var s = Server.MapPath("~/Photo");
                    string pa = Path.Combine(s, fname);
                    file.SaveAs(pa);

                    //for inserting path to table
                    var fullpath = Path.Combine("~\\Photo", fname);
                    userRegclssobj.photo = fullpath;
                }
                var getmaxid = dbobj.sp_maxLogId().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;
                }

                dbobj.sp_userreg(regid, userRegclssobj.name,userRegclssobj.age, userRegclssobj.phone, userRegclssobj.email, userRegclssobj.address, userRegclssobj.photo);
                dbobj.sp_LoginInsert(regid, userRegclssobj.username, userRegclssobj.pass, "user");
                userRegclssobj.usermsg = "Succesfully inserted";
                return View("InsertuserPageLoad", userRegclssobj);
            }
            return View("InsertuserPageLoad", userRegclssobj);
        }


    }
}