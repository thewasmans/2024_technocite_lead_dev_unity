using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BrickPrefabAuthoring : MonoBehaviour
{
    public GameObject PrefabBrick;
    public GameObject artworkPoints;
    public float Scale;
    public bool EnableAnimation;

    private class Baker : Baker<BrickPrefabAuthoring>
    {
        public override void Bake(BrickPrefabAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            Transform[] transforms = authoring.artworkPoints.GetComponentsInChildren<Transform>();

            AddComponent(entity, new BrickDataComponent(){
                entityBrick = GetEntity(authoring.PrefabBrick, TransformUsageFlags.Dynamic),
                Scale = authoring.Scale,
                Positions = authoring.CreateHobbyPool(transforms),
                EnableAnimation = authoring.EnableAnimation
            });
        }
    }

    public BlobAssetReference<PositionsBrick> CreateHobbyPool(Transform[] transforms)
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