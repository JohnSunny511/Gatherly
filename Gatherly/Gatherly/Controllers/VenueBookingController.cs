using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gatherly.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Gatherly.Controllers
{
    public class VenueBookingController : Controller
    {
        // GET: VenueBooking
        public ActionResult VenueBooking_PageLoad(int id)
        {
            VenueBookingclss clsobj = new VenueBookingclss();

            clsobj.venue_id = id;
            clsobj.event_date = DateTime.Today;
            clsobj.guest_count = 1;
            clsobj.event_type = "Wedding";
            clsobj.requirements = "";

            getVenueDetails(id, clsobj);

            ViewBag.BookedDates = getdata(id);

            return View(clsobj);
        }

        
public ActionResult VenueBooking_Click(VenueBookingclss clsobj)
        {
            if (Session["user_id"] == null)
            {
                return RedirectToAction("Login_PageLoad", "Login");
            }

            int userid = Convert.ToInt32(Session["user_id"]);

            string conString =
                ConfigurationManager.ConnectionStrings[
                    "importdataconnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                // Check venue is already booked
                SqlCommand cmdcheck =
                    new SqlCommand("SP_CheckVenueBooking", con);

                cmdcheck.CommandType = CommandType.StoredProcedure;
                cmdcheck.Parameters.AddWithValue("@venue_id", clsobj.venue_id);
                cmdcheck.Parameters.AddWithValue("@event_date", clsobj.event_date);

                int status = Convert.ToInt32(cmdcheck.ExecuteScalar());

                if (status == 1)
                {
                    ViewBag.Message =
                        "This venue is already booked on the selected date. Please choose another date.";

                    getVenueDetails(clsobj.venue_id, clsobj);
                    ViewBag.BookedDates = getdata(clsobj.venue_id);

                    return View("VenueBooking_PageLoad", clsobj);
                }

                // Insert booking request
                SqlCommand cmd =
                    new SqlCommand("SP_InsertVenueBooking", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@user_id", userid);
                cmd.Parameters.AddWithValue("@venue_id", clsobj.venue_id);
                cmd.Parameters.AddWithValue("@event_date", clsobj.event_date);
                cmd.Parameters.AddWithValue("@guest_count", clsobj.guest_count);
                cmd.Parameters.AddWithValue("@event_type", clsobj.event_type);
                cmd.Parameters.AddWithValue(
                    "@requirements",
                    clsobj.requirements ?? "");

                cmd.ExecuteNonQuery();

                TempData["Message"] =
                    "Your booking request has been submitted successfully!";
            }

            return RedirectToAction(
                "VenueBooking_PageLoad",
                new { id = clsobj.venue_id });
        }

        // Get booked dates for the venue to avoid further booking on that date
        private List<string> getdata(int venueid)
        {
            List<string> bookedDates = new List<string>();

            string conString =
                ConfigurationManager.ConnectionStrings[
                    "importdataconnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd =
                    new SqlCommand("SP_GetBookedDates", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@venue_id", venueid);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    bookedDates.Add(
                        Convert.ToDateTime(dr["event_date"])
                            .ToString("yyyy-MM-dd"));
                }
            }

            return bookedDates;
        }


        private void getVenueDetails(int venueid, VenueBookingclss clsobj)
        {
            string conString =
                ConfigurationManager.ConnectionStrings[
                    "importdataconnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT venue_id, venue_name, venue_image, location, capacity, price, venue_type " +
                    "FROM dbo.venue WHERE venue_id = @venue_id", con);

                cmd.Parameters.AddWithValue("@venue_id", venueid);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    clsobj.venue_id = Convert.ToInt32(dr["venue_id"]);
                    clsobj.venue_name = dr["venue_name"].ToString();
                    clsobj.venue_image = dr["venue_image"].ToString();
                    clsobj.location = dr["location"].ToString();
                    clsobj.capacity = Convert.ToInt32(dr["capacity"]);
                    clsobj.price = Convert.ToDecimal(dr["price"]);
                    clsobj.venue_type = dr["venue_type"].ToString();
                }

                dr.Close();
            }
        }
    }
}