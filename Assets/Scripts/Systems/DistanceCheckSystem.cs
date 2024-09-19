using Unity.Entities;
using Unity.Transforms;
using EntitiesAPI = Unity.Entities.SystemAPI;
using UnityEngine;
using System.Linq;

partial class DistanceCheckSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;

        // foreach (var aspect in EntitiesAPI.Query<LegoArtworkAspect>())
        // {
        //     EntityManager.AddComponent
        // }

        // EntityManager.GetAllEntities().

        foreach (var entity in EntityManager.GetAllEntities())
        {
            Debug.Log("enity");
            if(EntityManager.HasComponent<LocalTransform>(entity) && EntityManager.HasComponent<LegoArtworkDataComponent>(entity))
            {
                Debug.Log("true");
                var data = EntityManager.GetComponentData<LegoArtworkDataComponent>(entity);

                EntityManager.AddComponentData(entity, new LocalTransform(){
                    Position = data.point
                });
            }
        }
    }
}

public readonly partial struct LegoArtworkAspect : IAspect
{
    public readonly RefRO<LocalTransform> transform;
    public readonly RefRO<LegoArtworkDataComponent> data;
}