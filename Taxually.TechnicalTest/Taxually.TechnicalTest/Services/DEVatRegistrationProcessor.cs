using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class DEVatRegistrationProcessor : VatRegistrationProcessorBase
    {
        private readonly ITaxuallyQueueClient _xmlQueueClient;
        public DEVatRegistrationProcessor(VatRegistrationRequest request,
            ITaxuallyQueueClient taxuallyQueueClient) : base(request)
        {
            _xmlQueueClient = taxuallyQueueClient;
        }

        public override async Task SaveDataToDestinationAsync()
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, this.request);
                var xml = stringwriter.ToString();
                // Queue xml doc to be processed
                await _xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }
    }
}
