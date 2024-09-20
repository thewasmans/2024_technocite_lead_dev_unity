using Unity.Entities;
using Unity.Mathematics;

public struct BrickDataComponent : IComponentData
{
    public Entity entityBrick;
    public float Scale;
    public bool EnableAnimation;
    public BlobAssetReference<PositionsBrick> Positions;
    public BlobAssetReference<BrickId> Ids;
}