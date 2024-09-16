using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial struct RotatingSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        
    }
    
    public void OnUpdate(ref SystemState state)
    {
        RotatingJob job = new RotatingJob(){
            DeltaTime = SystemAPI.Time.DeltaTime
        };
        job.ScheduleParallel();

        // Debug.Log("OnUpdate");
        // foreach (var (transform, rotation) 
        //     in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotatingSpeedComponentData>>()){
        //         Debug.Log("bitch");
        //         transform.ValueRW = transform.ValueRO.RotateY(rotation.ValueRO.Value * SystemAPI.Time.DeltaTime);
        // };
    }
    
    public void OnDestroy(ref SystemState state)
    {
        
    }

    partial struct RotatingJob : IJobEntity
    {
        public float DeltaTime;
        public void Execute(ref LocalTransform transform, in RotatingSpeedComponentData rot)
        {
            FindPrimeNumber(100);
            Debug.Log(rot.Value);
            Debug.Log(DeltaTime);
            transform = transform.RotateY(rot.Value * DeltaTime);
            Debug.Log("Execute");
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
