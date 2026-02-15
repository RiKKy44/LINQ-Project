namespace CityBikeDA;

public static class BikeWeatherAnalysis
{
    public static void AnalyzeRideDurationByWeather(List<BikeTrip> trips, List<WeatherData> weather)
    {
        var combined = trips
            .Join(weather,
                trip => new { Date = trip.StartedAt.Date, Hour = trip.StartedAt.Hour },
                w => new { Date = w.Time.Date, Hour = w.Time.Hour },
                (trip, w) => new { trip, w })
            .GroupBy(x => x.w.Time.Date)
            .Select(g => new
            {
                Date = g.Key,
                AvgDuration = g.Average(x => (x.trip.EndedAt - x.trip.StartedAt).TotalMinutes),
                TripCount = g.Count(),
                AvgTemp = g.Average(x => x.w.Temperature),
                AvgRain = g.Average(x => x.w.Rain)
            })
            .OrderBy(x => x.Date);

        Console.WriteLine("\nRide Duration by Weather:");
        foreach (var day in combined)
            Console.WriteLine($"{day.Date:MM/dd} {day.AvgTemp:F0}C Rain:{day.AvgRain:F1}mm - {day.AvgDuration:F0}min, {day.TripCount} trips");
    }
    public static void AnalyzeRidesByTemperature(List<BikeTrip> trips, List<WeatherData> weather)
    {
        var tempRanges = trips
            .Join(weather,
                trip => new { Date = trip.StartedAt.Date, Hour = trip.StartedAt.Hour },
                w => new { Date = w.Time.Date, Hour = w.Time.Hour },
                (trip, w) => new { trip, w.Temperature })
            .GroupBy(x => x.Temperature switch { < 0 => "Freezing", < 10 => "Cold", < 20 => "Cool", < 30 => "Warm", _ => "Hot" })
            .Select(g => new { TempRange = g.Key, TripCount = g.Count(), AvgDuration = g.Average(x => (x.trip.EndedAt - x.trip.StartedAt).TotalMinutes) })
            .OrderBy(x => x.TripCount);

        Console.WriteLine("\nRides by Temperature:");
        foreach (var range in tempRanges)
            Console.WriteLine($"{range.TempRange,-10} {range.TripCount,6} trips, {range.AvgDuration:F0}min avg");
    }
}