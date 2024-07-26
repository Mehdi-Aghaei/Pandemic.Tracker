// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------

using Bunit;
using FluentAssertions;
using Pandemic.Tracker.Web.Components.Pages;

namespace Pandemic.Tracker.Web.Unit.Tests.Components.Pages;

public class ChartListTests : TestContext
{
    [Fact]
    public void ChartList_ShouldRenderCorrectly()
    {
        // Act
        var unitUnderTest = RenderComponent<ChartList>();

        // Assert
        unitUnderTest.Find("h1").MarkupMatches("<h1 class=\"mb-4\">Charts</h1>");

        var links = unitUnderTest.FindAll("a.list-group-item");
        links.Count.Should().Be(5);

        links[0].MarkupMatches("<a href=\"/bar-chart\" class=\"list-group-item list-group-item-action bg-primary text-white mb-2\">Bar Chart: Total Cases, Deaths, and Recovered</a>");
        links[1].MarkupMatches("<a href=\"/line-chart\" class=\"list-group-item list-group-item-action bg-success text-white mb-2\">Line Chart: Cases, Deaths, and Recovered Over Time</a>");
        links[2].MarkupMatches("<a href=\"/pie-chart\" class=\"list-group-item list-group-item-action bg-info text-white mb-2\">Pie Chart: Countries recovered per one million </a>");
        links[3].MarkupMatches("<a href=\"/heat-map\" class=\"list-group-item list-group-item-action bg-warning text-dark mb-2\">Heat Map: Cases per One Million</a>");
        links[4].MarkupMatches("<a href=\"/scatter-plot\" class=\"list-group-item list-group-item-action bg-danger text-white mb-2\">Scatter Plot: Cases vs. Population</a>");
    }
}