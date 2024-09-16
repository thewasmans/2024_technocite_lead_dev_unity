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
        public void Execute(ref LocalTransform transform, in ColorTweenComponentData rot, ref TimerComponentData timer)
        {
        //   SystemAPI.Query<RefRW<<>
        }
    }
}
