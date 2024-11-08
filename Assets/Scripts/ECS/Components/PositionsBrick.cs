using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using System.Linq;
using System.Collections.Generic;

public struct TransformBrick
{
    public float3 Position;
    public float3 Rotation;
    public float Id;   
}


public struct TransformBrickPool
{
    public BlobArray<TransformBrick> TransformBricks;
    
    public static BlobAssetReference<TransformBrickPool> CreateArrayPositionsBrick(Transform[] transforms)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref TransformBrickPool hobbyPool = ref builder.ConstructRoot<TransformBrickPool>();
        
        var arrayBuilder = builder.Allocate(
            ref hobbyPool.TransformBricks,
            transforms.Count()
        );

        for (int i = 0; i < transforms.Count(); i++)
        {
            arrayBuilder[i] = new TransformBrick() {
                Position = transforms[i].position,
                Id = i,
            };
        }

        var result = builder.CreateBlobAssetReference<TransformBrickPool>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }

    public static BlobAssetReference<TransformBrickPool> CreateArrayPositionsBrickGropuped(List<TransformID> transforms)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref TransformBrickPool transformBricks = ref builder.ConstructRoot<TransformBrickPool>();

        UnityEngine.Random.InitState(0);
        
        var arrayBuilder = builder.Allocate(
            ref transformBricks.TransformBricks,
            transforms.Count()
        );

        for (int i = 0; i < transforms.Count(); i++)
        {
            arrayBuilder[i] = new TransformBrick() {
                Position = transforms[i].Transform.position,
                Rotation = Vector3.one * UnityEngine.Random.Range(-1.0f,1.0f),
                Id = transforms[i].Id,
            };
        }

        var result = builder.CreateBlobAssetReference<TransformBrickPool>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }
}