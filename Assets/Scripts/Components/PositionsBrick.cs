using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using System.Linq;
using System.Collections.Generic;

public struct PositionsBrick
{
    public float3 Position;   
    public float Id;   
}


public struct PositionsBrickPool
{
    public BlobArray<PositionsBrick> Positions;
    
    public static BlobAssetReference<PositionsBrickPool> CreateArrayPositionsBrick(Transform[] transforms)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref PositionsBrickPool hobbyPool = ref builder.ConstructRoot<PositionsBrickPool>();
        
        var arrayBuilder = builder.Allocate(
            ref hobbyPool.Positions,
            transforms.Count()
        );

        for (int i = 0; i < transforms.Count(); i++)
        {
            arrayBuilder[i] = new PositionsBrick() {
                Position = transforms[i].position,
                Id = i,
            };
        }

        var result = builder.CreateBlobAssetReference<PositionsBrickPool>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }

    public static BlobAssetReference<PositionsBrickPool> CreateArrayPositionsBrickGropuped(List<TransformID> transforms)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref PositionsBrickPool positionPools = ref builder.ConstructRoot<PositionsBrickPool>();
        
        var arrayBuilder = builder.Allocate(
            ref positionPools.Positions,
            transforms.Count()
        );

        for (int i = 0; i < transforms.Count(); i++)
        {
            arrayBuilder[i] = new PositionsBrick() {
                Position = transforms[i].Transform.position,
                Id = transforms[i].Id,
            };
        }

        var result = builder.CreateBlobAssetReference<PositionsBrickPool>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }
}