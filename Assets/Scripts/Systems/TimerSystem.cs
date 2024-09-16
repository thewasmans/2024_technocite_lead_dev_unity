using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial struct TimerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        TimerJob job = new TimerJob(){
            DeltaTime = SystemAPI.Time.DeltaTime,
            TimerValue = 0
        };
        job.ScheduleParallel();
    }
    
    partial struct TimerJob : IJobEntity
    {
        public float DeltaTime;
        public float TimerValue;
        public void Execute(ref LocalTransform transform, in RotatingSpeedComponentData rot)
        {
            FindPrimeNumber(100);
            TimerValue += DeltaTime;
        }

        public long FindPrimeNumber(int n)
        {
            var a = 0;
            while(a < n)
            {
                a++;
            }
            return a;
        }
    }
}
