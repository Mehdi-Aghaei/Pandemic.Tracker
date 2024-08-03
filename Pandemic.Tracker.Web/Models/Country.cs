// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Pandemic.Tracker.Web.Models;

public class Country
{
	public string Name { get; set; } = default!;
	public string Iso3 { get; set; } = default!;
	public string Continent { get; set; } = default!;
	public int Cases { get; set; }
	public int TodayCases { get; set; }
	public int Deaths { get; set; }
	public int TodayDeaths { get; set; }
	public int Recovered { get; set; }
	public int TodayRecovered { get; set; }
	public int Population { get; set; }
	public int CasesPerOneMillion { get; set; }
	public int DeathsPerOneMillion { get; set; }
	public double RecoveredPerOneMillion { get; set; }
	public double CriticalPerOneMillion { get; set; }
	public DateTimeOffset CreatedDate { get; set; }
	public DateTimeOffset UpdatedDate { get; set; }
}