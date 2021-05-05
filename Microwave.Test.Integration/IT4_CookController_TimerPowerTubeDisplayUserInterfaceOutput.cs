using System;
using System.IO;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Timer = Microwave.Classes.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT4_CookController_TimerPowerTubeDisplayUserInterfaceOutput
    {
        private ITimer _timer;
        private IOutput _output;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IUserInterface _userInterface;
        private CookController _sut;
        private StringWriter _readConsole;


        private IButton _powerButton;
        private IButton _timerButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private ILight _light;

        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = new Output();
            _display = new Display(_output);

            _powerButton = new Button();
            _timerButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _light = new Light(_output);

            _powerTube = new PowerTube(_output);
            _sut = new CookController(_timer, _display, _powerTube);
            _userInterface = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _display, _light, _sut);
            _sut.UI = _userInterface;
            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
        }

        [TestCase(701, 10)]
        [TestCase(49, 10)]
        public void StartCooking_PowerOutOfRange_ThrowsException(int power, int time)
        {
            Assert.That(() => _sut.StartCooking(power, time), Throws.TypeOf<ArgumentOutOfRangeException>());
        }


        [TestCase(50, 101)]
        [TestCase(700, 101)]
        public void StartCooking_PowerInRange_ThrowsNothing(int power, int time)
        {
            Assert.That(() => _sut.StartCooking(power, time), Throws.Nothing);
        }

        [TestCase(50, 101)]
        [TestCase(700, 101)]
        public void StartCooking_PowerInRange_PowerIsCorrectlyWritten(int power, int time)
        {
            _sut.StartCooking(power, time);
            var text = _readConsole.ToString();
            Assert.That(text, Is.EqualTo($"PowerTube works with {power}\r\n"));
        }


        [TestCase(700, 101)]
        [TestCase(50, 101)]
        public void StartCooking_PowerInRange_TimeRemainingIsEqualToTime(int power, int time)
        {
            _sut.StartCooking(power, time);
            Assert.That(_timer.TimeRemaining, Is.EqualTo(time));
        }


        [TestCase(50, 101)]
        [TestCase(700, 101)]
        public void Stop_PowerTubeIsTurnedOff(int power, int time)
        {
            _sut.StartCooking(power, time);
            _sut.Stop();

            var text = _readConsole.ToString();
            Assert.That(text, Is.EqualTo($"PowerTube works with {power}\r\nPowerTube turned off\r\n"));
        }
        

        [TestCase(50, 60)]
        [TestCase(700, 60)]
        public void OnTimerTick_And_OnTimerExpired_ShowsTimeLeft_AndTimeDone(int power, int time)
        {
            string result = "";
            
            _powerButton.Press();
            result += string.Join("", $"Display shows: {power} W\r\n");

            _timerButton.Press();
            result += string.Join("", $"Display shows: 01:00\r\n");

            _startCancelButton.Press();
            result += string.Join("", $"Light is turned on\r\nPowerTube works with {power}\r\n");

            Thread.Sleep(6000);

            result += string.Join("", $"PowerTube works with {power}\r\n");

            for (int i = 1; i < 61; i++)
            {
                result += string.Join("", $"Display shows: 00:0" + (time - i) + "\r\n");
            }

            result += string.Join("", $"PowerTube turned off\r\n");
            result += string.Join("", "Display cleared\r\nLight is turned off\r\n");
            var text = _readConsole.ToString();
            Assert.That(text, Is.EqualTo(result));
        }
    }
}