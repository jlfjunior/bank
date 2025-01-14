using Bank.Transactions.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AccountService>();

builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("TransactionsInMemoryDatabase"));

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

app.MapPost("accounts", async (AccountRequest request, AccountService service) =>
    {
        var response = await service.CreateAccount(request);

        return response;
    })
    .WithName("CreateAccount")
    .WithOpenApi();


app.MapPost("/transactions", async (TransactionRequest request, AccountService service) =>
    {
        TransactionResponse response = await service.CreateAsync(request);
        
        return response;
    })
    .WithName("CreateTransaction")
    .WithOpenApi();

app.Run();
