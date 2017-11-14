using System.Collections.Generic;
using UnityEngine;

public class ConstellationView : MonoBehaviour
{
    public Constellation Constellation { get; private set; }

    public static ConstellationView Create(Constellation con, Dictionary<int, StarView> starViews, GameObject parent, Material mat)
    {
        var obj = new GameObject("Constellation-" + con.Id);
        obj.transform.parent = parent.transform;

        var constellationView = obj.AddComponent<ConstellationView>();
        constellationView.Constellation = con;

        var count = 0;
        foreach (var pair in con.Segments)
        {
            GameObject line = new GameObject("Line-" + count);
            line.transform.parent = obj.transform;

            LineRenderer lr = line.AddComponent<LineRenderer>();

            lr.material = mat;

            lr.startColor = Color.white;
            lr.endColor = Color.white;
            lr.startWidth = 0.5f;
            lr.endWidth = 0.5f;
            lr.SetPosition(0, starViews[pair.StarA].Position);
            lr.SetPosition(1, starViews[pair.StarB].Position);

            count++;
        }

        return constellationView;
    }
}
