using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvolentHealthContactManagement.Repository;
using EvolentHealthContactManagement.Models;
using EvolentHealthContactManagement.Controllers;
using System.Web.Http;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace EvolentHealthContactManagement.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        [TestMethod]
        public void GetContacts_should_return_NoTFound_when_getall_return_Zero_records()
        {
            // Arrange
            IEnumerable<ContactDTO> lstcon = new List<ContactDTO>();
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.GetAll()).Returns(lstcon);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetContacts();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetContacts_should_return_Ok_when_getall_return_data_records()
        {
            // Arrange
            IEnumerable<ContactDTO> lstcon = new List<ContactDTO>() { new ContactDTO() { FirstName = "Test" } };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.GetAll()).Returns(lstcon);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetContacts();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<IEnumerable<ContactDTO>>));
        }

        [TestMethod]
        public void GetContacts_should_return_Ok_with_responsebody_when_getall_return_data_records()
        {
            // Arrange
            IEnumerable<ContactDTO> lstcon = new List<ContactDTO>() { new ContactDTO() { ID = 1 } };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.GetAll()).Returns(lstcon);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetContacts();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<ContactDTO>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void GetContact_should_return_NoTFound_when_get_return_Null_records()
        {
            // Arrange
            ContactDTO dto = null;
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Get(It.IsAny<int>())).Returns(dto);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetContact(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetContact_should_return_Ok_when_get_return_data_records()
        {
            // Arrange
            ContactDTO dto = new ContactDTO();
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Get(It.IsAny<int>())).Returns(dto);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetContact(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<ContactDTO>));

        }

        [TestMethod]
        public void GetContact_should_return_Ok_with_responsebody_when_get_return_data_records()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1};
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Get(It.IsAny<int>())).Returns(dto);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.GetContact(1);
            var contentResult = actionResult as OkNegotiatedContentResult<ContactDTO>;

            // Assert

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.ID);
        }

        [TestMethod]
        public void AddContact_should_return_BadRequest_when_ID_is_non_zero()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1 };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContact(dto);
            
            // Assert

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
           
        }

        [TestMethod]
        public void AddContact_should_return_BadRequest_when_Add_method_return_false()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1 };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            int id;
            mockRepository.Setup(item => item.Add(It.IsAny<ContactDTO>(),out id)).Returns(false);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContact(dto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void AddContact_should_return_CreatedAtRoute_when_Add_method_return_True()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 0 };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            int id = 1;
            mockRepository.Setup(item => item.Add(It.IsAny<ContactDTO>(),out id)).Returns(true);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContact(dto);
            var response = actionResult as CreatedAtRouteNegotiatedContentResult<ContactDTO>;

            //Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Get", response.RouteName);
            Assert.AreEqual(id, response.RouteValues["Id"]);
        }

        [TestMethod]
        public void AddContacts_should_return_BadRequest_when_ID_is_non_zero_for_any_record_in_list()
        {
            // Arrange
            List<ContactDTO> lstdto = new List<ContactDTO>() { new ContactDTO() { ID = 1 } };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContacts(lstdto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void AddContacts_should_return_BadRequest_when_Status_value_is_inactive_for_any_record_in_list()
        {
            // Arrange
            List<ContactDTO> lstdto = new List<ContactDTO>() { new ContactDTO() { ID = 1, Status="Inactive" } };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContacts(lstdto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void AddContacts_should_return_BadRequest_when_AddRange_method_return_false()
        {
            // Arrange
            List<ContactDTO> lstdto = new List<ContactDTO>() { new ContactDTO() { FirstName = null,Status="Inactive" } };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.AddRange(It.IsAny<List<ContactDTO>>())).Returns(false);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContacts(lstdto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void AddContacts_should_return_OK_when_AddRange_method_return_True()
        {
            // Arrange
            List<ContactDTO> lstdto = new List<ContactDTO>() { new ContactDTO() { FirstName = null, Status = "active" } };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.AddRange(It.IsAny<List<ContactDTO>>())).Returns(true);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.AddContacts(lstdto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));

        }

        

        [TestMethod]
        public void UpdateContact_should_return_BadRequest_when_ID_parameter_value_ID_in_object_does_not_match()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 2 };
            var mockRepository = new Mock<IRepository<ContactDTO>>();

            var controller = new ContactController(mockRepository.Object);
            
            // Act
            IHttpActionResult actionResult = controller.UpdateContact(1, dto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void UpdateContact_should_return_BadRequest_when_user_tries_to_update_status_to_inactive()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1,Status = "Inactive" };
            var mockRepository = new Mock<IRepository<ContactDTO>>();

            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.UpdateContact(1, dto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void UpdateContact_should_return_Ok_when_update_method_return_true()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1, Status = "Active" };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Update(It.IsAny<ContactDTO>())).Returns(true);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.UpdateContact(1, dto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>));

        }

        [TestMethod]
        public void UpdateContact_should_return_NotFound_when_update_method_return_false()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1, Status="Active" };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Update(It.IsAny<ContactDTO>())).Returns(false);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.UpdateContact(1, dto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));

        }

        [TestMethod]
        public void UpdateContact_should_return_NotFound_when_update_method_throws_exception()
        {
            // Arrange
            ContactDTO dto = new ContactDTO() { ID = 1, Status = "Active" };
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Update(It.IsAny<ContactDTO>())).Throws(new System.Exception());
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.UpdateContact(1, dto);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));

        }

        [TestMethod]
        public void DeleteContact_should_return_NoTFound_when_Remove_return_false()
        {
            // Arrange
            ContactDTO con = new ContactDTO();
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Remove(It.IsAny<ContactDTO>())).Returns(false);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.DeleteContact(con);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteContact_should_return_Ok_when_Remove_return_true()
        {
            // Arrange
            ContactDTO con = new ContactDTO();
            var mockRepository = new Mock<IRepository<ContactDTO>>();
            mockRepository.Setup(item => item.Remove(It.IsAny<ContactDTO>())).Returns(true);
            var controller = new ContactController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.DeleteContact(con);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }
    }
}
