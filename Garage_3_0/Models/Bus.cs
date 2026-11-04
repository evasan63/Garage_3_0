using Garage_3_0.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Garage_3_0.Models
{
    internal class Bus : Vehicle
    {
        private new int numberOfSeats;
        private new int numberOfWheels;

        //**********************************************************
        // Constructor for the Bus class
        //**********************************************************
        public Bus ( string regNumber, string brand, Utilities.FuelType fuelType, string color, int numberOfSeats, int numberOfWheels = 4) :
                     base ( regNumber, brand, fuelType, color, numberOfSeats, numberOfWheels)
        {
            NumberOfSeats = numberOfSeats;
            NumberOfWheels = numberOfWheels;
        }

        //**********************************************************
        // Getter and setter for NumberOfSeats     
        //**********************************************************
        public override int NumberOfSeats
        {
            get { return numberOfSeats; }
            set { numberOfSeats = value; }
        }

        //**********************************************************
        // Getter and setter for NumberOfWheels     
        //**********************************************************
        public override int NumberOfWheels
        {
            get { return numberOfWheels; }
            set { numberOfWheels = value; }
        }

        //**********************************************************
        // Own version of ToString method
        //**********************************************************    
        public override string ToString()
        {
            return $"{"Buss",-9} {RegistrationNumber, -10} {Brand, -8} {FuelType,-10} {Color, -8} {NumberOfSeats, -6} {NumberOfWheels, -6}";
        }
    }
}
