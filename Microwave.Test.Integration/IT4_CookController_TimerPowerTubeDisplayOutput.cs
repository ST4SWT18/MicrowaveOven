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
        private CookController _sut;

        //Other
        private IButton _timerButton;
        private IButton _powerButton;
        private IButton _startCancelButton;
        private StringWriter _readConsole;

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

        [TestCase(1,1)]
        public void test(int NumberOfPowerPresses,int time)
        {
            string result = "";

            //int timesRun = NumberOfPowerPresses + 1;

            for (int i = 0; i < NumberOfPowerPresses; i++)
            {
                _powerButton.Press();
                result += string.Join("", "Display shows: " + 50 * i + " W\r\n");
            }
            //var text = _readConsole.ToString();

            //_timerButton.Press();
            
            //for (int i = 0; i < time;)
            //{
            //    i++;
            //    _timerButton.Press();
            //    string eachTime = "Display shows: 0" + i + ":00\r\n";
            //    result += string.Join("", eachTime);
            //}

            _startCancelButton.Press();
            result += string.Join("", "Light is turned on\r\nPowerTube works with " + 50 * NumberOfPowerPresses + "\r\n");

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo(result));
            //Assert.AreEqual(result, text);
        }
    }
}