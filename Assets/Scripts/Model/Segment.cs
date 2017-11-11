using System.Collections.Generic;

public class Segment
{
    public int StarA { get; set; }
    public int StarB { get; set; }

    public static List<Segment> GetSegments(int constId)
    {
        return DAL.GetSegments(constId);
    }
}