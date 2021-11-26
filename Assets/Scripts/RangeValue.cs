using System;
using Random = UnityEngine.Random;

[Serializable]
public struct RangeValue
{
    public float Min;
    public float Max;

    public RangeValue(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public float GetRandom() => Random.Range(Min, Max);
}
