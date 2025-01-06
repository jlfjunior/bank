using Microsoft.EntityFrameworkCore;

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

public record AccountCreatedEvent
{
    public Guid AccountId { get; set; }
    public string FullName { get; set; }
    public string TaxDocumentId { get; set; }
    public string Mobile { get; set; }
    public Guid EventId { get; set; }
    public DateTime Timestamp { get; set; }
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
            TaxDocumentId = TaxDocument.Create(request.TaxDocumentId),
            Mobile = request.Mobile
        };
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        
        AccountResponse response = new AccountResponse
        {
            Id = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId.Value,
            Mobile = account.Mobile,
        };
        
        //TODO: Dispatch AccountCreatedEvent
        AccountCreatedEvent accountCreated = new AccountCreatedEvent
        {
            AccountId = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId.Value,
            Mobile = account.Mobile,
            
            EventId = Guid.NewGuid(),
            Timestamp = DateTime.UtcNow
        };
        return response;
    }

    public async Task<List<AccountResponse>> GetAllAsync()
    {
        var accounts = await _context.Accounts
            .ToListAsync();

        var response = new List<AccountResponse>();
        
        foreach (var account in accounts)
        {
            var accountResponse = new AccountResponse
            {
                Id = account.Id,
                FullName = account.FullName,
                TaxDocumentId = account.TaxDocumentId.Value,
                Mobile = account.Mobile,
            };
            
            response.Add(accountResponse);
        }
        
        return response;
            
    }
}