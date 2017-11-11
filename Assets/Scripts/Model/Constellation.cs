using System.Collections.Generic;

public class Constellation
{
    public int Id { get; private set; }
    public List<Segment> Segments { get; set; }

    public static List<Constellation> GetConstellations()
    {
        var list = DAL.GetConstellations();

        foreach (var con in list)
            con.Segments = Segment.GetSegments(con.Id);

        return list;
    }
}

