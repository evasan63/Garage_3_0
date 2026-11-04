using Garage_3_0.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_3_0.Models
{
    internal class Motorcycle : Vehicle
    {
        private new const int numberOfSeats = 1;
        private new const int numberOfWheels = 2;

        //**********************************************************
        // Constructor for the Car class
        //**********************************************************
        public Motorcycle(string registrationNumber, string brand, Utilities.FuelType fuelType, string color) : 
                          base (registrationNumber, brand, fuelType, color, numberOfSeats, numberOfWheels)
        {
        }

        //**********************************************************
        // Own version of ToString method
        //**********************************************************
        public override string ToString()
        {
            return $"{"MC", -9} {RegistrationNumber, -10} {Brand, -8} {FuelType,-10} {Color, -8} {NumberOfSeats, -6} {NumberOfWheels, -6}";
        }
    }
}
