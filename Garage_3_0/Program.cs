
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;
using Garage_3_0.UI;

namespace Garage_3_0
{
    internal class Program
    {
        //**********************************************************
        // Main method to start the Garage application
        //**********************************************************
        private static void Main()
        {
            // Create an instance of the ConsoleUI class and run the application
            ConsoleUI ui = new ConsoleUI();
            ui.Run();
        }
    }
}
