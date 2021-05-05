using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

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

        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = new Output();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output); //Substitute.For<IPowerTube>(); //
            _sut = new CookController(_timer, _display, _powerTube);
        }

        [TestCase(701, 10)]
        [TestCase(49, 10)]
        [TestCase(701, 10)]
        [TestCase(50, 10)]
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

        
    }
}