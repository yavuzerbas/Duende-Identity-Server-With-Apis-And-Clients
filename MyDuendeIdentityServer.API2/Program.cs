using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyDuendeIdentityServer.API2.Constants;
using MyDuendeIdentityServer.API2.Models;
using MyDuendeIdentityServer.Shared.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
    {
        opts.Authority = UrlConstants.IdentityServerUrl;
        opts.Audience = ResourceConstants.ResourceApi2;

    });
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy(PicturePolicyConstants.ReadPicture, policy =>
    {
        policy.RequireClaim(ClaimConstants.Scope, ScopeConstants.Api2Read.ScopeName);
    });
    opts.AddPolicy(PicturePolicyConstants.UpdateOrCreatePicture, policy =>
    {
        policy.RequireClaim(ClaimConstants.Scope, ScopeConstants.Api2Write.ScopeName, ScopeConstants.Api2Update.ScopeName);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var pictureNames = new[]
{
    "Nature Picture", "Elephant Picture", "Lion Picture", "Mouse Picture", "Cat Picture"
};

app.MapGet("/api/pictures/getPictures", () =>
{
    var pictures = Enumerable.Range(1, 5).Select(index =>
        new Pircture
        (
            index,
            pictureNames[index - 1],
            pictureNames[index - 1].Replace(" ", "") + ".jpg"
        ))
        .ToArray();
    return pictures;
}).RequireAuthorization(PicturePolicyConstants.ReadPicture)
  .WithName("GetPictures")
  .WithOpenApi();

app.UseAuthentication();
app.UseAuthorization();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
