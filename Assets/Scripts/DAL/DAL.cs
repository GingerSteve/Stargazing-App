using SQLite4Unity3d;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class DAL
{
    const int DATABASE_VERSION = 0;

    static string _databaseName = "stargazing.db";
    static SQLiteConnection _connection;

    /// <summary>
    /// Sets up the DAL
    /// </summary>
    static DAL()
    {
        string path;

        // TODO: Test on all platforms
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.LinuxEditor:
                path = "Assets/StreamingAssets/" + _databaseName;
                break;
            case RuntimePlatform.Android:
                path = Application.persistentDataPath + "/" + _databaseName;

                // On Android, the database file is stored in the JAR. We need to copy it to persistent storage to access it.
                // Only copy the database if the stored version and packaged versions are different
                if (DATABASE_VERSION != PlayerPrefs.GetInt("dataVersion"))
                {
                    var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + _databaseName);

                    var start = DateTime.Now;
                    while (!loadDb.isDone && (DateTime.Now - start).Seconds < 30) { } // Wait for the file to open

                    File.WriteAllBytes(path, loadDb.bytes);
                    PlayerPrefs.SetInt("dataVersion", DATABASE_VERSION);
                }
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw/" + _databaseName;
                break;
            default:
                path =  Application.dataPath + "/StreamingAssets/" + _databaseName;
                break;
        }

        _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadOnly);
    }

    public static void Close()
    {
        _connection.Close();
    }

    /// <summary>
    /// Gets all Stars in the database that are either: brighter than minMagnitude OR in a constellation
    /// </summary>
    public static List<Star> GetStars(float minMagnitude)
    {
        var query = "SELECT s.* FROM Star s " +
            "JOIN ConstellationSegment cs " +
                "ON s.Id = cs.StarA " +
                "OR s.Id = cs.StarB " +
            "GROUP BY s.id " +
            "UNION " +
            "SELECT s.* FROM Star s " +
            "WHERE s.ApparentMagnitude < " + minMagnitude;

        return _connection.Query<Star>(query);
    }

    /// <summary>
    /// Gets a list of constellations for a culture
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
