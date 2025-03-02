using Catalog.Api.Data;
using Catalog.Api.Repositories;
using Common.Behaviors;
using Common.Exceptions.Handler;
using FluentValidation;
using HealthChecks.MongoDb;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CatalogDbSettings>(builder.Configuration.GetSection("Catalogdb"));
builder.Services.AddScoped<ICatalogRepository,CatalogRepository>();
builder.Services.AddSingleton<CatalogdbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(config => 
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
        .AddMongoDb(mongodbConnectionString: builder.Configuration.GetSection("Catalogdb:ConnectionString").Value,
        name: "mongo", 
        failureStatus: HealthStatus.Unhealthy); //adding MongoDb Health Checks



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthChecks("/health", new HealthCheckOptions {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapControllers();

app.UseExceptionHandler(options => { });

app.Run();
