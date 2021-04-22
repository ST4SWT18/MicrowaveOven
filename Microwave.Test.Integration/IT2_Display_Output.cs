using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT2_Display_Output
    {
        private IOutput _output;
        private Display _uut;
        
        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _uut = new Display(_output);
        }

        [TestCase(1,2)]
        public void ShowTime_MinutesAndSecondsIsShowed(int min, int sec)
        {
            _uut.ShowTime(min, sec);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{min},{sec}")));
        }

        [TestCase(50)]
        [TestCase(100)]
        public void ShowTime_MinutesAndSecondsIsShowed(int power)
        {
            _uut.ShowPower(power);
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains($"{power}")));
        }

        [Test]
        public void ClearDisplay()
        {
            _uut.Clear();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains("clear")));
        }
    }
}