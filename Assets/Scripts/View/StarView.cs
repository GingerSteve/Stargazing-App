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
    /// Creates and positions a GameObject for a Star
    /// </summary>
    public static StarView Create(Star star)
    {
        var parent = ViewerController.Instance.StarParent;
        var prefab = Resources.Load<GameObject>("StarPrefab");

        var obj = Instantiate(prefab, StarUtils.GetStartPosition(star), Quaternion.identity, parent.transform);
        obj.name = "Star-" + star.Id.ToString();

        var starView = obj.AddComponent<StarView>();
        starView.Star = star;

        starView.SetScale();

        return starView;
    }

    /// <summary>
    /// Sets the scale of the Star GameObject to the scale specified, or to the appropriate scale based on apparent magnitude
    /// </summary>
    public void SetScale(float? scale = null)
    {
        var mag = 1f;

        if (scale.HasValue)
            mag = scale.Value;
        else if (Star.ApparentMagnitude.HasValue)
            mag = 1f / (Star.ApparentMagnitude.Value + 2.4f);

        gameObject.transform.localScale = new Vector3(mag, mag, mag);
    }
}
