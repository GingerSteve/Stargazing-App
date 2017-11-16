using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Connects a Constellation to the corresponding GameObject.
/// Should be attached to the GameObject as a component.
/// </summary>
public class ConstellationView : MonoBehaviour
{
    public Constellation Constellation { get; private set; }

    /// <summary>
    /// Creates a GameObject for the Constellation, including the lines and clickable mesh
    /// </summary>
    public static ConstellationView Create(Constellation con)
    {
        var starViews = ViewerController.Instance.StarViews;
        var parent = ViewerController.Instance.ConstellationParent;
        var prefab = Resources.Load<GameObject>("LinePrefab");

        // Create a parent object for the constellation lines
        var obj = new GameObject(con.Name);
        obj.transform.parent = parent.transform;

        var constellationView = obj.AddComponent<ConstellationView>();
        constellationView.Constellation = con;


        // Create a GameObject for each line segment
        var count = 0;
        foreach (var pair in con.Segments)
        {
            var line = Instantiate(prefab, obj.transform);
            line.name = "Line-" + count;

            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.SetPosition(0, starViews[pair.StarA].Position);
            lr.SetPosition(1, starViews[pair.StarB].Position);

            count++;
        }


        // Create Mesh
        var verts = new List<Vector3>();
        foreach (var b in con.Border)
            verts.Add(starViews[b.StarId].Position);

        var tris = new List<int>();
        for (int i = 0; i < verts.Count - 1; i++)
        {
            tris.Add(0);
            tris.Add(i);
            tris.Add(i + 1);
        }

        var filter = obj.AddComponent<MeshFilter>();
        filter.mesh.vertices = verts.ToArray();
        filter.mesh.triangles = tris.ToArray();
        filter.mesh.RecalculateNormals();

        var col = obj.AddComponent<MeshCollider>();
        col.convex = true;


        // Create event trigger
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(data => ViewerController.Instance.Menu.OpenMenu(con));

        EventTrigger trigger = obj.AddComponent<EventTrigger>();
        trigger.triggers.Add(entry);

        return constellationView;
    }
}
