using System.Collections.Generic;

/// <summary>
/// A Segment of a Constellation. Used to draw the lines connecting the Stars in a Constellation.
/// </summary>
public class Segment
{
    public int StarA { get; private set; }
    public int StarB { get; private set; }

    public static List<Segment> GetSegments(int constId)
    {
        return DAL.GetSegments(constId);
    }
}