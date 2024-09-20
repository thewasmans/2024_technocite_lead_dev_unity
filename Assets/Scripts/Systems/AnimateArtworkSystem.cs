using System;
using System.Linq;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using SystemAPI = Unity.Entities.SystemAPI;
using Unity.Entities.UniversalDelegates;
using Unity.Burst;

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
        
        Time = Mathf.Clamp(Time + SystemAPI.Time.DeltaTime, 0, 1);

        Index+=50;
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

         [BurstCompile]
        public void Execute(ref LocalTransform transform, ref LegoArtworkDataComponent data)
        {
            int i;
            for (i = 0; i < MaxIndex; i++)
            {
                if(brick.Ids.Value.Ids[i] == data.Id) break;
            }
            
            if(i >= MaxIndex) return;

            transform = new LocalTransform(){
                Position = data.Point * Ease(data.TimeAnimation),
                Scale = brick.Scale * Ease(data.TimeAnimation)
            };
            data.TimeAnimation = Mathf.Clamp(data.TimeAnimation + DeltaTime, 0, 1);
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