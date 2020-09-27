using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile(CompileSynchronously = true)]
public struct MoveJob : IJobParallelFor
{
    public NativeArray<float3> Positions;
    [ReadOnly] public float DeltaTime;

    public static JobHandle Begin(NativeArray<float3> positions, int batchSize)
    {
        MoveJob job = new MoveJob()
        {
            Positions = positions,
            DeltaTime = Time.deltaTime
        };

        return IJobParallelForExtensions.Schedule(job, positions.Length, batchSize);
    }

    public void Execute(int index)
    {
        Positions[index] += DeltaTime;
    }
}