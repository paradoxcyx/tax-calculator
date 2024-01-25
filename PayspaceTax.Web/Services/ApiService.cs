using Microsoft.Extensions.Options;
using PayspaceTax.Web.Options;
using PayspaceTax.Web.Shared.Models.TaxCalculator;
using RestSharp;

namespace PayspaceTax.Web.Services;

public class ApiService(IOptions<AppOptions> appOptions) : IApiService
{
    private readonly RestClient _restClient = new(appOptions.Value.ServerUrl);

    public async Task<TaxCalculationViewModel?> CalculateTax(TaxCalculationViewModel model)
    {
        // Create request
        var request = new RestRequest("api/TaxCalculator/CalculateTax", Method.Post);
        
        // Add JSON body if needed
        request.AddJsonBody(model);

        // Execute the request
        var response = await _restClient.ExecuteAsync<TaxCalculationViewModel>(request);

        return response.Data;
    }
}