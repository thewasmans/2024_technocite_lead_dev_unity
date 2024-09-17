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
            Amplitude = 3,
            Frequency = 2
        }.ScheduleParallel();
    }

    partial struct WaveMovingJob : IJobEntity
    {
        public float Time;
        public float Amplitude;
        public float Frequency;
        
        public void Execute(ref LocalTransform transform, in CubePositionComponentData position)
        {
            transform.Position = new float3(position.X, MathF.Sin(Time * Frequency) * Amplitude, position.Y);           
        }
    }
}