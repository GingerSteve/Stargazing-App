using System.Collections.Generic;

public class Culture
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static List<Culture> GetCultures()
    {
        return DAL.GetCultures();
    }
}
