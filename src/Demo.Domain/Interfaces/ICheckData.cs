namespace Demo.Domain;

public interface ICheckData
{
    public Task<bool> CheckLogin(string login);
    public Task<bool> CheckPhoneNumber(string phoneNumber);
    public Task<bool> CheckEmail(string email);
}