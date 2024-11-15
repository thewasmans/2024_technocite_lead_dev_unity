using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using System.Linq;
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

            if(brick.Transforms.Value.TransformBricks[i].IdBrickSO == 1)
            {
                entity = EntityManager.Instantiate(brick.Brick_1x2);
            }
            else if(brick.Transforms.Value.TransformBricks[i].IdBrickSO == 2)
            {
                entity = EntityManager.Instantiate(brick.Brick_2x1);
            }
            else if(brick.Transforms.Value.TransformBricks[i].IdBrickSO == 3)
            {
                entity = EntityManager.Instantiate(brick.Brick_2x2);
            }
            else if(brick.Transforms.Value.TransformBricks[i].IdBrickSO == 4)
            {
                entity = EntityManager.Instantiate(brick.Brick_2x4);
            }
            else if(brick.Transforms.Value.TransformBricks[i].IdBrickSO == 5)
            {
                entity = EntityManager.Instantiate(brick.Brick_4x2);
            }
            else
            {
                entity = EntityManager.Instantiate(brick.Brick_1x1);
            }
            
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
            
            Debug.Log("entity ins"+ entity);
        }
    }
}