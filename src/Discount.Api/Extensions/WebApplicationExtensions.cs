using System;
using Microsoft.EntityFrameworkCore;

namespace Discount.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DiscountDbContext>();
            dbContext.Database.Migrate();  // Apply migrations automatically
        }

    }
}
