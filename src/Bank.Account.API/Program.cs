using Bank.Account.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AccountService>();

builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("InMemoryDatabase"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/accounts", async (AccountRequest request, AccountService service) =>
    {
        var response = await service.CreateAsync(request);
        
        return response;
    })
    .WithName("CreateAccount")
    .WithOpenApi();

app.MapGet("/accounts", () =>
    {
        var accounts = new List<AccountResponse>
        { 
            new AccountResponse 
            {
                Id = Guid.NewGuid(),
                FullName = "John Doe",
                TaxDocumentId = "0123456789",
                Mobile = "79912345678",
            }
        };
        
        return accounts;
    })
    .WithName("ListAccounts")
    .WithOpenApi();

app.Run();