using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;
using Taxually.TechnicalTest.Services;

namespace TechnicalTestUnitTests
{
    public class DEVatRegistrationProcessorTests
    {
        private DEVatRegistrationProcessor _processor;
        private Mock<ITaxuallyQueueClient> _queueClientMock;


        [SetUp]
        public void Setup()
        {
            _queueClientMock = new Mock<ITaxuallyQueueClient>();
            _processor = new DEVatRegistrationProcessor(new VatRegistrationRequest(), _queueClientMock.Object);
        }


        [Test]
        public async Task SaveDataToDestinationAsync_ShouldEnqueueData()
        {
            // Act
            await _processor.SaveDataToDestinationAsync();

            // Verify that EnqueueAsync method was called
            _queueClientMock.Verify(
                client => client.EnqueueAsync("vat-registration-xml", It.IsAny<string>()),
                Times.Once
            );
        }

    }
}
