using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using SystemAPI = Unity.Entities.SystemAPI;
using Unity.Burst;
using Unity.Mathematics;
using System.Numerics;
using System.Collections.Generic;

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
        animateJob.Timing += SystemAPI.Time.DeltaTime * brick.SpeedAnimation;
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
            float src = data.Index*.01f;
            float dst = data.Index*.01f+1f;
            float value = math.clamp(Timing, src, dst);
            value = math.remap(src, dst, 0, 1, value);
            
            transform = new LocalTransform(){
                Position = (brick.SpawnPosition + data.Point) *(1-value) + data.Point * value,
                Scale = brick.Scale * value,
                Rotation = quaternion.Euler(data.Rotation * (1-value) * 10)
            };

            BrickLego bricks;

            // BrickLego brick = bricks[i];

            // var value = Ease(i, CurrentTime, bricks.Count, 0.0f);
            
            var direction = data.Point - brick.SpawnPosition;

            transform = new LocalTransform(){
                Position = brick.SpawnPosition + direction * value,
                Scale = brick.Scale * EaseOutSine(value),
                // Rotation = UnityEngine.Quaternion.Lerp(brick.RotStart, brick.RotEnd, value),
                Rotation = quaternion.Euler(data.Rotation * (1-value) * 10),
            };
        }

        public float EaseOutSine(float x) => Mathf.Sin((x * Mathf.PI) / 2);
        
        public float Ease(int index, float time, float quantity, float phase)
        {
            var stepTime = 1.0f / quantity;

            var d = phase / quantity * index;

            float src = index * stepTime - d;
            float dst = index * stepTime + stepTime - d;
            float value = math.clamp(time * (1 - phase), src, dst);
            value = math.remap(src, dst, 0, 1, value);

            return value;
        }
    }
}

public readonly partial struct LegoArtworkAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<LegoArtworkDataComponent> data;
}