using CSharpFunctionalExtensions;
using Demo.Domain.Contracts;

namespace Demo.Domain;

public interface IAuthManager
{
    public Task<Result<UserRegistrationResponse, IError>> Registration(UserRegistrationRequest request);
    public Task<Result<UserLoginResponse, IError>> Login(UserLoginRequest request);
}