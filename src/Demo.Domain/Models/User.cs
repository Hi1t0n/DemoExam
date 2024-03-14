namespace Demo.Domain.Models;

public class User
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Fullname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public Guid RoleId { get; set; }
    public Role Role { get; set; }
    public List<Statement> Statements = new List<Statement>();
}