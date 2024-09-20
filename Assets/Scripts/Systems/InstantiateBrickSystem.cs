using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial class InstantiateBrickSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;

        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        for (int i = 0; i < brick.Positions.Value.Values.Length; i++)
        {
            Unity.Mathematics.float3 position = brick.Positions.Value.Values[i];
            
            Entity entity = EntityManager.Instantiate(brick.entityBrick);

            EntityManager.AddComponentData(entity, new LegoArtworkDataComponent(){
                point = position
            });
        }
    }
}