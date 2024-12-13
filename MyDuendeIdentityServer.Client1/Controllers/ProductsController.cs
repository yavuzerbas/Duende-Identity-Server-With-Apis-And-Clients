using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using MyDuendeIdentityServer.Shared.Constants;
using MyDuendeIdentityServer.Shared.Models;
using Newtonsoft.Json;

namespace MyDuendeIdentityServer.Client1.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IConfiguration _configuration;

		public ProductsController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<IActionResult> Index()
		{
			List<ProductDto> products = new List<ProductDto>();
			HttpClient client = new HttpClient();

			var disco = await client.GetDiscoveryDocumentAsync(UrlConstants.IdentityServerUrl);

			if (disco.IsError)
			{
				//log
			}

			ClientCredentialsTokenRequest request = new ClientCredentialsTokenRequest();

			request.ClientId = _configuration["Client:ClientId"];
			request.ClientSecret = _configuration["Client:ClientSecret"];
			request.Address = disco.TokenEndpoint;

			var token = await client.RequestClientCredentialsTokenAsync(request);

			if (token.IsError)
			{
				//log
			}

			client.SetBearerToken(token.AccessToken);
			Console.WriteLine($"token:{token.AccessToken}");
			var response = await client.GetAsync($"{UrlConstants.Api1Url}/api/products");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();

				products = JsonConvert.DeserializeObject<List<ProductDto>>(content);
				if (products == null)
				{
					//log
				}

			}

			else
			{
				//log
			}


			return View(products);
		}
	}
}
