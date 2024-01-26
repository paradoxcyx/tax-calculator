namespace PayspaceTax.Web.Shared.Models;

public class GenericResponseModel<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; }
}