using Garage_3_0.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage_3_0.Models
{
    internal class Car : Vehicle
    {
        private new int numberOfSeats;
        private new const int numberOfWheels = 4;

        //**********************************************************
        // Constructor for the Car class
        //**********************************************************
        public Car(string regNumber, string brand, Utilities.FuelType fuelType, string color, int numOfSeats) :
            base(regNumber, brand, fuelType, color, numOfSeats, numberOfWheels)
        {
            NumberOfSeats = numOfSeats;
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
            // set { numberOfWheels = value; }
        }

        //**********************************************************
        // Own version of ToString method
        //**********************************************************
        public override string ToString()
        {
            return $"{"Bil", -9} {RegistrationNumber, -10} {Brand, -8} {FuelType, -10} {Color, -8} {NumberOfSeats, -6} {NumberOfWheels, -6}";
        }
    }
}
