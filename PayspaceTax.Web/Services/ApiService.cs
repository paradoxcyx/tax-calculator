using Microsoft.Extensions.Options;
using PayspaceTax.Web.Options;
using PayspaceTax.Web.Shared.Models;
using RestSharp;

namespace PayspaceTax.Web.Services;

public class ApiService(AppSettings appSettings)
{
    private readonly RestClient _restClient = new("https://localhost:44363");

    public async Task<TaxCalculationModel?> CalculateTax(TaxCalculationModel model)
    {
        // Create request
        var request = new RestRequest("api/TaxCalculator/CalculateTax", Method.Post);
        
        // Add JSON body if needed
        request.AddJsonBody(model);

        // Execute the request
        var response = await _restClient.ExecuteAsync<TaxCalculationModel>(request);

        return response.Data;
    }
}