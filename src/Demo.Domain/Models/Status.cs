namespace Demo.Domain.Models;

public class Status
{
    public Guid StatusId { get; set; }
    public string StatusName { get; set; }
    public List<Statement> Statements = new List<Statement>();
}