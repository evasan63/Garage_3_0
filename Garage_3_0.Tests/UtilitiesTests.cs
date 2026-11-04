using System;
using System.Collections.Generic;
using System.Text;
using static Garage_3_0.Services;

namespace Garage_2_0.Tests
{
    public class UtilitiesTests
    {
        //********************************************************************************
        // Verify the IsValidRegistrationNumber() method
        //********************************************************************************
        [Theory]
        [InlineData("bil", true)]
        [InlineData("buss", true)]
        [InlineData("båt", true)]
        [InlineData("flygplan", true)]
        [InlineData("mc", true)]
        [InlineData("trehjuling", false)]
        [InlineData("1", false)]
        public void VehicleTypeValidTests(string vehicleType, bool expectedResult)
        {
            // Arrange


            // Act
            bool valid = Services.Utilities.IsValidVehicleType(vehicleType);

            // Assert
            Assert.Equal(valid, expectedResult);
        }

        //********************************************************************************
        // Verify the IsValidRegistrationNumber() method
        //********************************************************************************
        [Theory]
        [InlineData("hju78b", true)]
        [InlineData("LKY 257", true)]
        [InlineData("hab99bb", false)]
        [InlineData("l k U 0 7 3", true)]
        [InlineData("678456", false)]
        [InlineData("AA56DD", false)]
        [InlineData("678", false)]
        public void RegistrationNumberValidTests(string regNumber, bool expectedResult)
        {
            // Arrange


            // Act
            bool valid = Utilities.IsValidRegistrationNumber(regNumber);

            // Assert
            Assert.Equal(valid, expectedResult);
        }

        //********************************************************************************
        // Verify the CalculateParkingFee() method
        //********************************************************************************
        [Theory]
        [InlineData(5, 10, 30)]
        [InlineData(15, 10, 120)]
        [InlineData(12, 20, 200)]
        [InlineData(2, 25, 0)]
        [InlineData(24, 30, 360)]
        public void CalculateParkingFeeTests(int hoursParked,
                                             decimal hourlyRate,
                                             decimal expectedValue)
        {
            // Arrange


            // Act
            decimal parkingFee = Utilities.CalculateParkingFee(hoursParked, hourlyRate);

            // Assert
            Assert.Equal(parkingFee, expectedValue);
        }
    }
}
