// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Pandemic.Tracker.Web.Models;

public record ExternalCountry(long Updated, int Cases, int TodayCases, int Deaths, int TodayDeaths, int Recovered, int TodayRecovered, int Active, int Critical, int CasesPerOneMillion, int DeathsPerOneMillion, int Tests, int TestsPerOneMillion, int Population, int OneCasePerPeople, int OneDeathPerPeople, int OneTestPerPeople, double ActivePerOneMillion, double RecoveredPerOneMillion, double CriticalPerOneMillion)
{
    public string Country { get; set; } = default!;
    public CountryInfo CountryInfo { get; set; } = default!;
    public string Continent { get; set; } = default!;
}