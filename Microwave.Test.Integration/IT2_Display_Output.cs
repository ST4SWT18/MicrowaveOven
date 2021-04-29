using System.IO;
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
        private Display _sut;
        private StringWriter _readConsole;
        
        [SetUp]
        public void SetUp()
        {
            _output = new Output();
            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
            _sut = new Display(_output);
        }

        [TestCase(1,2)]
        [TestCase(2, 55)]
        [TestCase(10, 22)]
        [TestCase(55, 17)]
        public void ShowTime_MinutesAndSecondsIsDisplayed(int min, int sec)
        {
            _sut.ShowTime(min, sec);

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {min:D2}:{sec:D2}\r\n"));

            //_output.OutputLine(Arg.Is<string>(str => str.Contains($"{min},{sec}")));
        }

        [TestCase(1)]
        [TestCase(500)]
        [TestCase(1000)]
        public void ShowPower_PowerIsDisplayed(int power)
        {
            _sut.ShowPower(power);

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {power} W\r\n"));

            //_output.OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [Test]
        public void Clear_DisplayClearedIsDisplayed()
        {
            _sut.Clear();

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo("Display cleared\r\n"));

            //_output.OutputLine(Arg.Is<string>(str => str.ToLower().Contains("cleared")));
        }

    }
}