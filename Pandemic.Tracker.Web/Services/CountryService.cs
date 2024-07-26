// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json;
using Pandemic.Tracker.Web.Models;

namespace Pandemic.Tracker.Web.Services;

public class CountryService : ICountryService
{
	private readonly HttpClient _httpClient;

	private readonly IConfiguration _configuration;

	private readonly ILogger<CountryService> _logger;

	public CountryService(HttpClient httpClient, IConfiguration configuration, ILogger<CountryService> logger)
	{
		_httpClient = httpClient;
		_configuration = configuration;
		_httpClient.BaseAddress = new Uri(_configuration["APIUrl"]!);
		_logger = logger;
	}

	public List<Country> GetCountries()
	{
		var countries = _httpClient.GetFromJsonAsync<Country[]>("api/countries").GetAwaiter().GetResult();

		_logger.LogInformation("Countries fetched successfully");
		return [.. countries];
	}

	public List<Country> GetCountriesStatic()
	{
		var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "CountriesData.json");

		var jsonData = File.ReadAllText(filePath);

		var countries = JsonSerializer.Deserialize<List<Country>>(jsonData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

		return countries!;
	}
}