using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PriceComparer.Application;
using PriceComparer.Infrastucture;

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-bzg6zzl8.us.auth0.com/";
    options.Audience = "https://eastlevant.com";
});
builder.Services.AddCors(o => o.AddPolicy(name: "Default", 
                     policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PriceComparerContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddScoped<IOfferTypesService, OfferTypesService>();
builder.Services.AddScoped<IOffersService, OffersService>();


var app = builder.Build();
app.UseCors("Default");
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());
await using var scope = app.Services.CreateAsyncScope();
using var db = scope.ServiceProvider.GetService<PriceComparerContext>();
// small comment
await DoWithRetryAsync(async () => { await db.Database.MigrateAsync(); }, TimeSpan.FromSeconds(2), 10);
app.Run();

static async Task DoWithRetryAsync(Func<Task> action, TimeSpan sleepPeriod, int tryCount = 3)
{
    if (tryCount <= 0)
        throw new ArgumentOutOfRangeException(nameof(tryCount));

    while (true)
    {
        try
        {
            await action();
            return;
        }
        catch
        {
            if (--tryCount == 0)
                throw;
            await Task.Delay(sleepPeriod);
        }
    }
}

