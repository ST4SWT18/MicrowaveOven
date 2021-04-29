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

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _sut = new Light(_output);
        }

        [Test]
        public void TurnOn_LightIsTurnedOnIsWritten()
        {
            _sut.TurnOn();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"on")));
        }

        [Test]
        public void TurnOff_LightIsTurnedOffIsWritten()
        {
            _sut.TurnOff();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains("off")));
        }
    }
}