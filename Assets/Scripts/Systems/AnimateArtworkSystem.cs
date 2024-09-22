using System;
using System.Linq;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using SystemAPI = Unity.Entities.SystemAPI;
using Unity.Entities.UniversalDelegates;
using Unity.Burst;
using Unity.Mathematics;

partial class AnimateArtworkSystem : SystemBase
{
    public float Time = .0f;
    public int Index = 0;
    protected AnimateJob animateJob;

    protected override void OnCreate()
    {
        animateJob = new AnimateJob();
    }

    protected override void OnUpdate()
    {
        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        Time = Mathf.Clamp(Time + SystemAPI.Time.DeltaTime, 0, 2 * math.PI);

        Index+=1;
        animateJob.brick = brick;
        animateJob.DeltaTime = SystemAPI.Time.DeltaTime;
        animateJob.MaxIndex = Index;
        animateJob.ScheduleParallel();
    }

    public partial struct AnimateJob : IJobEntity 
    {
        public BrickDataComponent brick;
        public float DeltaTime;
        public int MaxIndex;
        public float3 Position;

        [BurstCompile]
        public void Execute(ref LocalTransform transform, ref LegoArtworkDataComponent data)
        {
            if(data.Id > MaxIndex) return;

            float radius = math.length(data.Point);

            Position = new float3(0,2,10);
            var t = data.TimeAnimation;
            var t2 = t * (5 * math.PI / 2) / (2 * math.PI);
            float x = math.cos(t) * t * (1 / (2 * math.PI));
            float y = math.sin(t2) * t2 * (2 / (5 * math.PI));
            
            transform = new LocalTransform(){
                // Position = new float3(data.Point.x * math.cos(data.TimeAnimation*6.4f)*data.TimeAnimation,
                // data.Point.z,
                // data.Point.y * math.sin(data.TimeAnimation*8f)*data.TimeAnimation)+Position,
                Position = new float3(data.Point.x * x,
                data.Point.z * x,
                data.Point.y * y),
                Scale = brick.Scale 
            };
            //* Ease(data.TimeAnimation / (math.PI * 2))
            // data.TimeAnimation = Mathf.Clamp(data.TimeAnimation + DeltaTime, 0, 1);
            data.TimeAnimation = Mathf.Clamp(data.TimeAnimation + DeltaTime, 0, 2 * math.PI);
        }

        public float CircularEaseX(float t)
        {
            return math.cos(t) * t;
        }

        public float CircularEaseZ(float t)
        {
            return math.sin(t) * t;
        }

        public float Ease(float time)
        {
            return MathF.Sin(time);
        }
    }
}

public readonly partial struct LegoArtworkAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<LegoArtworkDataComponent> data;
}