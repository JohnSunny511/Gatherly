using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gatherly.Models;

namespace Gatherly.Controllers
{
    public class OwnerRegController : Controller
    {
        GatherlyEntities dbobj = new GatherlyEntities();
        // GET: OwnerReg
        public ActionResult InsertOwnerPageLoad()
        {
            return View();
        }

        public ActionResult InsertOwner_Click(OwnerRegclss ownerRegclssobj, HttpPostedFileBase file, FormCollection form)
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
                    ownerRegclssobj.photo = fullpath;
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

                dbobj.sp_ownerreg(regid, ownerRegclssobj.name, ownerRegclssobj.email, ownerRegclssobj.phone, ownerRegclssobj.address, ownerRegclssobj.photo );
                dbobj.sp_LoginInsert(regid, ownerRegclssobj.username, ownerRegclssobj.pass, "owner");
                ownerRegclssobj.ownermsg = "Succesfully inserted";
                return View("InsertOwnerPageLoad", ownerRegclssobj);
            }
            return View("InsertOwnerPageLoad", ownerRegclssobj);
        }
    }
}