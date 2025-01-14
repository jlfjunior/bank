namespace Bank.Transactions.API;

public class Account
{
    public Guid Id { get; private set; }
    public decimal Balance { get; private set; }
    
    protected Account() { }

    private Account(Guid id)
    {
        Id = id;
    }

    public static Account Create(Guid accountId) 
        => new Account(accountId);
}

public record AccountRequest(Guid AccountId);
public record AccountResponse(Guid Id, decimal Balance);