using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gatherly.Models;

namespace Gatherly.Controllers
{
    public class VenueInsertController : Controller
    {
        GatherlyEntities dbobj = new GatherlyEntities();
        // GET: VenueInsert
        public ActionResult InsertVenuePageLoad()
        {
            return View();
        }

        public ActionResult AddVenue_Click(VenueInsertclss venueobj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (Session["owner_id"] == null)
                {
                    return RedirectToAction("LoginPageLoad", "Login");
                }

                int ownerid = Convert.ToInt32(Session["owner_id"]);


                if (file == null || file.ContentLength == 0)
                {
                    venueobj.venuemsg = "Please upload a venue image";
                    return View("AddVenuePageLoad", venueobj);
                }


                string fname = Path.GetFileName(file.FileName);


                string folder = Server.MapPath("~/VenuePhoto");

                string path = Path.Combine(folder, fname);

                file.SaveAs(path);


                string fullpath = Path.Combine("~\\VenuePhoto", fname);

                venueobj.venue_image = fullpath;


                dbobj.sp_venueinsert(
                    ownerid,
                    venueobj.venue_name,
                    venueobj.location,
                    venueobj.capacity,
                    venueobj.price,
                    venueobj.venue_type,
                    venueobj.description,
                    venueobj.available_from,
                    venueobj.available_to,
                    venueobj.venue_image
                );


                venueobj.venuemsg = "Venue successfully added";

                return View("InsertVenuePageLoad", venueobj);

            }
            return View("InsertVenuePageLoad", venueobj);
        }
    }
}