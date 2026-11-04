using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_3_0.Models
{
    public class SearchCriteria
    {
        public string SearchVehicleType { get; set; }
        public string SearchBrand { get; set; }
        public string SearchFuelType { get; set; }
        public string SearchColor { get; set; }
        public int SearchNumOfSeats { get; set; }
        public int SearchNumOfWheels { get; set; }

        public SearchCriteria(string vehicleType, string brand, string fuelType, string color, int numOfSeats, int numOfWheels)
        {
            SearchVehicleType = vehicleType;
            SearchBrand = brand;
            SearchFuelType = fuelType;
            SearchColor = color;
            SearchNumOfSeats = numOfSeats;
            SearchNumOfWheels = numOfWheels;
        }   

    }
}
