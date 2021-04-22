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
        private IPowerTube _uut;

        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _uut = new PowerTube(_output);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        [TestCase(101)]
        public void test(int power)
        {
            _uut.TurnOn(power);
            //_output.OutputLine(Arg.Is<string>(str));
        }
    }
}