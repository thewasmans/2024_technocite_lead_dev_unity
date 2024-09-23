using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using System.Linq;
using System;

public struct PositionsBrick
{
    public BlobArray<float3> Values;
    
    public static BlobAssetReference<PositionsBrick> CreateArrayPositionsBrick(Transform[] transforms)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref PositionsBrick hobbyPool = ref builder.ConstructRoot<PositionsBrick>();
        
        BlobBuilderArray<float3> arrayBuilder = builder.Allocate(
            ref hobbyPool.Values,
            transforms.Count()
        );

        for (int i = 0; i < transforms.Count(); i++)
        {
            arrayBuilder[i] = transforms[i].position;
        }

        var result = builder.CreateBlobAssetReference<PositionsBrick>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }
}