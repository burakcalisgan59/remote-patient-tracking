using PatientTracking.Domain.Alerts;
using Xunit;

public class News2Tests
{
    [Theory]
    [InlineData(40, 3)]
    [InlineData(41, 1)]
    [InlineData(50, 1)]
    [InlineData(51, 0)]
    [InlineData(90, 0)]
    [InlineData(91, 1)]
    [InlineData(110, 1)]
    [InlineData(111, 2)]
    [InlineData(130, 2)]
    [InlineData(131, 3)]
    public void Pulse_boundaries(int bpm, int expected) =>
        Assert.Equal(expected, News2.ScorePulse(bpm));

    [Theory]
    [InlineData(91, 3)]
    [InlineData(92, 2)]
    [InlineData(94, 1)]
    [InlineData(96, 0)]
    public void SpO2_boundaries(int percent, int expected) =>
        Assert.Equal(expected, News2.ScoreSpO2(percent));

    [Theory]
    [InlineData(35.0, 3)]
    [InlineData(35.1, 1)]
    [InlineData(36.1, 0)]
    [InlineData(38.0, 0)]
    [InlineData(38.1, 1)]
    [InlineData(39.1, 2)]
    public void Temperature_boundaries(double celsius, int expected) =>
        Assert.Equal(expected, News2.ScoreTemperature(celsius));

    [Fact]
    public void Single_red_score_gives_low_medium_risk()
    {
        var s = News2.Score(heartRate: 75, spo2: 91, systolic: 120, temperature: 37.0);
        Assert.Equal(RiskLevel.LowMedium, News2.ToRisk(s));
    }
}
