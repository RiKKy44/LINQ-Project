namespace CityBikeDA;

public record WeatherData
{
    public DateTime Time { get; init; }
    public double Temperature { get; init; }
    public double Precipitation { get; init; }
    public double Rain { get; init; }
    public double CloudCover { get; init; }
    public double WindSpeed { get; init; }
    public double WindDirection { get; init; }
}