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


            bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast P, T eller C");
                input = Console.ReadLine().ToString().ToLower();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'p':
                        powerButton.Press();
                        Console.WriteLine("Tryk T");
                        break;

                    case 't':
                        timeButton.Press();
                        break;
                    case 'c':
                        startCancelButton.Press();
                        break;

                    //case 'c':
                    //    door.OnDoorClose(true);
                    //    break;

                    //case 'r':
                    //    System.Console.WriteLine("Indtast RFID id: ");
                    //    string idString = System.Console.ReadLine();

                    //    int id = Convert.ToInt32(idString);
                    //    rfidReader.OnRfidRead(id);
                    //    break;

                    default:
                        break;
                }

            } while (!finish);


            door.Open();

            door.Close();

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
