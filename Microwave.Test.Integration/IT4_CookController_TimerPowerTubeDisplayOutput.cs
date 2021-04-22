using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
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
        private CookController _uut;

        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _output = new Output();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _uut = new CookController(_timer, _display, _powerTube);
        }
    }
}