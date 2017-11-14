using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// A Singleton responsible for displaying stars and constellations
/// </summary>
public class ViewerController : MonoBehaviour
{
    static ViewerController Instance { get; set; }

    Dictionary<int, StarView> _starObjects;
    GameObject _starParent;
    GameObject _constParent;
    Material _mat;

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
        _starParent = new GameObject("Stars");
        _constParent = new GameObject("Constellations");

        _mat = new Material(Shader.Find("Unlit/Color"));
        _mat.color = Color.white;

        var stars = Star.GetStars();

        _starObjects = new Dictionary<int, StarView>();
        foreach (var star in stars)
            _starObjects.Add(star.Id, StarView.Create(star, _starParent, _mat));

        CycleCulturesAsync();
    }

    /// <summary>
    /// Cycles through the cultures available and displays the constellations for each.
    /// In the future, his could be replaced with a menu allowing the user to choose a culture to view.
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
                views.Add(ConstellationView.Create(con, _starObjects, _constParent, _mat));

            await Task.Delay(TimeSpan.FromSeconds(10));
            i = (i + 1) % cultures.Count;
        }
    }

}
