// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using System.Text.Json;
using System.Text.Json.Serialization;
using Pandemic.Tracker.Web.Models;

namespace Pandemic.Tracker.Web.Services;

public class CountryService : ICountryService
{
	private const string APIUrl = "https://disease.sh/v3/covid-19/countries";

	private static readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "CountriesData.json");

	private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

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

	public List<Country> GetCountriesFromOriginalSource()
	{
		try
		{
			string jsonResponse = _httpClient.GetStringAsync(APIUrl).GetAwaiter().GetResult();

			var externalCountries = JsonSerializer.Deserialize<List<ExternalCountry>>(jsonResponse, _jsonSerializerOptions);
			if (externalCountries is null)
			{
				_logger.LogError("Countries not fetched successfully");

				return [];
			}

			var countries = externalCountries.Select(c => new Country
			{
				Name = c.Country,
				Cases = c.Cases,
				Deaths = c.Deaths,
				Recovered = c.Recovered,
				Population = c.Population,
				CasesPerOneMillion = c.CasesPerOneMillion,
				DeathsPerOneMillion = c.DeathsPerOneMillion,
				RecoveredPerOneMillion = c.RecoveredPerOneMillion,
				CriticalPerOneMillion = c.CriticalPerOneMillion,
				Continent = c.Continent,
				TodayCases = c.TodayCases,
				TodayDeaths = c.TodayDeaths,
				TodayRecovered = c.TodayRecovered,
				CreatedDate = GenerateRandomDateInPastMonths(6),
				UpdatedDate = DateTimeOffset.FromUnixTimeMilliseconds(c.Updated),
			}).ToArray();

			// overwrite to CountriesData.
			var jsonData = JsonSerializer.Serialize(countries, _jsonSerializerOptions);
			File.WriteAllTextAsync(_filePath, jsonData).GetAwaiter().GetResult();

			_logger.LogInformation("Countries  updated successfully");

			return [.. countries];
		}
		catch (Exception ex)
		{
			Console.WriteLine($"An error occurred: {ex.Message}");
			return [];
		}
	}

	public List<Country> GetCountriesStatic()
	{
		var jsonData = File.ReadAllText(_filePath);

		var countries = JsonSerializer.Deserialize<List<Country>>(jsonData, _jsonSerializerOptions);

		return countries!;
	}

	private static DateTime GenerateRandomDateInPastMonths(int months)
	{
		// This method is use to generate a random date in the past months.
		// because there is not much change in the data of the countries.

		var random = new Random();

		DateTime endDate = DateTime.Now;
		DateTime startDate = endDate.AddMonths(-months);

		int range = (endDate - startDate).Days;
		int randomDays = random.Next(range);

		return startDate.AddDays(randomDays);
	}
}