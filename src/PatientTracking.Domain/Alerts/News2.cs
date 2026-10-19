namespace PatientTracking.Domain.Alerts;

public enum Severity { Normal, Warning, Critical }
public enum RiskLevel { Low, LowMedium, Medium, High }

public record VitalScores(int Pulse, int SpO2, int Systolic, int Temperature)
{
    public int Total => Pulse + SpO2 + Systolic + Temperature;
    public int Max => Math.Max(Math.Max(Pulse, SpO2), Math.Max(Systolic, Temperature));
}

public static class News2
{
    public static int ScorePulse(int bpm) => bpm switch
    {
        <= 40 => 3,
        <= 50 => 1,
        <= 90 => 0,
        <= 110 => 1,
        <= 130 => 2,
        _ => 3
    };

    public static int ScoreSpO2(int percent) => percent switch
    {
        <= 91 => 3,
        <= 93 => 2,
        <= 95 => 1,
        _ => 0
    };

    public static int ScoreSystolic(int mmHg) => mmHg switch
    {
        <= 90 => 3,
        <= 100 => 2,
        <= 110 => 1,
        <= 219 => 0,
        _ => 3
    };

    public static int ScoreTemperature(double celsius)
    {
        var t = Math.Round(celsius, 1);
        return t switch
        {
            <= 35.0 => 3,
            <= 36.0 => 1,
            <= 38.0 => 0,
            <= 39.0 => 1,
            _ => 2
        };
    }

    public static VitalScores Score(int heartRate, int spo2, int systolic, double temperature) =>
        new(ScorePulse(heartRate), ScoreSpO2(spo2), ScoreSystolic(systolic), ScoreTemperature(temperature));

    public static Severity ToSeverity(int score) => score switch
    {
        0 => Severity.Normal,
        <= 2 => Severity.Warning,
        _ => Severity.Critical
    };

    public static RiskLevel ToRisk(VitalScores s) =>
        s.Total >= 7 ? RiskLevel.High :
        s.Total >= 5 ? RiskLevel.Medium :
        s.Max == 3 ? RiskLevel.LowMedium :
        RiskLevel.Low;
}
