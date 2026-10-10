using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gatherly.Models
{
    public class VenueSearchclss
    {
        public VenueSearchclss()
        {
            insertse = new VenueSearchFields();
            selectvenue = new List<UserViewVenueclss>();
        }

        public VenueSearchFields insertse { get; set; }

        public List<UserViewVenueclss> selectvenue { get; set; }
    }


    public class VenueSearchFields
    {
        public string location { get; set; }

        public int? capacity { get; set; }

        public decimal? price { get; set; }

        public string venue_type { get; set; }
    }
}