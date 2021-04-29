using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT5_UserInterface_LightButtonDoorOutput
    {
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _cancelButton;
        private IOutput _output;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _cancelButton = new Button();
            _output = new Output();
            _door = new Door();
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _uut = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void ResetValues_PowerLevelAndTime_IsReset()
        {
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_cancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            //_uut.OnPowerPressed(this, EventArgs.Empty);
            //_powerButton.Press();
            _powerButton.Press();

            var powerBeforeIncrease = 50;
            var powerAfterIncrease = 100;
            var powerAfterReset = 50;
            //_output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"{powerBeforeIncrease}")));
            //_output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"{powerAfterIncrease}")));
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"Display shows: 100 W")));
        }
    }
}