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
        var account = Account.Create(request.FullName, request.TaxDocumentId, request.Mobile);
        
        _context.Accounts.Add(account);
        
        await _context.SaveChangesAsync();

        var response = new AccountResponse(account);
        
        var accountCreated = new AccountCreatedEvent
        {
            AccountId = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId.Value,
            Mobile = account.Mobile.Value
        };
        
        //TODO: Notify payment system to create a new account
        
        return response;
    }

    public async Task<List<AccountResponse>> GetAllAsync()
    {
        var accounts = await _context.Accounts
            .ToListAsync();

        var response = new List<AccountResponse>();
        
        foreach (var account in accounts)
            response.Add(new AccountResponse(account));
        
        return response;
    }
}