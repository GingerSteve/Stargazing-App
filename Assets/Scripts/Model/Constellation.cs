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

    public List<StarView> StarViews { get; private set; }
    public List<Segment> Segments { get; private set; }
    public List<ConstellationBorder> Border { get; private set; }

    public static List<Constellation> GetConstellations(int cultureId)
    {
        var list = DAL.GetConstellations(cultureId);
        var starList = ViewerController.Instance.StarViews;

        foreach (var con in list)
        {
            // Get the StarView for each Star in the Constellation
            var tempStars = Star.GetStarsForConstellation(con.Id);
            con.StarViews = tempStars.Select(s => starList[s.Id]).ToList();

            con.Segments = Segment.GetSegments(con.Id);
            con.Border = ConstellationBorder.GetBorder(con.Id);
        }

        return list;
    }
}

