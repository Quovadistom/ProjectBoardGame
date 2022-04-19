using UnityEngine;

public static class MathExtensions
{
    public static bool CloseTo(this float a, float b, float tolerance = 0.00001f)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
}
