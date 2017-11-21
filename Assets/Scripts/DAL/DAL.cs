using SQLite4Unity3d;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class DAL
{
    static string _databaseName = "stargazing.db";
    static SQLiteConnection _connection;

    static DAL()
    {
        string path;
        string newPath;

        if (Application.platform == RuntimePlatform.WindowsEditor)
            path = string.Format(@"Assets/StreamingAssets/{0}", _databaseName);
        else
        {
            path = string.Format("{0}/{1}", Application.persistentDataPath, _databaseName);

            // If it's a debug build, re-copy the database when opening so changes are reflected
            if (File.Exists(path) && Debug.isDebugBuild)
                Debug.Log("HELLO");
        }

        if (!File.Exists(path))
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    // On Android, the database file is stored in the JAR, so we need to copy the file to persistent storage
                    var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + _databaseName);

                    var start = DateTime.Now;
                    while (!loadDb.isDone && (DateTime.Now - start).Seconds < 30) { } // Wait for the file to open

                    File.WriteAllBytes(path, loadDb.bytes);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    newPath = Application.dataPath + "/Raw/" + _databaseName;
                    File.Copy(newPath, path);
                    break;
                default:
                    newPath = Application.dataPath + "/StreamingAssets/" + _databaseName;
                    File.Copy(newPath, path);
                    break;
            }
        }

        _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadOnly);
    }

    public static void Close()
    {
        _connection.Close();
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
        var query = "SELECT c.Id, ci.Name, ci.Description, ci.ImageSource " +
            "FROM Constellation c " +
            "JOIN ConstellationInfo ci ON c.Id == ci.ConstellationId " +
            "WHERE ci.CultureId == " + cultureId;
        return _connection.Query<Constellation>(query);
    }

    /// <summary>
    /// Gets the list of Segments for a constellation
    /// </summary>
    public static List<Segment> GetSegments(int constellationId)
    {
        return _connection.Query<Segment>("SELECT * FROM ConstellationSegment WHERE ConstellationId == " + constellationId);
    }

    /// <summary>
    /// Gets a list of all cultures in the database
    /// </summary>
    public static List<Culture> GetCultures()
    {
        return _connection.Query<Culture>("SELECT * FROM Culture");
    }

    /// <summary>
    /// Get the stars that make up the border of a constellation
    /// </summary>
    public static List<ConstellationBorder> GetBorder(int constellationId)
    {
        var query = "SELECT * FROM ConstellationBorder " +
            "WHERE ConstellationId == " + constellationId + " " +
            "ORDER BY SortOrder asc";
        return _connection.Query<ConstellationBorder>(query);
    }
}
