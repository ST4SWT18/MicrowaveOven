using System;
using System.IO;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_CookController_TimerPowerTubeDisplayOutput
    {
        private ITimer _timer;
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ICookController _sut;

        //Other
        private IButton _timerButton;
        private IButton _powerButton;
        private IButton _startCancelButton;
        private StringWriter _readConsole;
        private IUserInterface _userInterface;
        private IDoor _door;
        private ILight _light;

        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = new Output();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output); 
            _sut = new CookController(_timer, _display, _powerTube);

            //Other
            _timerButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
            _door = new Door();
            _light = new Light(_output);
            _userInterface = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _display, _light,
                _sut);
        }

        [TestCase(101, 10)]
        [TestCase(0, 10)]
        public void StartCooking_PowerOutOfRange_ThrowsException(int power, int time)
        {
            Assert.That(() => _sut.StartCooking(power, time), Throws.TypeOf<ArgumentOutOfRangeException>());
        }


        [TestCase(100, 101)]
        public void StartCooking_PowerInRange_ThrowsNothing(int power, int time)
        {
            Assert.That(() => _sut.StartCooking(power, time), Throws.Nothing);
        }

        [TestCase(100, 101)]
        [TestCase(50, 101)]
        [TestCase(1, 101)]
        public void StartCooking_PowerInRange_TimeRemainingIsEqualToTime(int power, int time)
        {
            _sut.StartCooking(power, time);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(time));
        }

        //[Test]
        //public void Stop_()
        //{
        //    _sut.Stop();
        //    _powerTube.Received(1).TurnOff();
        //}

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
    }
}