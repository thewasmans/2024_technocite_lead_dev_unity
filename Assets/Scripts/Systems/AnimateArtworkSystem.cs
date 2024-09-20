using System;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using SystemAPI = Unity.Entities.SystemAPI;

partial class AnimateArtworkSystem : SystemBase
{
    public float Time = 0;
    public int index = 0;
    
    protected override void OnUpdate()
    {
        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        if(!brick.EnableAnimation) return;
        
        Time = Mathf.Clamp(Time + SystemAPI.Time.DeltaTime, 0, 1);
        
        // var i = 0;
        // foreach (var (aspect, entity) in SystemAPI.Query<LegoArtworkAspect>().WithEntityAccess())
        // {
        //     LocalTransform localTransform = new LocalTransform(){
        //             Position = aspect.data.ValueRO.point * Ease(Time),
        //             Scale = brick.Scale * Ease(Time)
        //         };

        //     EntityManager.SetComponentData(entity,
        //         localTransform
        //     );
        //     i++;

        //     if(i >= index)
        //     {
        //         index++;
        //         break;
        //     }
        // }
        new AnimateJob(){
            brick = brick,
            Time = Time
        }.ScheduleParallel();
    }

    public partial struct AnimateJob : IJobEntity 
    {
        public BrickDataComponent brick;
        public float Time;

        public void Execute(ref LocalTransform transform, in LegoArtworkDataComponent aspect)
        {
            transform = new LocalTransform(){
                    Position = aspect.point * Ease(Time),
                    Scale = brick.Scale * Ease(Time)
                };

            // EntityManager.SetComponentData(entity,
            //     localTransform
            // );
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