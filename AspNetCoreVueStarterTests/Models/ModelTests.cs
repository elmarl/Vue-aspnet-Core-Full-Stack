using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AspNetCoreVueStarter.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspNetCoreVueStarterTests.Models
{
    [TestClass()]
    public class ModelTests
    {
        [TestMethod]
        public void EventModel_WorkingCaseValidation()
        {
            var sut = new EventModel() {EventName="name", EventDate=DateTime.Today.AddDays(1), Location="loc", };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            Assert.AreEqual(true, isModelStateValid);
        }
        // Validate custom date attribute
        [TestMethod]
        public void EventModel_CustomDateAttributeValidation()
        {
            var sut = new EventModel() { EventName = "name", EventDate = DateTime.Today.AddDays(-1), Location = "loc", };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            Assert.AreEqual(false, isModelStateValid);
        }
        // Validate details length
        [TestMethod]
        public void EventModel_DetailsLengthValidation()
        {
            string details = new string('*', 1001);
            var sut = new EventModel() { EventName = "name", EventDate = DateTime.Today.AddDays(1), Location = "loc", Details = details, };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            Assert.AreEqual(false, isModelStateValid);
        }
        [TestMethod]
        public void EventModel_EmptyModelValidation()
        {
            var sut = new EventModel();
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            Assert.AreEqual(false, isModelStateValid);
        }
        // Participant Model Empty Model
        [TestMethod]
        public void ParticipantModel_EmptyModelValidation()
        {
            var sut = new ParticipantModel();
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            Assert.AreEqual(false, isModelStateValid);
        }
        [TestMethod]
        public void ParticipantMdel_CorrectModelValidation()
        {
            var sut = new ParticipantModel()
            {
                Firstname = "name",
                Familyname = "fname",
                Idcode = "123",
                Paymentmethod = "kaardimakse",
                ParticipantType = "person",
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            Assert.AreEqual(true, isModelStateValid);
        }
    }
}
