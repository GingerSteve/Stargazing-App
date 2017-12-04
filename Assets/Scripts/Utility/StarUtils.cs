using UnityEngine;

/// <summary>
/// Some utility methods and constants for Stars
/// </summary>
public class StarUtils
{
    // Some constants for unit conversion
    const float HOURS_TO_DAYS = 1 / 24f;
    const float MIN_TO_DAYS = 1 / (24f * 60f);
    const float SEC_TO_DAYS = 1 / (24f * 60f * 60f);
    const float ARCMIN_TO_DEG = 1 / 60f;
    const float ARCSEC_TO_DEG = 1 / (60f * 60f);

    const int RADIUS = 85; // The distance from the origin to the Star GameObjects

    /// <summary>
    /// Returns the position of the star in Unity's coordinates based on the Right Ascension and Declination
    /// </summary>
    public static Vector3 GetStartPosition(Star star)
    {
        float ra = 0f;
        float dec = 0f;

        if (star.RARadians != null && star.DecRadians != null)
        {
            ra = star.RARadians.Value;
            dec = star.DecRadians.Value;
        }
        else if (star.RAHours != null && star.RAMinutes != null && star.RASeconds != null &&
            star.DeclinationSignFactor != null && star.DeclinationDegrees != null && star.DeclinationArcMinutes != null && star.DeclinationArcSeconds != null)
        {
            ra = 2 * Mathf.PI * (star.RAHours.Value * HOURS_TO_DAYS + star.RAMinutes.Value * MIN_TO_DAYS + star.RASeconds.Value * SEC_TO_DAYS);
            dec = Mathf.Deg2Rad * star.DeclinationSignFactor.Value * (star.DeclinationDegrees.Value + star.DeclinationArcMinutes.Value * ARCMIN_TO_DEG + star.DeclinationArcSeconds.Value * ARCSEC_TO_DEG);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }

        var x = Mathf.Abs(Mathf.Cos(ra) * RADIUS * Mathf.Cos(dec));
        var y = Mathf.Abs(Mathf.Sin(dec) * RADIUS);
        var z = Mathf.Abs(Mathf.Sin(ra) * RADIUS * Mathf.Cos(dec));

        if (ra >= Mathf.PI / 2f && ra <= 1.5f * Mathf.PI)
            x = -x;
        if (ra >= Mathf.PI || ra <= 0)
            z = -z;
        y *= dec < 0 ? -1 : 1;

        return new Vector3(x, y, z);
    }
}
