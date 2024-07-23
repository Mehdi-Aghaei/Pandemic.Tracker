// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Pandemic.Tracker.Web.Models;
using Pandemic.Tracker.Web.Services;

namespace Pandemic.Tracker.Web.Unit.Tests.Services;
public sealed class CountryServiceTests
{
	[Fact]
	public void GetCountriesAsync_ShouldReturnCSountries_WhenApiCallIsSuccessful()
	{
		// Arrange
		var httpClient = new HttpClient(new MockHttpMessageHandler());
		var configuration = Substitute.For<IConfiguration>();
		var logger = Substitute.For<ILogger<CountryService>>();

		configuration["APIUrl"].Returns("https://example.com/");

		var service = new CountryService(httpClient, configuration, logger);

		// Act
		var countries = service.GetCountries();

		// Assert
		countries.Should().NotBeNullOrEmpty();
		countries.Should().ContainSingle(c => c.Name == "Country1");
	}

	[Fact]
	public void GetCountriesAsync_ShouldReturnEmptyArray_WhenApiCallFails()
	{
		// Arrange
		var httpClient = new HttpClient(new MockHttpMessageHandler(HttpStatusCode.InternalServerError));
		var configuration = Substitute.For<IConfiguration>();
		var logger = Substitute.For<ILogger<CountryService>>();

		configuration["APIUrl"].Returns("https://example.com/");

		var service = new CountryService(httpClient, configuration, logger);

		// Act
		var countries = service.GetCountries();

		// Assert
		countries.Should().BeEmpty();
		logger.Received().LogError(Arg.Any<HttpRequestException>(), "An error occurred while fetching countries");
	}

	private class MockHttpMessageHandler : HttpMessageHandler
	{
		private readonly HttpStatusCode _statusCode;

		public MockHttpMessageHandler(HttpStatusCode statusCode = HttpStatusCode.OK)
		{
			_statusCode = statusCode;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			if (_statusCode == HttpStatusCode.OK)
			{
				var countries = new[]
				{
					new Country { Name = "Country1" }
				};

				return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
				{
					Content = JsonContent.Create(countries)
				});
			}

			return Task.FromResult(new HttpResponseMessage(_statusCode));
		}
	}
}