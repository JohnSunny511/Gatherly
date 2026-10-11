using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gatherly.Models
{
    public class VenueBookingclss
    {
        public int venue_id { get; set; }

        public DateTime event_date { get; set; }

        public int guest_count { get; set; }

        public string event_type { get; set; }

        public string requirements { get; set; }




        public string venue_name { get; set; }
        public string venue_image { get; set; }
        public string location { get; set; }
        public int capacity { get; set; }
        public decimal price { get; set; }
        public string venue_type { get; set; }
    }
}