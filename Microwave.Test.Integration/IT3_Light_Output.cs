using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT3_Light_Output
    {
        private IOutput _output;
        private ILight _sut;
        private StringWriter _readConsole;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _sut = new Light(_output);
            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
        }

        [Test]
        public void TurnOn_LightIsTurnedOnIsWritten()
        {
            _sut.TurnOn();

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo("Light is turned on\r\n"));

            //_output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"on")));
        }

        [Test]
        public void TurnOff_LightIsTurnedOffIsWritten()
        {
            _sut.TurnOn();
            _sut.TurnOff();

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo("Light is turned on\r\nLight is turned off\r\n"));
            //_output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains("off")));
        }
    }
}