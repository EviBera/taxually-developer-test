using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Services
{
    public class VatRegistrationServiceFactory : IVatRegistrationServiceFactory
    {
        public IVatRegistrationProcessor CreateSuitableInterfaceForVRR(VatRegistrationRequest request)
        {
            switch(request.Country)
            {
                case "GB":
                    return new GBVatRegistrationProcessor(request);
                case "FR":
                    return new FRVatRegistrationProcessor(request);
                case "DE":
                    return new DEVatRegistrationProcessor(request);
                default:
                    throw new ArgumentException("Invalid argument");
            }
        }
    }
}
