using Microsoft.Extensions.Options;
using PayspaceTax.Web.Shared.Models.TaxCalculation;
using PayspaceTax.Web.Shared.Options;
using RestSharp;

namespace PayspaceTax.Web.Services;

public class TaxCalculationApiService : ApiService
{
    private const string BaseEndpoint = "api/TaxCalculation";
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public TaxCalculationApiService(IOptions<AppOptions> appOptions) : base(appOptions)
    {
    }
    
    public async Task<TaxCalculationViewModel?> CalculateTax(TaxCalculationViewModel model)
    {
        // Create request
        var request = new RestRequest($"{BaseEndpoint}/CalculateTax", Method.Post);
        
        // Add JSON body if needed
        request.AddJsonBody(model);

        // Execute the request
        var response = await RestClient.ExecuteAsync<TaxCalculationViewModel>(request);

        return response.Data;
    }
    
    public async Task<List<TaxCalculationHistoryViewModel>?> GetHistory()
    {
        // Create request
        var request = new RestRequest($"{BaseEndpoint}/History");
        
        // Execute the request
        var response = await RestClient.ExecuteAsync<List<TaxCalculationHistoryViewModel>>(request);

        return response.Data;
    }
}