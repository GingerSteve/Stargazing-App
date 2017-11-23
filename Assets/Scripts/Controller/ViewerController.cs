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
        StarParent = new GameObject("Stars");
        ConstellationParent = new GameObject("Constellations");

        var stars = Star.GetStars();

        StarViews = new Dictionary<int, StarView>();
        foreach (var star in stars)
            StarViews.Add(star.Id, StarView.Create(star));

        CycleCulturesAsync();
    }

    /// <summary>
    /// Cycles through the cultures available and displays the constellations for each.
    /// In the future, this could be replaced with a menu allowing the user to choose a culture to view.
    /// </summary>
    async void CycleCulturesAsync()
    {
        var cultures = Culture.GetCultures();
        var views = new List<ConstellationView>();

        var i = 0;
        while (Application.isPlaying)
        {
            // Destroy all currently visible constellations
            foreach (var v in views)
                Destroy(v.gameObject);
            views.Clear();

            // Display the constellations for the next culture
            var c = cultures[i];
            Debug.Log("Showing constellations for " + c.Name);

            var constellations = Constellation.GetConstellations(c.Id);
            foreach (var con in constellations)
                views.Add(ConstellationView.Create(con));

            await Task.Delay(TimeSpan.FromSeconds(100));
            i = (i + 1) % cultures.Count;
        }
    }

    void OnApplicationQuit()
    {
        DAL.Close();
    }
}
