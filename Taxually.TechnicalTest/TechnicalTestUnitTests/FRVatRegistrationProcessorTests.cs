using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace TechnicalTestUnitTests
{
    public class FRVatRegistrationProcessorTests
    {
        private FRVatRegistrationProcessor _processor;
        private Mock<ITaxuallyQueueClient> _queueClientMock;

        [SetUp]
        public void Setup()
        {
            _queueClientMock = new Mock<ITaxuallyQueueClient>();
            
        }


        [Test]
        public async Task SaveDataToDestinationAsync_ShouldEnqueueCsvData()
        {
            // Arrange
            var request = new VatRegistrationRequest
            {
                CompanyName = "TestCompany",
                CompanyId = "123",
                Country = "FR"
            };

            _processor = new FRVatRegistrationProcessor(request, _queueClientMock.Object);

            // Act
            await _processor.SaveDataToDestinationAsync();

            // Assert
            // Verify that EnqueueAsync method was called with the correct arguments
            _queueClientMock.Verify(
                client => client.EnqueueAsync("vat-registration-csv", It.IsAny<byte[]>()),
                Times.Once
            );
        }

    }
}
