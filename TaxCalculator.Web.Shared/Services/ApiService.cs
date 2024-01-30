using Microsoft.Extensions.Options;
using RestSharp;
using TaxCalculator.Web.Shared.Options;

namespace TaxCalculator.Web.Shared.Services;

public class ApiService
{
    protected readonly RestClient RestClient;
    
    // ReSharper disable once ConvertToPrimaryConstructor
    protected ApiService(IOptions<AppOptions> appOptions)
    {
        RestClient = new RestClient(appOptions.Value.ServerUrl);
    }
}