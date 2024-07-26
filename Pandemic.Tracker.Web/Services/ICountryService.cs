// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Pandemic.Tracker.Web.Models;

namespace Pandemic.Tracker.Web.Services;

public interface ICountryService
{
    List<Country> GetCountries();
    List<Country> GetCountriesStatic();
}