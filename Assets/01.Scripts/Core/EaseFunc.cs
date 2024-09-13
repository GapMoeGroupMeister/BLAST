using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EaseFunc
{
    public static float InQuad(float x)
        => x * x;
    public static float OutQuad(float x)
       => 1 - (1 - x) * (1 - x);
    public static float InOutQuad(float x)
        => x < 0.5f ? 2f * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2f;
    public static float InCubic(float x)
        => x * x * x;
    public static float OutCubic(float x)
        => 1 - Mathf.Pow(1 - x, 3);
    public static float InOutCubic(float x)
        => x < 0.5f ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2f;
}
