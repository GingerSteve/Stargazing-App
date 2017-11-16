﻿using System.Collections.Generic;

/// <summary>
/// A star that makes up the border for a Constellation. This is used to generate the clickable mesh.
/// </summary>
public class ConstellationBorder
{
    public int StarId { get; set; }
    public int Order { get; set; }

    public static List<ConstellationBorder> GetBorder(int constId)
    {
        return DAL.GetBorder(constId);
    }
}