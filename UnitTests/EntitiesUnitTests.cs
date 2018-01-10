using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using Domain.ValueObjects;

namespace UnitTests
{
    [TestClass]
    public class EntitiesUnitTests
    {
        User user;
        TransportMean transportMean;
        DateTime creationDate;
        Comment comment;

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
            transportMean = TransportMean.Create("IdentifyingCode Test", null, 0.0, null);
            creationDate = DateTime.Now;
            comment = Comment.Create(transportMean, user, creationDate, "Comment Text", 0.0, 0.0);
        }
       
        [TestMethod]
        public void CreateUserTest()
        {
            var result = user.Name.Equals("Test")&&
            user.Password.Equals("TestPassword")&&
            user.Username.Equals("testing")&&
            user.Email.Equals("Test@Automation.com")&&
            user.Trust.Equals(0.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            user.Update("Test Updated", "Password Updated", "Username Updated", "Email@Updated", 1.0, null);

            var result = user.Name.Equals("Test Updated")&&
            user.Password.Equals("Password Updated")&&
            user.Username.Equals("Username Updated")&&
            user.Email.Equals("Email@Updated")&&
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
            TransportMean transportMean = TransportMean.Create("IdentifyingCode Test", null, 0.0, null);
            transportMean.Update("IdentifyingCode Test Updated", null, 1.0, null);

            Assert.IsTrue(transportMean.IdentifyingCode.Equals("IdentifyingCode Test Updated"));
            Assert.IsTrue(transportMean.Rating.Equals(1.0));
        }

        [TestMethod]
        public void CreateCommentTest()
        {
            var result = comment.TransportationMean.Equals(transportMean) &&
                         comment.User.Equals(user) &&
                         comment.CreationDate.Equals(creationDate) &&
                         comment.Text.Equals("Comment Text") &&
                         comment.Rating.Equals(0.0) &&
                         comment.Trust.Equals(0.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void UpdateCommentTest()
        {

            comment.Update(transportMean, user, creationDate, "Updated comment text", 1.0, 1.0);
            var result = comment.TransportationMean.Equals(transportMean) &&
                          comment.User.Equals(user) &&
                          comment.CreationDate.Equals(creationDate) &&
                          comment.Text.Equals("Updated comment text") &&
                          comment.Rating.Equals(1.0) &&
                          comment.Trust.Equals(1.0);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetAuthenticationDataTest()
        {
            byte[] byt = new byte[] { 64,255};
            user.SetAuthenticationData("pass1", byt);

            var result = user.Password.Equals("pass1")
                && user.Salt.Equals(byt);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetCoordinatesTest()
        {
            Coordinates result = new Coordinates(12.0, 14.5);
            Assert.IsTrue(result.Latitude.Equals(12.0) && result.Longitude.Equals(14.5));
        }
    }
}