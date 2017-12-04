using System.Collections.Generic;

public class Star
{
    public int Id { get; private set; }
    public int? HipId { get; private set; }
    public float? RARadians { get; private set; }
    public float? DecRadians { get; private set; }
    public int? RAHours { get; private set; }
    public int? RAMinutes { get; private set; }
    public float? RASeconds { get; private set; }
    public int? DeclinationSignFactor { get; private set; }
    public int? DeclinationDegrees { get; private set; }
    public int? DeclinationArcMinutes { get; private set; }
    public float? DeclinationArcSeconds { get; private set; }
    public float? ApparentMagnitude { get; private set; }

    public static List<Star> GetStars(float minMagnitude)
    {
        return DAL.GetStars(minMagnitude);
    }
}
