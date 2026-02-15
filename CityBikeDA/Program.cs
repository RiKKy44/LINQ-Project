namespace CityBikeDA;


class Program
{
    static void Main()
    {
        var trips = DataLoader.Load("CityBikeFeb2020.csv");
        var weather = WeatherLoader.Load("NYC_weather.csv");
        var febWeather = weather
            .Where(w => w.Time.Year == 2020 && w.Time.Month == 2)
            .ToList();
        var longest = Analyze.LongestRide(trips);
        var shortest = Analyze.ShortestRide(trips);
        Console.WriteLine($"Longest: {(longest.EndedAt - longest.StartedAt).TotalMinutes:F0} min\n");
        Console.WriteLine($"Shortest: {(shortest.EndedAt - shortest.StartedAt).TotalMinutes:F0} min\n");
        
        Analyze.AnalyzePeakHoursByDayType(trips);
        Analyze.CalculateStationBalance(trips);
        Analyze.PrintTripDurations(trips);
        
        Analyze.PrintMostPopularRoute(trips);
        BikeWeatherAnalysis.RideDurationByWeather(trips, febWeather);
        BikeWeatherAnalysis.RidesByTemperature(trips,febWeather);
    }
}