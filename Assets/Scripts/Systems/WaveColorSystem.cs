using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct WaveColorSystem : ISystem
{
    public float Time;

    public void OnUpdate(ref SystemState state)
    {
        new WaveColorJob(){
            Time = Time += SystemAPI.Time.DeltaTime,
        }.ScheduleParallel();
    }

    partial struct WaveColorJob : IJobEntity
    {
        public float Time;
        
        public void Execute(ref LocalTransform transform, in WaveColorComponentData data)
        {

        }
    }
}