using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A Singleton responsible for displaying stars and constellations.
/// Also stores 'static' information about the scene.
/// </summary>
public class ViewerController : MonoBehaviour
{
    public MenuController Menu;

    public Dictionary<int, StarView> StarViews { get; private set; }
    public GameObject StarParent { get; private set; }
    public GameObject ConstellationParent { get; private set; }

    int _currentCulture = -1;
    List<ConstellationView> _constellationViews = new List<ConstellationView>();

    public static ViewerController Instance { get; private set; }
    object _lock = new object();

    void Awake()
    {
        lock (_lock)
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }
    }

    void Start()
    {
        Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;

        StarParent = new GameObject("Stars");
        ConstellationParent = new GameObject("Constellations");

        var stars = Star.GetStars();

        StarViews = new Dictionary<int, StarView>();
        foreach (var star in stars)
            StarViews.Add(star.Id, StarView.Create(star));

        DisplayNextCulture();
    }

    /// <summary>
    /// Switches the visible Culture to the next.
    /// In the future, this could be replaced with a menu allowing the user to choose a culture to view.
    /// </summary>
    public void DisplayNextCulture()
    {
        var cultures = Culture.GetCultures();

        _currentCulture++;
        _currentCulture %= cultures.Count;

        // Destroy all currently visible constellations
        foreach (var v in _constellationViews)
            Destroy(v.gameObject);
        _constellationViews.Clear();

        // Display the constellations for the next culture
        var c = cultures[_currentCulture];
        var constellations = Constellation.GetConstellations(c.Id);
        foreach (var con in constellations)
            _constellationViews.Add(ConstellationView.Create(con));
    }

    void OnApplicationQuit()
    {
        DAL.Close();
    }
}
