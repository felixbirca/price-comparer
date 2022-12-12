global using FastEndpoints;
global using FluentValidation;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using PriceComparer.Application;
using PriceComparer.Infrastucture;
using PriceComparer.Middleware;

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
app.Run();
