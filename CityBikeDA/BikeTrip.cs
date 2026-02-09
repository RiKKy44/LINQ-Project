namespace CityBikeDA;

public record BikeTrip()
{
    public string RideId { get; init; }
    public string RideableType  { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime EndedAt { get; init; }
    public string StartStationId { get; init; }
    public string EndStationId { get; init; }
    public double StartLat { get; init; }
    public double StartLng { get ; init; }
    public double EndLat { get; init; }
    public double EndLng { get; init; }
    public string MemberType { get; init; }
}