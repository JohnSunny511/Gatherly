using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Gatherly.Models;

namespace Gatherly.Controllers
{
    public class UserViewVenueController : Controller
    {
        GatherlyEntities dbobj = new GatherlyEntities();

        // Display all venues when user opens the page by default
        public ActionResult UserViewVenue_PageLoad()
        {
            VenueSearchclss clsobj = new VenueSearchclss();

            clsobj.selectvenue = getdata("");

            return View(clsobj);
        }

        // Search venues using filters by user
        public ActionResult SearchVenue_Click(VenueSearchclss clsobj)
        {
            string qry = "";

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.location))
            {
                qry += " and location like '%" + clsobj.insertse.location + "%'";
            }

            if (clsobj.insertse.capacity.HasValue)
            {
                qry += " and capacity >= " + clsobj.insertse.capacity;
            }

            if (clsobj.insertse.price.HasValue)
            {
                qry += " and price <= " + clsobj.insertse.price;
            }

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.venue_type))
            {
                qry += " and venue_type like '%" + clsobj.insertse.venue_type + "%'";
            }

            clsobj.selectvenue = getdata(qry);

            return View("UserViewVenue_PageLoad", clsobj);
        }

        // Get venues from stored procedure
        private List<UserViewVenueclss> getdata(string qry)
        {
            List<UserViewVenueclss> venueList =
                new List<UserViewVenueclss>();

            string conString =
                ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("sp_VenueFilter", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    UserViewVenueclss obj = new UserViewVenueclss();

                    obj.venue_id = Convert.ToInt32(dr["venue_id"]);
                    obj.venue_image = dr["venue_image"].ToString();
                    obj.venue_name = dr["venue_name"].ToString();
                    obj.location = dr["location"].ToString();
                    obj.capacity = Convert.ToInt32(dr["capacity"]);
                    obj.price = Convert.ToDecimal(dr["price"]);
                    obj.venue_type = dr["venue_type"].ToString();

                    venueList.Add(obj);
                }

                con.Close();
            }

            return venueList;
        }
    }
}