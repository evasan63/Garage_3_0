using Garage_3_0.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Garage_3_0.Models
{
    public class Vehicle
    {
        protected string vehicleType;
        protected string registrationNumber;
        protected string brand;
        protected Utilities.FuelType fuelType;
        protected string color;
        protected int numberOfSeats;
        protected int numberOfWheels;

        //**********************************************************
        // Constructor for the Vehicle class
        //**********************************************************
        public Vehicle ( string registrationNumber, string brand, Utilities.FuelType fuelType, string color)
        {
            RegistrationNumber = registrationNumber;
            Brand = brand;
            FuelType = fuelType;
            Color = color;
        }

        public Vehicle(string registrationNumber, string brand, Utilities.FuelType fuelType, string color, int numberOfSeats, int numberOfWheels)
        {
            RegistrationNumber = registrationNumber;
            Brand = brand;
            FuelType = fuelType;
            Color = color;
            NumberOfSeats = numberOfSeats;
            NumberOfWheels = numberOfWheels;
        }

        //**********************************************************
        // Getter and setter for RegistrationNumber
        //**********************************************************
        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set
            {
                if (Utilities.IsValidRegistrationNumber(value))
                {
                    registrationNumber = value.Replace(" ", "").ToUpperInvariant();
                }
                else
                {
                    throw new ArgumentException("Invalid registration number format.");
                }
            }
        }

        //**********************************************************
        // Getter and setter for Brand
        //**********************************************************
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        //**********************************************************
        // Getter and setter for FuelType
        //**********************************************************
        public Utilities.FuelType FuelType
        {
            get { return fuelType; }
            set 
            { 
                //if ((Utilities.FuelType) Enum.Parse(typeof(Utilities.FuelType), fuelType, true))
                fuelType = value;
            }
        }

        //**********************************************************
        //Getter and setter for Color
        //**********************************************************
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        //**********************************************************
        // Getter and setter for NumberOfSeats     
        //**********************************************************
        public virtual int NumberOfSeats
        {
            get { return numberOfSeats; }
            set { numberOfSeats = value; }
        }

        //**********************************************************
        // Getter and setter for NumberOfWheels     
        //**********************************************************
        public virtual int NumberOfWheels
        {
            get { return numberOfWheels; }
            set { numberOfWheels = value; }
        }

        //**********************************************************
        // Own version of ToString method
        //**********************************************************
        public virtual string ToString()
        {
            return $"{GetType().Name,-9} {RegistrationNumber,-10} {Brand,-8} " +
                   $"{Color,-8} {NumberOfSeats,-6} {NumberOfWheels,-6}";
        }

        //**********************************************************
        // Method ToFile method used to store the vehicle information
        // to a file when the application is closed.
        //**********************************************************
        public string ToFile()
        {
            return $"{GetType().Name} {RegistrationNumber} {Brand} {FuelType} " +
                   $"{Color} {NumberOfSeats} {NumberOfWheels}";
        }

    }
}
