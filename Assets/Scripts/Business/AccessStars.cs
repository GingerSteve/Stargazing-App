using System.Collections.Generic;

public class AccessStars
{
    DAL _dal;

    public AccessStars()
    {
        _dal = Main.DataAccess;
    }

    public List<Star> GetStars()
    {
        return _dal.GetStars();
    }
}
