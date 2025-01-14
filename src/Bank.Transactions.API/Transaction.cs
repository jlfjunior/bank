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