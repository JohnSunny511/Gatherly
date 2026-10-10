using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gatherly.Models
{
    public class UserViewVenueclss
    {
        public int venue_id { get; set; }

        public string venue_image { get; set; }

        public string venue_name { get; set; }

        public string location { get; set; }

        public int capacity { get; set; }

        public decimal price { get; set; }

        public string venue_type { get; set; }
    }
}