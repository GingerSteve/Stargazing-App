using System.Collections.Generic;
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

    float _magnitudeCutoff;
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
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // Set the screen to stay on while the app is active
        _magnitudeCutoff = SystemInfo.deviceType == DeviceType.Desktop ? 6.0f : 5.5f; // Can show a few more on faster devices

        // Create parent objects for stars and constellations
        StarParent = new GameObject("Stars");
        ConstellationParent = new GameObject("Constellations");

        // Get all the stars in the database and display StarViews
        var stars = Star.GetStars(_magnitudeCutoff);

        StarViews = new Dictionary<int, StarView>();
        foreach (var star in stars)
            StarViews.Add(star.Id, StarView.Create(star));

        // Display the constellations for the first culture
        //DisplayNextCulture();
		DisplayNextCulture(0);
    }

    /// <summary>
    /// Switches the visible Culture.
    /// This method has been modified to accept an integer from a dropdown call. 
	/// In the future any additional cultures need only be added to the dropdown options
	/// as long as their dropdown position corresponds to their postion in the culture db
    /// </summary>
	/// 


	public void DisplayNextCulture(int index)
    {
        var cultures = Culture.GetCultures();

		// This 'if' statement protects against an index mismatch 
		if (cultures [index] != null) {
			
			_currentCulture = index;

			// Destroy all currently visible constellations
			foreach (var v in _constellationViews)
				Destroy (v.gameObject);
			_constellationViews.Clear ();

			// Display the constellations for the next culture
			var c = cultures [_currentCulture];
			var constellations = Constellation.GetConstellations (c.Id);
			foreach (var con in constellations)
				_constellationViews.Add (ConstellationView.Create (con));
		}
    }

    void OnApplicationQuit()
    {
        DAL.Close();
    }
}
