using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using SystemAPI = Unity.Entities.SystemAPI;
using Unity.Burst;
using Unity.Mathematics;

partial class AnimateArtworkSystem : SystemBase
{
    protected AnimateJob animateJob;

    protected override void OnCreate()
    {
        animateJob = new AnimateJob();
    }

    protected override void OnUpdate()
    {
        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        animateJob.brick = brick;
        animateJob.Timing+=SystemAPI.Time.DeltaTime;
        animateJob.DeltaTime = SystemAPI.Time.DeltaTime;
        animateJob.ScheduleParallel();
    }

    public partial struct AnimateJob : IJobEntity 
    {
        public BrickDataComponent brick;
        public float DeltaTime;
        public float Timing;
        public int MaxIndex;
        public float3 Position;

        [BurstCompile]
        public void Execute(ref LocalTransform transform, ref LegoArtworkDataComponent data)
        {
            float src = data.Id*.1f;
            float dst = data.Id*.1f+1f;
            float value = math.clamp(Timing, src, dst);
            value = math.remap(src, dst, 0, 1, value);
            
            transform = new LocalTransform(){
                Position = new float3(10,10,10)*(1-value) + data.Point * value,
                Scale = brick.Scale * value,
                Rotation = quaternion.Euler(Vector3.left * (1-value) + Vector3.forward * (1-value))
            };
        }
    }
}

public readonly partial struct LegoArtworkAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<LegoArtworkDataComponent> data;
}