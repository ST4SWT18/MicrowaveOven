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
        private ILight _uut;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _uut = new Light(_output);
        }

        [Test]
        public void TurnOn_LightIsTurnedOnIsWritten()
        {
            _uut.TurnOn();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"on")));
        }

        [Test]
        public void TurnOff_LightIsTurnedOffIsWritten()
        {
            _uut.TurnOff();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains("off")));
        }
    }
}