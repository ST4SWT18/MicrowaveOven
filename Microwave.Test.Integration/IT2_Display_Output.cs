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
            _sut = new Display(_output);
            _readConsole = new StringWriter();
            System.Console.SetOut(_readConsole);
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
        }

        [TestCase(50)]
        [TestCase(500)]
        [TestCase(700)]
        public void ShowPower_PowerIsDisplayed(int power)
        {
            _sut.ShowPower(power);

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo($"Display shows: {power} W\r\n"));
        }

        [Test]
        public void Clear_DisplayClearedIsDisplayed()
        {
            _sut.Clear();

            var text = _readConsole.ToString();

            Assert.That(text, Is.EqualTo("Display cleared\r\n"));
        }

    }
}