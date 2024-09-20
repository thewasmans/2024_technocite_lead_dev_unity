using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class LegoArtworkAuthoring : MonoBehaviour
{
    public GameObject artworkPoints;
    public NativeArray<float> points = new NativeArray<float>();

    private class Baker : Baker<LegoArtworkAuthoring>
    {
        public override void Bake(LegoArtworkAuthoring authoring)
        {
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;

            Transform[] transforms = authoring.artworkPoints.GetComponentsInChildren<Transform>();
            
            // foreach(var transform in transforms)
            // {
            //     Entity entity = manager.CreateEntity();
            //     manager.AddComponentData(entity, new LegoArtworkDataComponent(){point = new float3(transform.position)});
            //     manager.AddComponentData(entity, new LocalTransform(){Position = new float3(transform.position)});
            // }
        }
    }
}