using System.Xml.Serialization;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class DEVatRegistrationProcessor : VatRegistrationProcessorBase
    {
        public DEVatRegistrationProcessor(VatRegistrationRequest request) : base(request)
        {
        }

        public override async Task SaveDataToDestinationAsync()
        {
            // Germany requires an XML document to be uploaded to register for a VAT number
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, this.request);
                var xml = stringwriter.ToString();
                var xmlQueueClient = new TaxuallyQueueClient();
                // Queue xml doc to be processed
                await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }
    }
}
