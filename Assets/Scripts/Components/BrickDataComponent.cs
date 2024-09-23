using Unity.Entities;
using Unity.Mathematics;

public struct BrickDataComponent : IComponentData
{
    public Entity entityBrick;
    public float Scale;
    public int Steps;
    public bool EnableAnimation;
    public BlobAssetReference<PositionsBrick> Positions;
}