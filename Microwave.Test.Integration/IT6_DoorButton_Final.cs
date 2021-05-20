using System;
using System.Collections.Generic;
using System.Text;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class IT6_DoorButton_Final
    {
        private Door _door;
        private Button _button;
        private IUserInterface _userInterface;

        [SetUp]
        public void Setup()
        {
            _door = new Door();
            _button = new Button();
            _userInterface = Substitute.For<IUserInterface>();
        }
    }
}
