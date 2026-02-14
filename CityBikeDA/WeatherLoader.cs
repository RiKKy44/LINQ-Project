using System.Globalization;
namespace CityBikeDA;

public static class WeatherLoader
{
    public static List<WeatherData> Load(string path)
    {
        var weather = new List<WeatherData>();

        if (!File.Exists(path))
        {
            Console.WriteLine($"File {path} does not exist");
            return weather;
        }

        using (StreamReader reader = new StreamReader(path))
        {
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                var values = line.Split(',');

                if (values.Length >= 10)
                {
                    weather.Add(new WeatherData
                    {
                        Time = DateTime.Parse(values[0]),
                        Temperature = ParseDouble(values[1]),
                        Precipitation = ParseDouble(values[2]),
                        Rain = ParseDouble(values[3]),
                        CloudCover = ParseDouble(values[4]),
                        WindSpeed = ParseDouble(values[8]),
                        WindDirection = ParseDouble(values[9])
                    });
                }
            }
        }
        return weather;
    }
    private static double ParseDouble(string s)
    {

        if (string.IsNullOrEmpty(s))
        {
            return 0.0;
        }
        return double.Parse(s,CultureInfo.InvariantCulture);
    }
}