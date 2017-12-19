using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
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
            Assert.IsTrue(user.Name.Equals("Test"),"Create user is not correct!");
            Assert.IsTrue(user.Password.Equals("TestPassword"));
            Assert.IsTrue(user.Username.Equals("testing"));
            Assert.IsTrue(user.Email.Equals("Test@Automation.com"));
            Assert.IsTrue(user.Trust.Equals(0.0));          
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            user.Update("Test Updated", "Password Updated", "Username Updated", "Email@Updated", 1.0, null);

            Assert.IsTrue(user.Name.Equals("Test Updated"));
            Assert.IsTrue(user.Password.Equals("Password Updated"));
            Assert.IsTrue(user.Username.Equals("Username Updated"));
            Assert.IsTrue(user.Email.Equals("Email@Updated"));
            Assert.IsTrue(user.Trust.Equals(1.0));
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
        public void SetCoordinatesTest()
        {
            Coordinates result = new Coordinates(12.0,14.5);
            Assert.IsTrue(result.Latitude.Equals(12.0)&& result.Longitude.Equals(14.5));
        }
    }
}