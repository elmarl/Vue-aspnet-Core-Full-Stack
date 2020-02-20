using AspNetCoreVueStarter.Data;
using AspNetCoreVueStarter.Models;
using AspNetCoreVueStarter.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace AspNetCoreVueStarterTests.Serice
{
    [TestClass()]
    public class ServiceTests
    {
        private EventService eventService;
        private DataContext dataContext;

        [TestInitialize]
        public void Setup()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("testdb");
            dataContext = new DataContext(dbOptionsBuilder.Options);
            //eventService = new EventService(dataContext);
        }

        // test if service logic code works in normal conditions
        [TestMethod]
        public void AddParticipant_TestValidCase()
        {
            // Setup
            var mock = new Mock<IDataContext>();
            mock.Setup(x => x.SaveChanges())
                .Returns(1);
            mock.Setup(x => x.EventSet.Find(0))
                .Returns(new EventModel() { EventName="test", EventDate=DateTime.Now.AddDays(1), Location="test"});
            EventService userService = new EventService(mock.Object);

            // Act
            var us = userService.AddParticipant(0, new ParticipantModel { Firstname = "test", Familyname = "test", Idcode = "123", Paymentmethod = "sularaha", ParticipantType = "person", DetailsPerson = "test" });

            // Assert
            Assert.AreEqual(us, us);
        }
        // test if service logic code works in normal conditions
        [TestMethod]
        public void AddParticipant_TestNoFamilyNameCase()
        {
            // Setup
            var mock = new Mock<IDataContext>();
            mock.Setup(x => x.SaveChanges())
                .Returns(1);
            mock.Setup(x => x.EventSet.Find(0))
                .Returns(new EventModel() { EventName = "test", EventDate = DateTime.Now.AddDays(1), Location = "test" });
            EventService userService = new EventService(mock.Object);

            // Act
            var us = userService.AddParticipant(0, new ParticipantModel { Firstname = "test", Familyname = "", Idcode = "123", Paymentmethod = "sularaha", ParticipantType = "person", DetailsPerson = "test" });

            // Assert
            Assert.AreEqual(null, us);
        }
        // test if server logic will not allow a user to change family name to null
        [TestMethod]
        public void AddParticipant_TestValidLongCompanyDetails()
        {
            // Setup
            ParticipantModel pm = new ParticipantModel { Firstname = "test", Familyname = "", Idcode = "123", Paymentmethod = "sularaha", ParticipantType = "person"};
            var mock = new Mock<IDataContext>();
            mock.Setup(x => x.SaveChanges())
                .Returns(1);
            mock.Setup(x => x.ParticipantSet.Find(0))
                .Returns(returnNull());
            EventService userService = new EventService(mock.Object);

            // Act
            var us = userService.DeleteParticipant(0);

            // Assert
            Assert.AreEqual(null, us);
        }
        public ParticipantModel returnNull()
        {
            return null;
        }
    }
}
