namespace CityBikeDA;

public static class Analyze
{
    public static BikeTrip LongestRide(List<BikeTrip> trips)
    {
        return trips.MaxBy(t => (t.EndedAt - t.StartedAt).TotalMinutes);
    }

    public static BikeTrip ShortestRide(List<BikeTrip> trips)
    {
        return trips.MinBy(t => (t.EndedAt - t.StartedAt).TotalMinutes);
    }

    public static void PrintMostPopularRoute(List<BikeTrip> trips)
    {
        var groups = trips.GroupBy(t =>
        new {
            t.StartStationName, t.EndStationName
        });

        var mostPopular = groups.MaxBy(g => g.Count());

        if (mostPopular != null)
        {
            Console.WriteLine($"Most popular route is: {mostPopular.Key.StartStationName} -> {mostPopular.Key.EndStationName}");
            Console.WriteLine($"Number of routes is: {mostPopular.Count()}");
        }
    }

    public static void CalculateStationBalance(List<BikeTrip> trips)
    {
        var departures = trips.Select(t => new { Station = t.StartStationName, Change = -1 });
        
        var arrivals = trips.Select(t => new { Station = t.EndStationName, Change = 1 });

        var allTrips = departures.Concat(arrivals);

        var stationStats = allTrips
            .GroupBy(t => t.Station)
            .Select(g =>
                new
                {
                    StationName = g.Key,
                    Balance = g.Sum(t => t.Change)
                })
            .OrderBy(t => t.Balance)
            .ToList();
        foreach (var stat in stationStats.Take(5))
        {
            Console.WriteLine($"{stat.StationName} : {stat.Balance}");
        }
    }

    public static void AnalyzePeakHoursByDayType(List<BikeTrip> trips)
    {
        var dayTypeStats = trips
            .Select(t => new
            {
                Hour = t.StartedAt.Hour,
                DayType = (t.StartedAt.DayOfWeek == DayOfWeek.Saturday || t.StartedAt.DayOfWeek == DayOfWeek.Sunday)
                    ? "Weekend"
                    : "Weekday"
            })
            .GroupBy(x => new { x.Hour, x.DayType })
            .Select(g => new
            {
                Hour = g.Key.Hour,
                DayType = g.Key.DayType,
                TripCount = g.Count()
            })
            .OrderBy(x => x.DayType)
            .ThenBy(x => x.Hour);
        
        var weekdayPeaks = dayTypeStats.Where(x => x.DayType == "Weekday").OrderByDescending(x => x.TripCount).Take(5);
        var weekendPeaks = dayTypeStats.Where(x => x.DayType == "Weekend").OrderByDescending(x => x.TripCount).Take(5);
        Console.WriteLine("\nTop 5 Weekday Peak Hours:");
        foreach (var peak in weekdayPeaks)
        {
            Console.WriteLine($"{peak.Hour:D2}:00 - {peak.TripCount} trips");
        }
        Console.WriteLine("\nTop 5 Weekend Peak Hours:");
        foreach (var peak in weekendPeaks)
        {
            Console.WriteLine($"  {peak.Hour:D2}:00 - {peak.TripCount} trips");
        }
    }

    public static void PrintTripDurations(List<BikeTrip> trips)
    {
        var durationRanges = trips
            .Select(t => (t.EndedAt - t.StartedAt).TotalMinutes)
            .GroupBy(duration => duration switch
            {
                < 5 => "0-5 min",
                < 10 => "5-10 min",
                < 15 => "10-15 min",
                < 20 => "15-20 min",
                < 30 => "20-30 min",
                < 45 => "30-45 min",
                < 60 => "45-60 min",
                _ => "60+ min"
            })
            .Select(g => new
            {
                Range = g.Key,
                Count = g.Count(),
                Percentage = (g.Count() * 100.0) / trips.Count
            })
            .OrderBy(x => x.Range);
        
        Console.WriteLine("\nTrip duration distribution: ");
        foreach (var range in durationRanges)
        {
            Console.WriteLine($"{range.Range} | {range.Count} trips ({range.Percentage:F1}%)");
        }
        
    }
}