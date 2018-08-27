using CheckInWeb.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckInWeb.Models
{
    public class AddLocationViewModel
    {
        public  ApplicationUser User { get; set; }
        public Location Location { get; set; }
    }
}