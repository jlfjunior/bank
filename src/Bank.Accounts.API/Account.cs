namespace Bank.Accounts.API;

public class Account
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public TaxDocument TaxDocumentId { get; set; }
    public string Mobile { get; set; }
}

public record AccountResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }
}

public record AccountRequest
{
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }
}