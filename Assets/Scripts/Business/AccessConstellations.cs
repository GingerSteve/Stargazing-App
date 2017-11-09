using System.Collections.Generic;

public class AccessConstellations
{
    DAL _dal;

    public AccessConstellations()
    {
        _dal = Main.DataAccess;
    }

    public List<Constellation> GetConstellations()
    {
        var list = _dal.GetConstellations();

        foreach (var con in list)
            con.Segments = _dal.GetSegments(null, con.Id);

        return list;
    }
}
