using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WebLogin.IBLL;
using Moq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using WebLogin.Site.Models;
using System.Net;
using AutoMapper;
using System;

namespace WebLogin.Site.Controllers.API.Tests
{
    [TestClass()]
    public class UserControllerTests : IDisposable
    {
        public UserControllerTests()
        {
            AutoMapperConfig.RegisterMappings();
        }

        public void Dispose()
        {
            Mapper.Reset();
        }

        [TestMethod()]
        public void GetAllUsersTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();

            userModelMock.Setup(m => m.GetUsers()).Returns(new List<Objects.User>()
            {
                new Objects.User()
                {
                    UserName = "user1",
                    Roles = new List<Objects.Role>() { Objects.Role.PAGE_1 }
                },
                new Objects.User()
                {
                    UserName = "user2",
                    Roles = new List<Objects.Role>() { Objects.Role.PAGE_1, Objects.Role.PAGE_2 }
                },
            });
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<List<User>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
            Assert.AreEqual("user1", contentResult.Content[0].UserName);
            Assert.IsNull(contentResult.Content[0].Password);
            Assert.AreEqual("user2", contentResult.Content[1].UserName);
            Assert.IsNull(contentResult.Content[1].Password);
            Assert.AreEqual(2, contentResult.Content[1].Roles.Count);
        }

        [TestMethod()]
        public void GetUserTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();

            userModelMock.Setup(m => m.GetUser("user1")).Returns(
                new Objects.User()
                {
                    UserName = "user1",
                    Roles = new List<Objects.Role>() { Objects.Role.PAGE_1 }
                }
            );
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.Get("user1");
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("user1", contentResult.Content.UserName);
            Assert.IsNull(contentResult.Content.Password);
            Assert.AreEqual(Role.PAGE_1, contentResult.Content.Roles[0]);
        }

        [TestMethod()]
        public void GetUserNotFoundTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var actionResult = controller.Get("user1");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void PostCreateUserTest()
        {
            // Arrange
            var newUser = new User { UserName = "user1", Password = "Password", Roles = new List<Role>() { Role.PAGE_1, Role.PAGE_2 } };
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.CreateUser(It.IsAny<Objects.User>())).Returns(true);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Post(newUser);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual("user1", createdResult.RouteValues["id"]);
        }

        [TestMethod()]
        public void PostCreateUserAlreadyExistTest()
        {
            // Arrange
            var newUser = new User { UserName = "user1", Password = "Password", Roles = new List<Role>() { Role.PAGE_1, Role.PAGE_2 } };
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.UserExist("user1")).Returns(true);
            userModelMock.Setup(m => m.CreateUser(It.IsAny<Objects.User>())).Returns(false);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Post(newUser);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod()]
        public void PostCreateUserInternalErrorTest()
        {
            // Arrange
            var newUser = new User { UserName = "user1", Password = "Password", Roles = new List<Role>() { Role.PAGE_1, Role.PAGE_2 } };
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.CreateUser(It.IsAny<Objects.User>())).Returns(false);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Post(newUser);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod()]
        public void PutUpdateUserTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.UpdateUser("user1", It.IsAny<Objects.User>())).Returns(true);
            userModelMock.Setup(m => m.UserExist("user1")).Returns(true);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var user = new User { UserName = "user1", Password = "Password", Roles = new List<Role>() { Role.PAGE_1 } };

            // Act
            IHttpActionResult actionResult = controller.Put("user1", user);
            var contentResult = actionResult as NegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("user1", contentResult.Content.UserName);
            Assert.AreEqual("Password", contentResult.Content.Password);
            Assert.AreEqual(1, contentResult.Content.Roles.Count);
            Assert.AreEqual(Role.PAGE_1, contentResult.Content.Roles[0]);
        }


        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.UserExist("user1")).Returns(true);
            userModelMock.Setup(m => m.DeleteUser("user1")).Returns(true);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Delete("user1");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod()]
        public void DeleteNotFoundTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.UserExist("user1")).Returns(false);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Delete("user1");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void DeleteInternalErrorTest()
        {
            // Arrange
            var userModelMock = new Mock<IUserModel>();
            userModelMock.Setup(m => m.UserExist("user1")).Returns(true);
            userModelMock.Setup(m => m.DeleteUser("user1")).Returns(false);
            UserController controller = new UserController(userModelMock.Object);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult actionResult = controller.Delete("user1");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }
    }
}