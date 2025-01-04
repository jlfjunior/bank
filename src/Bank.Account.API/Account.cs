namespace Bank.Account.API;

public class Account
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
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

public class AccountService
{
    readonly Context _context;

    public AccountService(Context context)
    {
        _context = context;
    }

    public async Task<AccountResponse> CreateAsync(AccountRequest request)
    {
        Account account = new Account
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            TaxDocumentId = request.TaxDocumentId,
            Mobile = request.Mobile
        };
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        
        AccountResponse response = new AccountResponse
        {
            Id = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId,
            Mobile = account.Mobile,
        };
        
        //TODO: Dispatch AccountCreatedEvent
        
        return response;
    }
}