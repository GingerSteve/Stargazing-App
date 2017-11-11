using System.Collections.Generic;

public class Star
{
    public int Id { get; set; }
    public int RAHours { get; set; }
    public int RAMinutes { get; set; }
    public float RASeconds { get; set; }
    public int DeclinationSignFactor { get; set; }
    public int DeclinationDegrees { get; set; }
    public int DeclinationArcMinutes { get; set; }
    public float DeclinationArcSeconds { get; set; }

    public static List<Star> GetStars()
    {
        return DAL.GetStars();
    }
}
