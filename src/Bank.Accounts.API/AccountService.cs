using Microsoft.EntityFrameworkCore;

namespace Bank.Accounts.API;

public class AccountService
{
    readonly Context _context;

    public AccountService(Context context)
    {
        _context = context;
    }

    public async Task<AccountResponse> CreateAsync(AccountRequest request)
    {
        var account = new Account
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            TaxDocumentId = TaxDocument.Create(request.TaxDocumentId),
            Mobile = ContactNumber.Create(request.Mobile)
        };
        
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        
        var response = new AccountResponse
        {
            Id = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId.Value,
            Mobile = account.Mobile.Value,
        };
        
        var accountCreated = new AccountCreatedEvent
        {
            AccountId = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId.Value,
            Mobile = account.Mobile.Value
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
                Mobile = account.Mobile.Value,
            };
            
            response.Add(accountResponse);
        }
        
        return response;
            
    }
}