using System.Collections.Generic;
using System.Linq;

/// <summary>
/// An object representing a constellation for a single Culture.
/// This combines the database tables: Constellation, ConstellationInfo, Segment, and ConstellationBorder
/// </summary>
public class Constellation
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string ImageSource { get; private set; }

    public List<Segment> Segments { get; private set; }
    public List<ConstellationBorder> Border { get; private set; }

    public static List<Constellation> GetConstellations(int cultureId)
    {
        var list = DAL.GetConstellations(cultureId);

        foreach (var con in list)
        {
            con.Segments = Segment.GetSegments(con.Id);
            con.Border = ConstellationBorder.GetBorder(con.Id);
        }

        return list;
    }
}

