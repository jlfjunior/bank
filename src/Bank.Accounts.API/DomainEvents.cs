namespace Bank.Accounts.API;

public record DomainEvent
{
    public Guid EventId { get; protected set;  } = Guid.NewGuid();
    public DateTime Timestamp { get; protected set; } = DateTime.UtcNow;
}

public record AccountCreatedEvent : DomainEvent
{
    public Guid AccountId { get; set; }
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }
}

public record AccountUpdatedEvent : DomainEvent
{
    public Guid AccountId { get; set; }
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }
}