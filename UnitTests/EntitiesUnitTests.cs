using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class EntitiesUnitTests
    {
        private Comment comment;
        private DateTime creationDate;
        private TransportMean transportMean;
        private User user;

        [TestCleanup]
        public void Clean()
        {
            Console.WriteLine("Clean Tests");
            user = null;
            transportMean = null;
            comment = null;
        }

        [TestInitialize]
        public void Init()
        {
            Console.WriteLine("Init Tests");
            user = User.Create("Test", "TestPassword", "testing", "Test@Automation.com", 0.0, new List<Comment>());
            transportMean = TransportMean.Create("IdentifyingCode Test", null, 0.0, null, 4);
            creationDate = DateTime.Now;
            comment = Comment.Create(transportMean.Id, user.Id, creationDate, "Comment Text", 0.0, 0.0);
        }

        [TestMethod]
        public void CreateUserTest()
        {
            var result = user.Name.Equals("Test") &&
                         user.Password.Equals("TestPassword") &&
                         user.Username.Equals("testing") &&
                         user.Email.Equals("Test@Automation.com") &&
                         user.Trust.Equals(0.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            user.Update("Test Updated", "Password Updated", "Username Updated", "Email@Updated", 1.0, null);

            var result = user.Name.Equals("Test Updated") &&
                         user.Password.Equals("Password Updated") &&
                         user.Username.Equals("Username Updated") &&
                         user.Email.Equals("Email@Updated") &&
                         user.Trust.Equals(1.0);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void CreateTransportMeanTest()
        {
            Assert.IsTrue(transportMean.IdentifyingCode.Equals("IdentifyingCode Test"));
            Assert.IsTrue(transportMean.Rating.Equals(0.0));
        }

        [TestMethod]
        public void UpdateTransportMeanTest()
        {
            var transportMean = TransportMean.Create("IdentifyingCode Test", null, 0.0, null, 4);
            transportMean.Update("IdentifyingCode Test Updated", null, 1.0, null, 4);

            Assert.IsTrue(transportMean.IdentifyingCode.Equals("IdentifyingCode Test Updated"));
            Assert.IsTrue(transportMean.Rating.Equals(1.0));
        }

        [TestMethod]
        public void CreateCommentTest()
        {
            var result = comment.TransportMeanId.Equals(transportMean.Id) &&
                         comment.UserId.Equals(user.Id) &&
                         comment.CreationDate.Equals(creationDate) &&
                         comment.Text.Equals("Comment Text") &&
                         comment.Rating.Equals(0.0) &&
                         comment.Trust.Equals(0.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateCommentTest()
        {
            comment.Update(transportMean.Id, user.Id, creationDate, "Updated comment text", 1.0, 1.0);
            var result = comment.TransportMeanId.Equals(transportMean.Id) &&
                         comment.UserId.Equals(user.Id) &&
                         comment.CreationDate.Equals(creationDate) &&
                         comment.Text.Equals("Updated comment text") &&
                         comment.Rating.Equals(1.0) &&
                         comment.Trust.Equals(1.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetAuthenticationDataTest()
        {
            byte[] byt = {64, 255};
            user.SetAuthenticationData("pass1", byt);

            var result = user.Password.Equals("pass1")
                         && user.Salt.Equals(byt);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetCoordinatesTest()
        {
            var result = new Coordinates(12.0, 14.5);
            Assert.IsTrue(result.Latitude.Equals(12.0) && result.Longitude.Equals(14.5));
        }
    }
}