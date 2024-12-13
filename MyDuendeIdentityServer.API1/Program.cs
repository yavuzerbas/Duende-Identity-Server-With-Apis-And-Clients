using Microsoft.AspNetCore.Authentication.JwtBearer;
using MyDuendeIdentityServer.API1.Models;
using MyDuendeIdentityServer.API2.Constants;
using MyDuendeIdentityServer.Shared.Constants;
using MyDuendeIdentityServer.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
	{
		opts.Authority = UrlConstants.IdentityServerUrl;
		opts.Audience = ResourceConstants.ResourceApi1;
	});
builder.Services.AddAuthorization(opts =>
{
	opts.AddPolicy(ProductPolicyConstants.ReadProduct, policy =>
	{
		policy.RequireClaim(ClaimConstants.Scope, ScopeConstants.Api1Read.ScopeName);
	});

	opts.AddPolicy(ProductPolicyConstants.UpdateOrCreateProduct, policy =>
	{
		policy.RequireClaim(ClaimConstants.Scope, ScopeConstants.Api1Write.ScopeName, ScopeConstants.Api1Update.ScopeName);
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

var productNames = new[]
{
	"Book", "Pencil", "Pen", "Rubber", "Notebook"
};

var products = Enumerable.Range(1, 5).Select(index =>
		new ProductDto
		(
			index,
			productNames[index - 1],
			Random.Shared.Next(10, 50),
			Random.Shared.Next(10, 50)
		))
		.ToList();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/products", () =>
{
	return Results.Ok(products);
}).RequireAuthorization(ProductPolicyConstants.ReadProduct)
.WithName("GetProducts")
.WithOpenApi();

app.MapPost("/api/products", (CreateProduct createProduct) =>
{
	var id = products.Count;
	var product = new ProductDto(id, createProduct.Name, createProduct.Price, createProduct.Stock);
	products.Add(product);
	return Results.Ok($"Created the product with id:{id}");
}).RequireAuthorization(ProductPolicyConstants.UpdateOrCreateProduct)
.WithName("CreateProduct")
.WithOpenApi();

app.MapPut("/api/products", (int id, CreateProduct createProduct) =>
{
	var productIndex = products.FindIndex(x => x.Id == id);
	if (productIndex == -1)
	{
		return Results.NotFound();
	}

	products[productIndex] = new ProductDto(id, createProduct.Name, createProduct.Price, createProduct.Stock);

	return Results.Ok($"Updated the product with id:{id}");
}).RequireAuthorization(ProductPolicyConstants.UpdateOrCreateProduct)
.WithName("UpdateProduct")
.WithOpenApi();


app.Run();
