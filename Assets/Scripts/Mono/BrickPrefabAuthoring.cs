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

            List<Transform> transforms = authoring.artworkPoints.GetComponentsInChildren<Transform>().ToList();
            List<Transform> transformsRandom = new List<Transform>();
            
            for (; transforms.Count() > 0;)
            {
                // Transform transform = transforms.ElementAt(authoring.random.NextInt(transforms.Count()));
                Transform transform = transforms.ElementAt(0);
                transforms.Remove(transform);
                transformsRandom.Add(transform);
            }

            AddComponent(entity, new BrickDataComponent(){
                entityBrick = GetEntity(authoring.PrefabBrick, TransformUsageFlags.Dynamic),
                Scale = authoring.Scale,
                Positions = authoring.CreateArrayPositionsBrick(transformsRandom.ToArray()),
                EnableAnimation = authoring.EnableAnimation
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
}

public struct PositionsBrick
{
    public BlobArray<float3> Values;
}