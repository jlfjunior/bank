namespace Bank.Transactions.API;

public class AccountService
{
    readonly Context _context;

    public AccountService(Context context)
    {
        _context = context;
    }

    public async Task<AccountResponse> CreateAccount(AccountRequest request)
    {
        var account = Account.Create(request.AccountId);
        _context.Accounts.Add(account);
        
        await _context.SaveChangesAsync();
        
        return new AccountResponse(account.Id, account.Balance);
    }

    public async Task<TransactionResponse> CreateAsync(TransactionRequest request)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Date = request.Date,
            CreatedAt = DateTime.UtcNow,
            Type = request.Type,
            Direction = request.Type == TransactionType.Deposit ? TransactionDirection.Credit : TransactionDirection.Debit,
            Amount = request.Amount,
            AccountId = request.AccountId
        };
        
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        var response = new TransactionResponse
        {
            Id = transaction.Id,
            Date = transaction.Date,
            Type = transaction.Type,
            Amount = transaction.Amount,
            AccountId = transaction.AccountId
        };
        
        //TODO: Dispatch TransactionCreatedEvent
        
        return response;
    }
}