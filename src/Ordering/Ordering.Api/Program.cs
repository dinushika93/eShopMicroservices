using Ordering.Infra.Extensions;
using Ordering.Infrastructure.Data.Extensions;
using  Ordering.App.Extensions;
using Ordering.Api.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
.AddApplicationServices(builder.Configuration)
.AddInfrastructureServices(builder.Configuration)
.AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
      await app.Services.InitialiseDatabaseAsync();
}

app.Run();

