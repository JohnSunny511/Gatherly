using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Gatherly.Models
{
    public class VenueInsertclss
    {
        [Required(ErrorMessage = "Enter venue name")]
        public string venue_name { get; set; }

        [Required(ErrorMessage = "Enter location")]
        public string location { get; set; }

        [Required(ErrorMessage = "Enter capacity")]
        public int? capacity { get; set; }

        [Required(ErrorMessage = "Enter price")]
        public decimal? price { get; set; }

        [Required(ErrorMessage = "Select venue type")]
        public string venue_type { get; set; }

        [Required(ErrorMessage = "Enter description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Select available from date")]
        public DateTime? available_from { get; set; }

        [Required(ErrorMessage = "Select available to date")]
        public DateTime? available_to { get; set; }

        public string venue_status { get; set; }

        public string venue_image { get; set; }

        public string venuemsg { get; set; }
    }
}