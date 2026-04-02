using Azure.Identity;
using MyPaymentService.Models;
using MyPaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// Azure App Configuration + Key Vault
if (!builder.Environment.IsDevelopment() && !string.IsNullOrEmpty(builder.Configuration["AppConfig:Endpoint"]))
{
    builder.Configuration.AddAzureAppConfiguration(options =>
    {
        options.Connect(builder.Configuration["AppConfig:Endpoint"])
               .ConfigureKeyVault(kv =>
               {
                   kv.SetCredential(new DefaultAzureCredential());
               });
    });
}

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IPaymentService, PaymentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Minimal API endpoint
app.MapPost("/api/payment", async (PaymentRequest request, IPaymentService paymentService, ILogger<Program> logger) =>
{
    logger.LogInformation("Processing payment for {User}", request.UserId);

    var result = await paymentService.ProcessPaymentAsync(request);

    return Results.Ok(result);
})
.WithName("ProcessPayment");

app.Run();