using System.Globalization;

namespace CityBikeDA;

public record BikeTrip
{

    public BikeTrip(string[] columns)
    {
        RideId = columns[0];
        RideableType = columns[1];
        StartedAt = DateTime.Parse(columns[2]);
        EndedAt = DateTime.Parse(columns[3]);
        StartStationName = columns[4];
        StartStationId = columns[5];
        EndStationName = columns[6];
        EndStationId = columns[7];
        StartLat = ParseDouble(columns[8]);
        StartLng = ParseDouble(columns[9]);
        EndLat = ParseDouble(columns[10]);
        EndLng = ParseDouble(columns[11]);
        MemberType = columns[12];
    }
    public string RideId { get; init; }
    public string RideableType { get; init; }
    public DateTime StartedAt { get; init; }
    public DateTime EndedAt { get; init; }
    public string StartStationName { get; init; }
    public string StartStationId { get; init; }
    public string EndStationName { get; init; }
    public string EndStationId { get; init; }
    public double StartLat { get; init; }
    public double StartLng { get; init; }
    public double EndLat { get; init; }
    public double EndLng { get; init; }
    public string MemberType { get; init; }


    private static double ParseDouble(string s)
    {

        if (string.IsNullOrEmpty(s))
        {
            return 0.0;
        }
        return double.Parse(s,CultureInfo.InvariantCulture);
    }
}