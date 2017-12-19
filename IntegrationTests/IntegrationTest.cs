using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;


namespace IntegrationTests
{
    [TestClass]
    public class IntegrationTest : BaseIntegrationTest
    {
        [TestMethod]
        public void WhenCreatingAUser_TheUserShouldBeProperlySaved()
        {
           /*
            RunOnDatabase(sut =>
            {
                //Arrange
                var repository = new UserRepository(sut);
                var user = User.Create("Nume");

                //Act
                repository.CreateUser(user);

                //Assert
                var users = repository.GetAllUsers();
                Assert.AreEqual(1, users.Count);

            });
            */
        }

        [TestMethod]
        public void WhenCreating2User_TheUserShouldBeProperlySaved()
        {
              /*
            RunOnDatabase(sut => {
                //Arrange
                var repository = new UserRepository(sut);
                var user = User.Create("Nume");
                var user2 = User.Create("Nume2");
                //Act
                repository.CreateUser(user);
                repository.CreateUser(user2);
                //Assert
                var users = repository.GetAllUsers();
                Assert.AreEqual(2, users.Count);



            });
                */
        }
    }
}
