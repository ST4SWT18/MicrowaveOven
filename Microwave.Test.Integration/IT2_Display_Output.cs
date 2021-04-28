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
        [TestCase(2, 55)]
        [TestCase(10, 22)]
        [TestCase(55, 17)]
        public void ShowTime_MinutesAndSecondsIsDisplayed(int min, int sec)
        {
            _uut.ShowTime(min, sec);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{min},{sec}")));
        }

        [TestCase(1)]
        [TestCase(500)]
        [TestCase(1000)]
        public void ShowPower_PowerIsDisplayed(int power)
        {
            _uut.ShowPower(power);
            _output.OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [Test]
        public void Clear_DisplayClearedIsDisplayed()
        {
            _uut.Clear();
            _output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

    }
}