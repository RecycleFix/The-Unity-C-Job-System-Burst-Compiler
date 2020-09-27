using Unity.Burst;
using Unity.Mathematics;

public static class Util
{
    // Random at every run, instead of using a static number as seed for Random
    private static Random _RANDOM = new Random(unchecked((uint)System.Guid.NewGuid().GetHashCode()));

    [BurstCompile(CompileSynchronously = true)]
    public static float3 GetNext(float3 min, float3 max)
    {
        return _RANDOM.NextFloat3(min, max);
    }
}