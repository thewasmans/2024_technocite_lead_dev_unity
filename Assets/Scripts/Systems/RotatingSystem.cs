using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial struct RotatingSystem : ISystem
{
    public float time;

    public void OnUpdate(ref SystemState state)
    {
        RotatingJob job = new RotatingJob(){
            DeltaTime = SystemAPI.Time.DeltaTime,
            time = time
        };
        time += SystemAPI.Time.DeltaTime;
        job.ScheduleParallel();
    }

    partial struct RotatingJob : IJobEntity
    {
        public float DeltaTime;
        public float time;
        public void Execute(ref LocalTransform transform, in RotatingSpeedComponentData rot, ref TimerComponentData timer)
        {
            transform = transform.RotateY(time < timer.Value ? rot.Value * DeltaTime : 0);
            Debug.Log("time" + time);
        }
    }
}
