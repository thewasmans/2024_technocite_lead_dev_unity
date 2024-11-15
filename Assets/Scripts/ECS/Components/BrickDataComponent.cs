using Unity.Entities;
using Unity.Mathematics;

public struct BrickDataComponent : IComponentData
{
    public float Scale;
    public float SpeedAnimation;
    public int Steps;
    public bool EnableAnimation;
    public BlobAssetReference<TransformBrickPool> Transforms;
    public BlobAssetReference<PrefabBrickPool> BrickPrefabs;
    public float3 SpawnPosition;
    public Entity Brick_1x1;
    public Entity Brick_2x1;
    public Entity Brick_1x2;
    public Entity Brick_2x2;
    public Entity Brick_2x4;
    public Entity Brick_4x2;
}