using Microsoft.EntityFrameworkCore;

namespace Bank.Accounts.API;

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
        modelBuilder.Entity<Account>().Property(account => account.TaxDocumentId)
            .HasConversion(
                taxDocument => taxDocument.Value,
                value => TaxDocument.Create(value));
        
        base.OnModelCreating(modelBuilder);
    }
}