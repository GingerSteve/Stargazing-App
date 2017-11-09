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

    const int RADIUS = 75; // The distance from the origin to the Star GameObjects

    /// <summary>
    /// Returns the position of the star in Unity's coordinates based on the Right Ascension and Declination
    /// </summary>
    public static Vector3 GetStartPosition(Star star)
    {
        var ra = 2 * Mathf.PI * (star.RAHours * HOURS_TO_DAYS + star.RAMinutes * MIN_TO_DAYS + star.RASeconds * SEC_TO_DAYS);
        var dec = Mathf.Deg2Rad * star.DeclinationSignFactor * (star.DeclinationDegrees + star.DeclinationArcMinutes * ARCMIN_TO_DEG + star.DeclinationArcSeconds * ARCSEC_TO_DEG);

        var x = Mathf.Abs(Mathf.Cos(ra) * RADIUS * Mathf.Cos(dec));
        var y = Mathf.Abs(Mathf.Sin(dec) * RADIUS);
        var z = Mathf.Abs(Mathf.Sin(ra) * RADIUS * Mathf.Cos(dec));

        if (ra >= Mathf.PI / 2f && ra <= 1.5f * Mathf.PI)
            x = -x;
        if (ra >= Mathf.PI || ra <= 0)
            z = -z;
        y *= star.DeclinationSignFactor;

        return new Vector3(x, y, z);
    }
}
