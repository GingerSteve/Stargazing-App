using UnityEngine;

/// <summary>
/// Represents a Star GameObject
/// </summary>
public class StarView : MonoBehaviour
{
    public Star Star { get; set; }
    public Vector3 Position { get { return gameObject.transform.position; } }

    public static StarView Create(Star star, GameObject parent, Material mat)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = "Star-" + star.Id.ToString();

        obj.transform.parent = parent.transform;
        obj.transform.localPosition = StarUtils.GetStartPosition(star);

        obj.GetComponent<Renderer>().material = mat;

        var starView = obj.AddComponent<StarView>();
        starView.Star = star;

        return starView;
    }
}
