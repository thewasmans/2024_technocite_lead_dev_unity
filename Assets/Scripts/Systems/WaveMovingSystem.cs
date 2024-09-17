using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct WaveMovingSystem : ISystem
{
    public float Time;

    public void OnUpdate(ref SystemState state)
    {
        new WaveMovingJob(){
            Time = Time += SystemAPI.Time.DeltaTime,
        }.ScheduleParallel();
    }

    partial struct WaveMovingJob : IJobEntity
    {
        public float Time;
        
        public void Execute(ref LocalTransform transform, in CubePositionComponentData data)
        {
            // transform.Position = new float3(data.X, MathF.Sin(Time * data.Frequency + data.X + data.Y) * data.Amplitude, data.Y);
            transform.Position = new float3(data.X, MathF.Sin(MathF.Sin(data.X * data.Y) * Time * data.Frequency + data.X + data.Y) * data.Amplitude, data.Y);
        }
    }
}