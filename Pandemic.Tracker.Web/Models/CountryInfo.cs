// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

namespace Pandemic.Tracker.Web.Models;

public record CountryInfo(int Id, double Lat, double Long)
{
    public string Iso2 { get; set; } = default!;
    public string Iso3 { get; set; } = default!;
    public string Flag { get; set; } = default!;
}