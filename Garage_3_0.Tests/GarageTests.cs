using Garage_3_0.Models;

namespace Garage_2_0.Tests
{
    public class GarageTests
    {
        //********************************************************************************
        // Verify the GetVehicle() method
        //********************************************************************************
        [Theory]
 
        [InlineData("mJI 432", true)]
        [InlineData("lku074", true)]
        [InlineData("24jk80", false)]
        [InlineData("fgh 045b", false)]
        [InlineData("FgW789", true)]
        public void Garage_GetVehicle_Tests(string regNumber, bool expectedResult)
        {
            Garage garage = new Garage(30, 10, 25);
            //List<Vehicle> vehicles = new List<Vehicle>()

            // Arrange
            garage.AddVehicle("bil", "FgW789", "Audi", "4", "blue", 5);
            garage.AddVehicle("buss", "fgh 045g", "Volvo", "2", "röd", 50, 6);
            garage.AddVehicle("båt", " kvf 73e", "Sandström", "2", "vit", 5);
            garage.AddVehicle("mc", "LKU 074", "Kawazaki", "1", "gul");
            garage.AddVehicle("flygplan", "MJI432", "Boing", "2", "grön", 250);


            // Act
            bool found = garage.GetVehicle(regNumber);

            // Assert
            Assert.Equal(expectedResult, found);
        }
    }
}
