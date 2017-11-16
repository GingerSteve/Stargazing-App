using UnityEngine;

/// <summary>
/// Connects a Star to the corresponding GameObject.
/// Should be attached to the GameObject as a component.
/// </summary>
public class StarView : MonoBehaviour
{
    public Star Star { get; set; }
    public Vector3 Position { get { return gameObject.transform.position; } }

    /// <summary>
    /// Creates and positions a GameObject for the Star
    /// </summary>
    public static StarView Create(Star star)
    {
        var parent = ViewerController.Instance.StarParent;
        var prefab = Resources.Load<GameObject>("StarPrefab");

        var obj = Instantiate(prefab, StarUtils.GetStartPosition(star), Quaternion.identity, parent.transform);
        obj.name = "Star-" + star.Id.ToString();

        var starView = obj.AddComponent<StarView>();
        starView.Star = star;

        return starView;
    }
}
