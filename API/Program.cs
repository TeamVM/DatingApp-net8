using API.Data;
using API.ExceptionMiddleware;
using API.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
var app = builder.Build();
//app.UseDeveloperExceptionPage();
app.UseMiddleware<ExceptionMiddleware>();   
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().
    WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scoope = app.Services.CreateScope();
var services = scoope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedUsers(context);
}catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
    throw;
}

app.Run();
