using System.Collections.Generic;

public class Culture
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public static List<Culture> GetCultures()
    {
        return DAL.GetCultures();
    }
}
