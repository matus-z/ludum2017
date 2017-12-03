using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class ExtensionMethods
{
    /// <summary>
    /// Rotates Vecto2 by angle
    /// </summary>
    /// <param name="angle">Angle in degrees</param>
    /// <returns>Rotated Vector2</returns>
    public static Vector2 Rotated(this Vector2 v, float angle)
    {
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;

        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);

        return v;
    }
}
