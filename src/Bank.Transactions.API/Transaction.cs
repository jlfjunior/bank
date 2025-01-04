namespace Bank.Transactions.API;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public TransactionDirection Direction { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public record TransactionResponse
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
}

public record TransactionRequest
{
    public Guid AccountId { get; set; }
    public DateOnly Date { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
}

public enum TransactionDirection
{
    Credit = 10,
    Debit = 20
}

public enum TransactionType
{
    Deposit = 10,
    Withdraw = 20,
    Transfer = 30,
    Payment = 40
}


public class TransactionService
{
    readonly Context _context;

    public TransactionService(Context context)
    {
        _context = context;
    }

    public async Task<TransactionResponse> CreateAsync(TransactionRequest request)
    {
        Transaction transaction = new Transaction
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

        TransactionResponse response = new TransactionResponse
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