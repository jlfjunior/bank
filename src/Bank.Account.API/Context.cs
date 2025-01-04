using Microsoft.EntityFrameworkCore;

namespace Bank.Account.API;

public class Context : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasKey(account => account.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}