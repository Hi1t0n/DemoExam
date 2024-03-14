using Demo.Domain;

namespace Demo.Infrastructure.ErrorObject;

public class BadRequestError : IError
{
    public string ErrorMessage { get; }

    public BadRequestError(string errorMessange)
    {
        ErrorMessage = errorMessange;
    }
}