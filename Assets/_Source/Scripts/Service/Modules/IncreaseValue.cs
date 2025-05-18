using UnityEngine;

public static class IncreaseValue
{
    public static int Int(float baseValue, float level, float degree)
    {
        float value = baseValue * Mathf.Pow(degree, level);

        return Mathf.RoundToInt(value);
    }

    public static float Float(float baseValue, float level, float degree)
    {
        float value = baseValue * Mathf.Pow(degree, level);

        return Mathf.Round(value);
    }

    public static float FloatNotRound(float baseValue, float level, float degree)
    {
        float value = baseValue * Mathf.Pow(degree, level);

        return value;
    }
}