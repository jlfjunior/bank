using Microsoft.EntityFrameworkCore;

namespace Bank.Transactions.API;

public class Context : DbContext
{
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Account> Accounts { get; set; }
    
    public Context(DbContextOptions<Context> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().HasKey(transaction => transaction.Id);
        modelBuilder.Entity<Account>().HasKey(account => account.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}