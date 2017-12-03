using System.Collections.Generic;

/// <summary>
/// A star that makes up the border for a Constellation. This is used to generate the clickable mesh.
/// Order MUST BE clockwise, or the mesh won't be clickable.
/// </summary>
public class ConstellationBorder
{
    public int StarId { get; private set; }
    public int Order { get; private set; }

    public static List<ConstellationBorder> GetBorder(int constId)
    {
        return DAL.GetBorder(constId);
    }
}
