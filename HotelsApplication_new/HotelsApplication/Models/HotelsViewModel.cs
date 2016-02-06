using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelsApplication.Models
{
    public class HotelsViewModel
    {
        public List<Hotels> HotelsList { get; set; }
        public List<Cities> CitiesList { get; set; }
        public Hotels SelectedHotel { get; set; }
        public string DisplayMode { get; set; }
    }
}