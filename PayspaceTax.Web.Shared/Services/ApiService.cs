using Microsoft.Extensions.Options;
using PayspaceTax.Web.Shared.Options;
using RestSharp;

namespace PayspaceTax.Web.Shared.Services;

public class ApiService
{
    protected readonly RestClient RestClient;
    
    // ReSharper disable once ConvertToPrimaryConstructor
    protected ApiService(IOptions<AppOptions> appOptions)
    {
        RestClient = new RestClient(appOptions.Value.ServerUrl);
    }
}