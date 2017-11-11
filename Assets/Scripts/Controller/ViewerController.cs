using System.Collections.Generic;
using UnityEngine;

public class ViewerController : MonoBehaviour
{
    private static ViewerController Instance { get; set; }

    Dictionary<int, GameObject> _starObjects;
    GameObject _parent;
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
        _parent = new GameObject("StarParent");
        _mat = new Material(Shader.Find("Unlit/Color"));
        _mat.color = Color.white;

        var stars = Star.GetStars();
        _starObjects = CreateStarObjects(stars);

        var constellations = Constellation.GetConstellations();
        DrawConstellations(constellations);
    }

    Dictionary<int, GameObject> CreateStarObjects(List<Star> stars)
    {
        Dictionary<int, GameObject> dict = new Dictionary<int, GameObject>();

        foreach (var star in stars)
        {
            var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obj.name = "Star-" + star.Id.ToString();

            var s = obj.AddComponent<StarView>();
            s.Star = star;

            obj.transform.parent = _parent.transform;
            obj.transform.localPosition = StarUtils.GetStartPosition(star);

            obj.GetComponent<Renderer>().material = _mat;

            dict.Add(star.Id, obj);
        }

        return dict;
    }

    void DrawConstellations(List<Constellation> cons)
    {
        foreach (var con in cons)
            DrawLines(con);
    }

    void DrawLines(Constellation con)
    {
        var count = 0;
        foreach (var pair in con.Segments)
        {
            GameObject line = new GameObject();
            line.name = "Line-" + con.Id + "-" + count;
            line.transform.parent = _parent.transform;

            LineRenderer lr = line.AddComponent<LineRenderer>();

            lr.material = _mat;

            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.startWidth = 0.5f;
            lr.endWidth = 0.5f;
            lr.SetPosition(0, _starObjects[pair.StarA].transform.position);
            lr.SetPosition(1, _starObjects[pair.StarB].transform.position);

            count++;
        }
    }

}
