using Basket.Api.Data;
using Carter;
using Common.Behaviors;
using Common.Exceptions.Handler;
using Discount.Api;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Messaging.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
// builder.Services.AddScoped<IBasketRepository>(provider => {
//     var repo = provider.GetRequiredService<BasketRepository>();
//     return new CachedBasketRepository(repo,provider.GetRequiredService<IDistributedCache>());
// });

builder.Services.Decorate<IBasketRepository,CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(opts =>{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<Cart>().Identity(x => x.UserName);
}).UseLightweightSessions();

//RabbitMQ message broker
builder.Services.AddMessageBroker(builder.Configuration);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database"))
    .AddRedis(builder.Configuration.GetConnectionString("Redis"));

//gRPC services
// builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(options =>
//  {
//     // Set the gRPC server URL
//     options.Address = new Uri(builder.Configuration["GrpcClientSettings:Endpoint"]);
// });

builder.Logging.AddConsole();  // Ensures logs go to stdout
builder.Logging.AddDebug();    // Enables Debug logs
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health", new HealthCheckOptions {
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseExceptionHandler(opt => {});

app.UseHttpsRedirection();

app.MapCarter();

app.Run();

