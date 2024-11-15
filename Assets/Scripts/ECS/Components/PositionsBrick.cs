using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;
using Unity.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

public struct TransformBrick
{
    public int IdBrickSO;
    public float3 Position;
    public float3 Rotation;
    public float Id; 
}

public struct BrickSOBlob
{
    public Entity Prefab;
    // public BrickType Type;
    public int2 Dimension;
    public float Id;
}

public struct PrefabBrickPool
{
    public BlobArray<BrickSOBlob> Prefabs;
    
    public static BlobAssetReference<PrefabBrickPool> CreateArrayPrefabsBricks(BrickSO[] prefabsBrick)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref PrefabBrickPool transformBricks = ref builder.ConstructRoot<PrefabBrickPool>();
        
        var arrayBuilder = builder.Allocate(
            ref transformBricks.Prefabs,
            prefabsBrick.Count()
        );

        for (int i = 0; i < prefabsBrick.Count(); i++)
        {
            arrayBuilder[i] = new BrickSOBlob()
            {
                // Type = prefabsBrick[i].Type,
                Prefab = prefabsBrick[i].Entity,
                Dimension = new int2(prefabsBrick[i].Dimension.x, prefabsBrick[i].Dimension.y),
                Id = prefabsBrick[i].Id
            };
        }
        
        var result = builder.CreateBlobAssetReference<PrefabBrickPool>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }
}

public struct TransformBrickPool
{
    public BlobArray<TransformBrick> TransformBricks;

    public static BlobAssetReference<TransformBrickPool> CreateArrayPositionsBrickGropuped(List<BrickTransform> transforms)
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
            arrayBuilder[i] = new TransformBrick()
            {
                Position = transforms[i].Transform.position,
                Rotation = Vector3.one * UnityEngine.Random.Range(-1.0f,1.0f),
                Id = i,
                IdBrickSO = transforms[i].BrickSO.Id,
            };
        }

        var result = builder.CreateBlobAssetReference<TransformBrickPool>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }
}