using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        var linePrefab = Resources.Load<GameObject>("LinePrefab");
        var constPrefab = Resources.Load<GameObject>("ConstellationPrefab");

        // Create a parent object for the constellation lines
        var obj = Instantiate(constPrefab, new Vector3(0, 0, 0), Quaternion.identity, parent.transform);
        obj.name = con.Name;

        // Set the ConstellationView component's constellation to the correct one
        var constellationView = obj.GetComponent<ConstellationView>();
        constellationView.Constellation = con;

        // Set the canvas position and text
        obj.GetComponentInChildren<Canvas>().transform.position = GetPosition(con);
        obj.GetComponentInChildren<Text>().text = con.Name;


        // Create a GameObject for each line segment and position the segment
        var count = 0;
        foreach (var pair in con.Segments)
        {
            var line = Instantiate(linePrefab, obj.transform);
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

        var mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();

        var col = obj.AddComponent<MeshCollider>();
        col.sharedMesh = mesh;


        // Create event trigger to make the mesh clickable
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener(data =>
        {
            if (!(data as PointerEventData).dragging)
                ViewerController.Instance.Menu.OpenMenu(con);
        });

        var trigger = obj.AddComponent<EventTrigger>();
        trigger.triggers.Add(entry);


        return constellationView;
    }

    /// <summary>
    /// Returns the centre position of a constellation (average position of all Stars in the constellation)
    /// </summary>
    static Vector3 GetPosition(Constellation con)
    {
        var vec = new Vector3(0, 0, 0);

        foreach (var s in con.StarViews)
            vec += s.Position;

        vec /= con.StarViews.Count;

        return vec;
    }
}
