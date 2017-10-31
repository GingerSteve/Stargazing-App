using SQLite4Unity3d;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class DAL
{
    private SQLiteConnection _connection;

    public DAL(string databasePath)
    {
        string path;

        if (Application.platform == RuntimePlatform.WindowsEditor)
            path = string.Format(@"Assets/StreamingAssets/{0}", databasePath);
        else
            path = string.Format("{0}/{1}", Application.persistentDataPath, databasePath);

        if (!File.Exists(path))
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + databasePath);

                    var start = DateTime.Now;
                    while (!loadDb.isDone && (DateTime.Now - start).Seconds < 30) { }

                    File.WriteAllBytes(path, loadDb.bytes);
                    break;
                case RuntimePlatform.IPhonePlayer:
                    var load2 = Application.dataPath + "/Raw/" + databasePath;
                    File.Copy(load2, path);
                    break;
                default:
                    var load3 = Application.dataPath + "/StreamingAssets/" + databasePath;
                    File.Copy(load3, path);
                    break;
            }
        }

        _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite);
    }
}
