using System;
using Microsoft.EntityFrameworkCore;

namespace ThirdService.Data;

public class DbInitializer
{
    public static void Init(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }

}
