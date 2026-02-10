using System.Net.Http.Headers;

namespace CityBikeDA;

public static class DataLoader
{
    public static List<BikeTrip> Load(string path)
    {
        var trips = new List<BikeTrip>();
        if (!File.Exists(path))
        {
            Console.WriteLine($"File {path} does not exist");
            return trips;
        }

        using (StreamReader reader = new StreamReader(path))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(',');

                if (values.Length >= 13)
                {
                    var trip = new BikeTrip(values);

                    trips.Add(trip);
                }
            }
            return trips;
        }
    }
}