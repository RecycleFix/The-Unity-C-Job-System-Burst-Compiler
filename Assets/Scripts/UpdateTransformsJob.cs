using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Jobs;

[BurstCompile(CompileSynchronously = true)]
public struct UpdateTransformsJob : IJobParallelForTransform
{
    public NativeArray<float3> Positions;

    public static JobHandle Begin(TransformAccessArray array, NativeArray<float3> positions, JobHandle dependency)
    {
        UpdateTransformsJob job = new UpdateTransformsJob()
        {
            Positions = positions
        };

        return IJobParallelForTransformExtensions.Schedule(job, array, dependency);
    }

    public void Execute(int index, TransformAccess transform)
    {
        transform.position = Positions[index];
    }
}