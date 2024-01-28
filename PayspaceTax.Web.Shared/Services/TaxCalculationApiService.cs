using Microsoft.Extensions.Options;
using PayspaceTax.Web.Shared.Models;
using PayspaceTax.Web.Shared.Models.TaxCalculation;
using PayspaceTax.Web.Shared.Options;
using RestSharp;

namespace PayspaceTax.Web.Shared.Services;

public class TaxCalculationApiService : ApiService
{
    private const string BaseEndpoint = "api/TaxCalculation";
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public TaxCalculationApiService(IOptions<AppOptions> appOptions) : base(appOptions)
    {
    }
    
    public async Task<GenericResponseModel<TaxCalculationDataViewModel?>?> CalculateTax(TaxCalculationViewModel model)
    {
        // Create request
        var request = new RestRequest($"{BaseEndpoint}/CalculateTax", Method.Post);
        
        // Add JSON body if needed
        request.AddJsonBody(model.TaxData);

        // Execute the request
        var response = await RestClient.ExecuteAsync<GenericResponseModel<TaxCalculationDataViewModel?>>(request);

        return response.Data;
    }
    
    public async Task<GenericResponseModel<List<TaxCalculationHistoryViewModel?>>?> GetHistory()
    {
        // Create request
        var request = new RestRequest($"{BaseEndpoint}/History");
        
        // Execute the request
        var response = await RestClient.ExecuteAsync<GenericResponseModel<List<TaxCalculationHistoryViewModel?>>>(request);

        return response.Data;
    }
}