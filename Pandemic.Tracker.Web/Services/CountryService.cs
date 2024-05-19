// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Pandemic.Tracker.Web.Models;

namespace Pandemic.Tracker.Web.Services;

public class CountryService
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

	public async Task<Country[]> GetCountriesAsync()
	{
		var countries = await _httpClient.GetFromJsonAsync<Country[]>("api/countries");

		_logger.LogInformation("Countries fetched successfully");
		return [.. countries];
	}
}