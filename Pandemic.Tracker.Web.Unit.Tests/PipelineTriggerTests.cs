using FluentAssertions;

namespace Pandemic.Tracker.Web.Unit.Tests;

public class PipelineTriggerTests
{
	[Fact]
	public void ShouldCalculateTheSum()
	{
		// Arrange
		int a = 5;
		int b = 2;

		// Act
		var result = Sum(a, b);
		
		// Assert
		result.Should().Be(7);
	}

	private static int Sum(int x, int y) => x + y;
}