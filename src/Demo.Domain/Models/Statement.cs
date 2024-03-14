namespace Demo.Domain.Models;

public class Statement
{
    public Guid StatementId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string CarNumber { get; set; }
    public string Description { get; set; }
    public Guid StatusId { get; set; }
    public Status Status { get; set; }
}