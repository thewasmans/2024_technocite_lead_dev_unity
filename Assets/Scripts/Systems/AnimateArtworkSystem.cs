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
    protected AnimateJob animateJob;

    protected override void OnCreate()
    {
        animateJob = new AnimateJob();
    }

    protected override void OnUpdate()
    {
        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        animateJob.brick = brick;
        animateJob.Timing = brick.Timing;
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
            transform = new LocalTransform(){
                Position = new float3(data.Point.x * Timing / (1 + (data.Id * .15f ) * (1-Timing)),
                data.Point.z * Timing / (1 + (data.Id * .15f ) * (1-Timing)),
                data.Point.y * Timing / (1 + (data.Id * .15f ) * (1-Timing))),
                Scale = brick.Scale,
                Rotation = quaternion.identity
            };
            data.TimeAnimation = Mathf.Clamp(data.TimeAnimation + DeltaTime, 0, .9f);
        }
    }
}

public readonly partial struct LegoArtworkAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<LegoArtworkDataComponent> data;
}