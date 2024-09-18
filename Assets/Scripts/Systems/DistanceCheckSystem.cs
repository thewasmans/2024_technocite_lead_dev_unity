using Unity.Entities;
using Unity.Transforms;
using EntitiesAPI = Unity.Entities.SystemAPI;
using System.Diagnostics;
using UnityEngine;
using Unity.VisualScripting;

partial struct DistanceCheckSystem : ISystem
{
    // void onCreate(ref SystemState state) => Job = new DistanceCheckJob(){Entities = EntitiesAPI.Query<QueryEntities>() };

    void onUpdate(ref SystemState state)
    {
        UnityEngine.Debug.Log("start");
        
        foreach (var entity in EntitiesAPI.Query<QueryEntities>().WithEntityAccess())
        {
            UnityEngine.Debug.Log("sd");
        }
        UnityEngine.Debug.Log("end");
    }
}

public readonly partial struct QueryEntities : IAspect 
{
    public readonly RefRO<LocalTransform> transform;
}