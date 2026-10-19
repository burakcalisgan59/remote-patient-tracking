using System.Globalization;
using PatientTracking.Domain.Alerts;

while (true)
{
    Console.Write("Nabız SpO2 Sistolik Ateş (örn: 96 92 120 37.2, çıkış: q): ");
    var line = Console.ReadLine();
    if (line is null || line.Trim().ToLower() == "q") break;

    var p = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    if (p.Length != 4
        || !int.TryParse(p[0], out var hr)
        || !int.TryParse(p[1], out var spo2)
        || !int.TryParse(p[2], out var sys)
        || !double.TryParse(p[3].Replace(',', '.'), CultureInfo.InvariantCulture, out var temp))
    {
        Console.WriteLine("Hatalı giriş.");
        continue;
    }

    var s = News2.Score(hr, spo2, sys, temp);
    Console.WriteLine($"Nabız: {s.Pulse} ({News2.ToSeverity(s.Pulse)})");
    Console.WriteLine($"SpO2: {s.SpO2} ({News2.ToSeverity(s.SpO2)})");
    Console.WriteLine($"Sistolik: {s.Systolic} ({News2.ToSeverity(s.Systolic)})");
    Console.WriteLine($"Ateş: {s.Temperature} ({News2.ToSeverity(s.Temperature)})");
    Console.WriteLine($"Toplam: {s.Total}, risk: {News2.ToRisk(s)}\n");
}