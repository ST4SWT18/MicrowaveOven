using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence


            System.Console.WriteLine("Tryk på O");
            bool run = true;
            while (run)
            {
                string input;
                input = Console.ReadLine().ToString().ToLower();
                //if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'o':
                        door.Open();
                        Console.WriteLine("\r\nTryk C");
                        break;
                    case 'c':
                        door.Close();
                        Console.WriteLine("\r\nTryk P");
                        break;
                    case 'p':
                        powerButton.Press();
                        Console.WriteLine("\r\nTryk P igen for mere tid eller T");
                        break;

                    case 't':
                        timeButton.Press();
                        Console.WriteLine("\r\nTryk T igen for mere tid eller S");
                        break;

                    case 's':
                        startCancelButton.Press();
                        break;
                }
            }


            //door.Open();

            //door.Close();

            //powerButton.Press();

            //timeButton.Press();

            //startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();
        }
    }
}
