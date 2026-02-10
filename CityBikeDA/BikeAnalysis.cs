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
}