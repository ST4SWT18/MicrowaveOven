using System;
using System.IO;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NUnit.Framework;
using NSubstitute;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_PowerTube_Output
    {
        private IOutput _output;
        private IPowerTube _sut;
        private StringWriter _readConsole;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _sut = new PowerTube(_output);
            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
        }

        [TestCase(50)]
        [TestCase(700)]
        public void TurnOn_PowerIsCorrectlyWritten(int power)
        {
            _sut.TurnOn(power);

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"PowerTube works with {power}\r\n"));
        }

        [TestCase(50)]
        [TestCase(700)]
        public void TurnOff_PowerIsTurnedOffIsWritten(int power)
        {
            _sut.TurnOn(power);
            _sut.TurnOff();

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"PowerTube works with {power}\r\nPowerTube turned off\r\n"));
            
            //_output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"off")));
        }
    }
}