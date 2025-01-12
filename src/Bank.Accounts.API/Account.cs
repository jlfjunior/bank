namespace Bank.Accounts.API;

public class Account
{
    public Guid Id { get; private set; }
    public string FullName { get; private set; }
    public TaxDocument TaxDocumentId { get; private set; }
    public ContactNumber Mobile { get; private set; }
    
    protected Account() { }
    
    private Account(string fullName, string taxDocumentId, string mobile)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        TaxDocumentId = TaxDocument.Create(taxDocumentId);
        Mobile = ContactNumber.Create(mobile);
    }

    public static Account Create(string fullName, string taxDocumentId, string mobile)
        => new Account(fullName, taxDocumentId, mobile);
}

public record AccountResponse
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }

    public AccountResponse(Account account)
    {
        Id = account.Id.ToString();
        FullName = account.FullName;
        TaxDocumentId = account.TaxDocumentId.Value;
        Mobile = account.Mobile.Value;
    }
}

public record AccountRequest
{
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }
}