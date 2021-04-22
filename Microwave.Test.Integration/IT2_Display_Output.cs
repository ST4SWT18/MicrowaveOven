using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
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

        [TestCase(10,2)]
        public void ShowTimeIsViewed(int min, int sec)
        {
            _uut.ShowTime(min, sec);
            _output.Received().
        }
    }
}