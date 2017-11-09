using UnityEngine;

/// <summary>
/// Singleton responsible for setting up and storing instances of the DAL and ConstellationViewer
/// </summary>
public class Main : MonoBehaviour
{
    private static Main Instance { get; set; }
    public static DAL DataAccess { get; private set; }
    public static ConstellationViewer Viewer { get; private set; }

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
        DataAccess = new DAL("stargazing.db");
        Viewer = new ConstellationViewer();
    }
}
