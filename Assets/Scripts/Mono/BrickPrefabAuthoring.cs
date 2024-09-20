using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using UnityEngine;

public class BrickPrefabAuthoring : MonoBehaviour
{
    public GameObject PrefabBrick;
    public GameObject artworkPoints;
    public float Scale;
    public bool EnableAnimation;
    public Unity.Mathematics.Random random = Unity.Mathematics.Random.CreateFromIndex(0);

    private class Baker : Baker<BrickPrefabAuthoring>
    {
        public override void Bake(BrickPrefabAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            Transform[] transforms = authoring.artworkPoints.GetComponentsInChildren<Transform>();

            List<int> ids = transforms.Select( (e,i) => i).ToList();
            List<int> idsRandom = new List<int>();

            for (; ids.Count() > 0;)
            {
                int index = authoring.random.NextInt(ids.Count());
                int id = ids.ElementAt(index);
                ids.RemoveAt(index);
                idsRandom.Add(id);
            }

            AddComponent(entity, new BrickDataComponent(){
                entityBrick = GetEntity(authoring.PrefabBrick, TransformUsageFlags.Dynamic),
                Scale = authoring.Scale,
                Positions = authoring.CreateArrayPositionsBrick(transforms),
                EnableAnimation = authoring.EnableAnimation,
                Ids = authoring.CreateArrayBrickId(idsRandom)
            });
        }
    }

    public BlobAssetReference<PositionsBrick> CreateArrayPositionsBrick(Transform[] transforms)
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

    public BlobAssetReference<BrickId> CreateArrayBrickId(List<int> idsRandom)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref PositionsBrick hobbyPool = ref builder.ConstructRoot<PositionsBrick>();
        
        BlobBuilderArray<float3> arrayBuilder = builder.Allocate(
            ref hobbyPool.Values,
            idsRandom.Count()
        ); 

        for (int i = 0; i < idsRandom.Count(); i++)
        {
            arrayBuilder[i] = idsRandom[i];
        }

        var result = builder.CreateBlobAssetReference<BrickId>(Allocator.Persistent);
        builder.Dispose();
        return result;
    }
}

public struct PositionsBrick
{
    public BlobArray<float3> Values;
}

public struct BrickId
{
    public BlobArray<float> Ids;
}