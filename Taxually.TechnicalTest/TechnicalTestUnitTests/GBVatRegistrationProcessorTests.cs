using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Controllers;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace TechnicalTestUnitTests
{
    public class GBVatRegistrationProcessorTests
    {
        private GBVatRegistrationProcessor _processor;
        private Mock<ITaxuallyHttpClient> _httpClientMock;
        private Mock<Microsoft.Extensions.Configuration.IConfiguration> _configMock;

        [SetUp]
        public void Setup()
        {
            _httpClientMock = new Mock<ITaxuallyHttpClient>();
            _configMock = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _processor = new GBVatRegistrationProcessor(new VatRegistrationRequest(), 
                _httpClientMock.Object, _configMock.Object);

            _configMock.Setup(x => x["GBVatRegistrationURL"]).Returns("https://api.uktax.gov.uk");

        }


        [Test]
        public async Task SaveDataToDestinationAsync_ShouldPostToHttpClientWithCorrectUrl()
        {
            
            //Act
            await _processor.SaveDataToDestinationAsync();

            //Assert
            _httpClientMock.Verify(
            client => client.PostAsync("https://api.uktax.gov.uk", It.IsAny<VatRegistrationRequest>()),
            Times.Once
        );
        }
    }
}
