using System;
using System.Threading;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
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

        [Test]
        public void test()
        {
            _powerButton.Press();
            _timerButton.Press();

            _startCancelButton.Press();

            Thread.Sleep(60100);

            


            //_sut.StartCooking(3, 1000);


            ////_timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            //_output.OutputLine(Arg.Is<string>(str => str.Contains($"{1},{55}")));
        }
    }
}