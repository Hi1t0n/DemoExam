using Demo.Domain;

namespace Demo.Infrastructure.ErrorObject;

public class NotFoundError : IError
{
    public string ErrorMessage { get; }

    public NotFoundError(string errorMessange)
    {
        ErrorMessage = errorMessange;
    }
}