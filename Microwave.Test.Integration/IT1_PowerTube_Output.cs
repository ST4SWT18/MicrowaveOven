using System;
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

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _sut = new PowerTube(_output);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void TurnOn_PowerIsCorrectlyWritten(int power)
        {
            _sut.TurnOn(power);
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"{power}")));
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void TurnOff_PowerIsTurnedOffIsWritten(int power)
        {
            _sut.TurnOff();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"off")));
        }
    }
}