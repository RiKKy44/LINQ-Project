namespace CityBikeDA;

public record WeatherData
{
    public DateTime Date { get; init; }
    public double TemperatureMax { get; init; }
    public double TemperatureMin { get; init; }
    public double TemperatureMean { get; init; }
    public double PrecipitationSum { get; init; }
    public double WindSpeedMax { get; init; }
}