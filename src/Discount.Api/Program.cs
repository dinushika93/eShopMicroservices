using Discount.Api;
using Discount.Api.Extensions;
using Discount.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<DiscountDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"))
);

var app = builder.Build();

// Run Migrations
app.UseMigrations();

// Configure the HTTP request pipeline
app.MapGrpcService<DiscountService1>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

// Bind the server to port 8080 and ensure HTTP (not HTTPS) is used
app.Run();
