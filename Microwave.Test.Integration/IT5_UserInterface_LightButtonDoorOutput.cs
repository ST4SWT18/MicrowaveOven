using System;
using System.IO;
using System.Linq;
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
        private UserInterface _sut;
        private StringWriter _readConsole;

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
            _sut = new UserInterface(_powerButton, _timeButton, _cancelButton, _door, _display, _light, _cookController);

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
            string outputText = "";
            var powerBeforeIncrease = 50;

            _powerButton.Press();

            outputText.Join(outputText, $"Display shows: {powerBeforeIncrease}W\r\n");
            _timeButton.Press();
            _timeButton.Press();

            
            var powerAfterIncrease = 100;

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {powerBeforeIncrease} W\r\nDisplay shows: {powerAfterIncrease} W\r\n"));
        }
    }
}