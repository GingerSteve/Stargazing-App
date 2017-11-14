using SQLite4Unity3d;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class DAL
{
    static string _databasePath = "stargazing.db";
    static SQLiteConnection _connection;

    static DAL()
    {
        string path;

        if (Application.platform == RuntimePlatform.WindowsEditor)
            path = string.Format(@"Assets/StreamingAssets/{0}", _databasePath);
        else
            path = string.Format("{0}/{1}", Application.persistentDataPath, _databasePath);

        if (!File.Exists(path))
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + _databasePath);

                    var start = DateTime.Now;
                    while (!loadDb.isDone && (DateTime.Now - start).Seconds < 30) { }

                    File.WriteAllBytes(path, loadDb.bytes);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    var load2 = Application.dataPath + "/Raw/" + _databasePath;
                    File.Copy(load2, path);
                    break;
                default:
                    var load3 = Application.dataPath + "/StreamingAssets/" + _databasePath;
                    File.Copy(load3, path);
                    break;
            }
        }

        _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite);
    }

    /// <summary>
    /// Gets all Stars in the database
    /// </summary>
    public static List<Star> GetStars()
    {
        return _connection.Query<Star>("SELECT * FROM Star");
    }

    /// <summary>
    /// Gets a list of all constellations
    /// </summary>
    public static List<Constellation> GetConstellations(int cultureId)
    {
        var query = "SELECT c.Id " +
            "FROM Constellation c " +
            "JOIN ConstellationInfo ci ON c.Id == ci.ConstellationId " +
            "WHERE ci.CultureId == " + cultureId;
        return _connection.Query<Constellation>(query);
    }

    /// <summary>
    /// Gets the a list of Segments for a constellation
    /// </summary>
    public static List<Segment> GetSegments(int constellationId)
    {
        return _connection.Query<Segment>("SELECT * FROM ConstellationSegment WHERE ConstellationId == " + constellationId);
    }

    public static List<Culture> GetCultures()
    {
        return _connection.Query<Culture>("SELECT * FROM Culture");
    }
}
