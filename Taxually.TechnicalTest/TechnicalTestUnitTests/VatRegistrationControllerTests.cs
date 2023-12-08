using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace TechnicalTestUnitTests
{
    public class VatRegistrationControllerTests
    {

        private Mock<IVatRegistrationServiceFactory> _vatRegistrationServiceFactoryMock;
        private Mock<IVatRegistrationProcessor> _vatRegistrationProcessorMock;
        private VatRegistrationController _controller;


        [SetUp]
        public void Setup()
        {
            _vatRegistrationServiceFactoryMock = new Mock<IVatRegistrationServiceFactory>();
            _vatRegistrationProcessorMock = new Mock<IVatRegistrationProcessor>();
            _controller = new VatRegistrationController(_vatRegistrationServiceFactoryMock.Object);

        }


        [Test]
        public async Task PostReturnsOk_IfServicesWork()
        {
            //Arrange
            _vatRegistrationServiceFactoryMock.Setup(factory => 
                factory.CreateSuitableInterfaceForVRR(It.IsAny<VatRegistrationRequest>()))
                .Returns(_vatRegistrationProcessorMock.Object);
            var testRequest = new VatRegistrationRequest();

            //Act
            var result = await _controller.Post(testRequest);

            //Assert
            Assert.IsInstanceOf<OkResult>(result);
        }


        [Test]
        public async Task PostReturnsBadRequest_AndCorrectErrorMessage_IfServicesFail()
        {
            //Arrange
            _vatRegistrationProcessorMock.Setup(service => 
                service.SaveDataToDestinationAsync()).Throws(new Exception("Have a nice day!"));
            _vatRegistrationServiceFactoryMock.Setup(factory => 
                factory.CreateSuitableInterfaceForVRR(It.IsAny<VatRegistrationRequest>()))
                .Returns(_vatRegistrationProcessorMock.Object);
            var testRequest = new VatRegistrationRequest();

            //Act
            var result = await _controller.Post(testRequest);
            var badRequestResult = result as BadRequestObjectResult;
            var errorMessage = badRequestResult.Value as string;
            
            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.That(errorMessage, Is.EqualTo("Have a nice day!"));

        }
    }
}