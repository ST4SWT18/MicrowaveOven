using System;
using System.IO;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_UserInterface_LightButtonDoorOutput
    {
        private IButton _powerButton;
        private IButton _timerButton;
        private IButton _startCancelButton;
        private IOutput _output;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IUserInterface _sut;
        private StringWriter _readConsole;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();
            _output = new Output();
            _door = new Door();
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _display, _light, _cookController);

            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
        }
        [Test]
        public void PowerButton_Press2Times_50And100IsWritten()
        {
    
            _powerButton.Press();
            _powerButton.Press();

            var powerBeforeIncrease = 50;
            var powerAfterIncrease = 100;

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {powerBeforeIncrease} W\r\nDisplay shows: {powerAfterIncrease} W\r\n"));
        }
        [Test]
        public void TimeButton_Press2Times_()
        {
            _powerButton.Press();
            _timerButton.Press();
            _timerButton.Press();

            var powerBeforeIncrease = 50;
            var powerAfterIncrease = 100;

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {powerBeforeIncrease} W\r\nDisplay shows: {powerAfterIncrease} W\r\n"));
        }

        //Skal måske være i UserInterface...
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        public void test(int NumberOfPowerPresses)
        {
            string result = "";

            NumberOfPowerPresses++;
            for (int i = 1; i < NumberOfPowerPresses; i++)
            {
                _powerButton.Press();
                result += string.Join("", "Display shows: " + 50 * i + " W\r\n");
            }

            NumberOfPowerPresses--;

            _timerButton.Press();

            result += string.Join("", "Display shows: 01:00\r\n");

            _startCancelButton.Press();
            result += string.Join("", "Light is turned on\r\nPowerTube works with " + 50 * NumberOfPowerPresses + "\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));

        }

        //Test til at finde 1000 fejlen
        [Test]
        public void test1()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _timerButton.Press();
            result += string.Join("", "Display shows: 01:00\r\n");

            _startCancelButton.Press();

            result += string.Join("", "Light is turned on\r\nPowerTube works with 50\r\n");

            Thread.Sleep(60100);

            for (int i = 1; i < 51; i++)
            {
                result += string.Join("", "Display shows: 00:"+(60-i)+"\r\n");
            }

            for (int i = 51; i < 61; i++)
            {
                result += string.Join("", "Display shows: 00:0" + (60 - i) + "\r\n");
            }

            result += string.Join("", "PowerTube turned off\r\n");
            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }
    }
}