using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CheckInWeb.Data.Entities;
using System.Web;

namespace CheckInWeb.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckIn_()
        {
            //Arrange
            var user = new ApplicationUser();
            var location = new Location();
            var checkIn = new CheckIn();
            checkIn.User = user;
            checkIn.Location = location;
            checkIn.Time = DateTime.Now;
            //Act

            //Assert

        }
        
    }
}
