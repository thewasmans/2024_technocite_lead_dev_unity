using System;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using SystemAPI = Unity.Entities.SystemAPI;

partial class LegoArtworkSystem : SystemBase
{
    public float Time = 0;
    public int index = 0;
    
    protected override void OnUpdate()
    {
        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        //if(!brick.EnableAnimation) return;
        
        Time = Mathf.Clamp(Time + SystemAPI.Time.DeltaTime, 0, 1);
        
        var i = 0;
        foreach (var (aspect, entity) in SystemAPI.Query<LegoArtworkAspect>().WithEntityAccess())
        {
            LocalTransform localTransform = new LocalTransform(){
                    Position = aspect.data.ValueRO.point * Ease(Time),
                    Scale = brick.Scale * Ease(Time)
                };

            EntityManager.SetComponentData(entity,
                localTransform
            );
            i++;

            if(i >= index)
            {
                index++;
                Debug.Log("Index " + index);
                break;
            }
        }
    }

    public float Ease(float time)
    {
        return MathF.Sin(time);
    }
}

public readonly partial struct LegoArtworkAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<LegoArtworkDataComponent> data;
}