using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AspNetCoreVueStarter.Service;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreVueStarter.Models;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreVueStarter.Controllers.Tests
{
    
    [TestClass()]
    public class EventsControllerTests
    {
        private Mock<IEventService> _mockService;

        [TestMethod()]
        public void GetEventModelTest()
        {
            // Arrange
            _mockService = new Mock<IEventService>();
            _mockService.Setup(repo => repo.GetEvents())
            .Returns(GetTestSessions());
            var controller = new EventsController(_mockService.Object);

            // Act
            var result = controller.GetEventModel();
        
            // Assert
            Assert.IsInstanceOfType(result.Value , typeof(IEnumerable<EventModel>));
        }
        [TestMethod]
        public void GetEventModelTest_EmptyRepo()
        {
            // Arrange
            _mockService = new Mock<IEventService>();
            _mockService.Setup(repo => repo.GetEvents())
            .Returns(GetEmptySession());
            var controller = new EventsController(_mockService.Object);

            // Act
            var result = controller.GetEventModel();

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<EventModel>));
        }
        [TestMethod]
        public void GetEventModelTest_NullIdRepo()
        {
            // Arrange
            int id = 0;
            _mockService = new Mock<IEventService>();
            _mockService.Setup(repo => repo.GetEvent(id))
            .Returns(GetNullSession());
            var controller = new EventsController(_mockService.Object);

            // Act
            var result = controller.GetEventModel(id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        // Test invalid modelstate for events
        [TestMethod]
        public void PostEventModelTest_addMissingNameEventToRepo()
        {
            // Arrange
            _mockService = new Mock<IEventService>();
            var controller = new EventsController(_mockService.Object);
            controller.ModelState.AddModelError("EventName","required");

            // Act
            var testEvent = new EventModel() { EventDate = DateTime.Now, Location = "testlocation", Details = "testdetails", };
            var result = controller.PostEventModel(testEvent);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        // Test case for deleting events
        [TestMethod]
        public void DeleteEventModel_DeleteNonExistantEvent()
        {
            // Arrange
            int id = 0;
            _mockService = new Mock<IEventService>();
            var controller = new EventsController(_mockService.Object);
            controller.ModelState.AddModelError("EventDate", "must be in the future");

            // Act
            var result = controller.DeleteEventModel(id);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        // Test invalid modelstate for events
        [TestMethod]
        public void PostParticipantModelTest_InvalidModelstate()
        {
            // Arrange
            _mockService = new Mock<IEventService>();
            var controller = new EventsController(_mockService.Object);
            controller.ModelState.AddModelError("Firstname", "required");

            // Act
            var pModel = new ParticipantModel() { Firstname="test", Familyname="test2", Idcode="123", ParticipantType="person", Paymentmethod="kaardimakse", };
            var result = controller.PostParticipantModel(0, pModel);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }
        // 
        private EventModel GetNullSession()
        {
            return null;
        }

        private List<EventModel> GetEmptySession()
        {
            return new List<EventModel>();
        }

        private List<EventModel> GetTestSessions()
        {
            var sessions = new List<EventModel>();
            sessions.Add(new EventModel()
            {
                Eventid = 1,
                EventName = "Test event name future",
                EventDate = new DateTime().AddDays(5),
                Location = "Test location",
                Details = "Test details",
            });
            sessions.Add(new EventModel()
            {
                Eventid = 1,
                EventName = "Test event name past",
                EventDate = new DateTime().AddDays(100),
                Location = "Test location 2",
            });
            return sessions;
        }
    }
}