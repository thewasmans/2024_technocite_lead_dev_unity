using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BrickDataComponent : IComponentData
{
    public Entity entityBrickRed;
    public Entity entityBrickYellow;
    public Entity entityBrickGreen;
    public float Scale;
    public float SpeedAnimation;
    public int Steps;
    public bool EnableAnimation;
    public BlobAssetReference<TransformBrickPool> Transforms;
    public float3 SpawnPosition;
}