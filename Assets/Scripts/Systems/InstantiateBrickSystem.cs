using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial class InstantiateBrickSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;

        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();

        UnityEngine.Random.InitState(0);
        
        for (int i = 0; i < brick.Transforms.Value.TransformBricks.Length; i++)
        {
            float3 position = brick.Transforms.Value.TransformBricks[i].Position;
            float3 rotation = brick.Transforms.Value.TransformBricks[i].Rotation;
            float id = brick.Transforms.Value.TransformBricks[i].Id;

            Entity entity;
            var r = UnityEngine.Random.Range(0, 3);

            if(r == 0)
                entity = EntityManager.Instantiate(brick.entityBrickRed);
            else if(r == 1)
                entity = EntityManager.Instantiate(brick.entityBrickGreen);
            else
                entity = EntityManager.Instantiate(brick.entityBrickYellow);

            Vector3 eulerAngles = UnityEngine.Random.value * Vector3.forward + 
            UnityEngine.Random.value * Vector3.right + 
            UnityEngine.Random.value * Vector3.up;

            EntityManager.AddComponentData(entity, new LegoArtworkDataComponent(){
                Point = position,
                IdGroup = id,
                Index = i + UnityEngine.Random.Range(0,1.0f),
                IndexNormalized = i / brick.Transforms.Value.TransformBricks.Length,
                Rotation = rotation
            });
            
            EntityManager.AddComponentData(entity,  new LocalTransform(){
                Position = new float3(0,0,0),
                Scale = 0,
                Rotation = quaternion.identity
            });
        }
    }
}