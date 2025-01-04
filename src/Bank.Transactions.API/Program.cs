using Bank.Transactions.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<TransactionService>();

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

app.MapPost("/transactions", async (TransactionRequest request, TransactionService service) =>
    {
        TransactionResponse response = await service.CreateAsync(request);
        
        return response;
    })
    .WithName("CreateTransaction")
    .WithOpenApi();

app.Run();
