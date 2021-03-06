using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class IT6_UserInterface_DoorButton
    {
        private Door _door;
        private Button _powerButton;
        private Button _timerButton;
        private Button _startCancelButton;
        private IUserInterface _userInterface;
        private IOutput _output;
        private IDisplay _display;
        private ICookController _cookController;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private ILight _light;
        private StringWriter _readConsole;

        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();
            _output = new Output();
            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _light = new Light(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _userInterface;

            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
        }

        [TearDown]
        public void TearDown()
        {
            _timer.Stop();
        }

        [Test]
        public void PowerButton_Press2Times_50And100IsDisplayed()
        {
            _powerButton.Press();
            _powerButton.Press();

            var powerBeforeIncrease = 50;
            var powerAfterIncrease = 100;

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {powerBeforeIncrease} W\r\nDisplay shows: {powerAfterIncrease} W\r\n"));
        }

        [Test]
        public void SetTime_DisplayCorrectText()
        {
            string result = "";
            var power = 50;

            _powerButton.Press();

            result += string.Join("", $"Display shows: {power} W\r\n");

            for (int i = 0; i < 5; i++)
            {
                _timerButton.Press();
                result += string.Join("", $"Display shows: 0{(1 * i) + 1}:00\r\n");
            }

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

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
        public void PowerTimerAndStartPress_DisplaysCorrectText(int NumberOfPowerPresses)
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

        [Test]
        public void StartCancelPressed_StateSetPower_CorrectTextIsDisplayed()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _startCancelButton.Press();
            result += string.Join("", "Display cleared\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void StartCancelPressed_StateSetTime_CorrectTextIsDisplayed()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _timerButton.Press();
            result += string.Join("", "Display shows: 01:00\r\n");

            _startCancelButton.Press();
            result += string.Join("", "Light is turned on\r\nPowerTube works with 50\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void StartCancelPressed_StateCooking_CorrectTextIsDisplayed()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _timerButton.Press();
            result += string.Join("", "Display shows: 01:00\r\n");

            _startCancelButton.Press();
            result += string.Join("", "Light is turned on\r\nPowerTube works with 50\r\n");


            _startCancelButton.Press();
            result += string.Join("", "PowerTube turned off\r\nLight is turned off\r\nDisplay cleared\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void DoorOpened_StateReady_CorrectTextIsDisplayed()
        {
            string result = "";

            _door.Open();
            result += string.Join("", "Light is turned on\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void DoorOpened_StateSetPower_CorrectTextIsDisplayed()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _door.Open();
            result += string.Join("", "Light is turned on\r\nDisplay cleared\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void DoorOpened_StateSetTime_CorrectTextIsDisplayed()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _timerButton.Press();
            result += string.Join("", "Display shows: 01:00\r\n");

            _door.Open();
            result += string.Join("", "Light is turned on\r\nDisplay cleared\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void DoorOpened_StateCooking_CorrectTextIsDisplayed()
        {
            string result = "";

            _powerButton.Press();
            result += string.Join("", "Display shows: 50 W\r\n");

            _timerButton.Press();
            result += string.Join("", "Display shows: 01:00\r\n");

            _startCancelButton.Press();
            result += string.Join("", "Light is turned on\r\nPowerTube works with " + 50 + "\r\n");


            _door.Open();
            result += string.Join("", "PowerTube turned off\r\nDisplay cleared\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }

        [Test]
        public void DoorClosed_StateDoorOpen_CorrectTextIsDisplayed()
        {
            string result = "";

            _door.Open();
            result += string.Join("", "Light is turned on\r\n");

            _door.Close();
            result += string.Join("", "Light is turned off\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
        }
    }
}
