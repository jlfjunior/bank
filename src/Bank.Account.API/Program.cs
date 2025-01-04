using Bank.Account.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapPost("/accounts", (AccountRequest request) =>
    {
        Account account = new Account
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName,
            TaxDocumentId = request.TaxDocumentId,
            Mobile = request.Mobile
        };

        AccountResponse response = new AccountResponse
        {
            Id = account.Id,
            FullName = account.FullName,
            TaxDocumentId = account.TaxDocumentId,
            Mobile = account.Mobile,
        };
        
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