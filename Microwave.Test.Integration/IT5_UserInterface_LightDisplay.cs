using System;
using System.IO;
using System.Threading;
using System.Linq;
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
    public class IT5_UserInterface_LightDisplay //TODO powertube display - stub evt button, door og output
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
            _powerButton = Substitute.For<IButton>();
            _timerButton = Substitute.For<IButton>();
            _startCancelButton = Substitute.For<IButton>();
            _output = Substitute.For<IOutput>();
            _door = Substitute.For<IDoor>();
            _display = new Display(_output);
            _light = new Light(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _sut = new UserInterface(_powerButton, _timerButton, _startCancelButton, _door, _display, _light, _cookController);
            _cookController.UI = _sut;


            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
        }
        [TearDown]
        public void TearDown()
        {
            _timer.Stop();
        }

        [Test]
        public void Door_OnDoorOpened_Output()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("on")));
        }


        [Test]
        public void Door_OnDoorClosed_Output()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("off")));
        }

        [Test]
        public void Button_PowerOn_Output50W()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("50 W")));
        }

        [Test]
        public void Button_PowerOn_Output700W()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("700 W")));
        }

        [Test]
        public void Button_Time_OutputOneMinute()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("01:00")));
        }

        [Test]
        public void Button_Time_OutputFiveMinute()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);

            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("05:00")));
        }

        [Test]
        public void Button_StartCancel_OutputLightOn()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);


            _output.Received(2).OutputLine(Arg.Is<string>(s => s.Contains("on")));
        }


        //Test til at finde 1000 fejlen
        [Test]
        public void CookingIsDone_StateCooking_CorrectTextIsDisplayed()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _timerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _sut.CookingIsDone();
            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("cleared")));
            _output.Received(2).OutputLine(Arg.Is<string>(s => s.Contains("off")));
        }

        [Test]
        public void Button_SetPower_OpenDoor()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);


            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("cleared")));
        }

        [Test]
        public void Button_SetPower_StartCancel()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _powerButton.Pressed += Raise.EventWith(this, EventArgs.Empty);
            _startCancelButton.Pressed += Raise.EventWith(this, EventArgs.Empty);


            _output.Received(1).OutputLine(Arg.Is<string>(s => s.Contains("cleared")));
        }
    }
}