using Unity.Entities;
using UnityEngine;

public struct BrickDataComponent : IComponentData
{
    public Entity entityBrickRed;
    public Entity entityBrickYellow;
    public Entity entityBrickGreen;
    public float Scale;
    public float Timing;
    public int Steps;
    public bool EnableAnimation;
    public BlobAssetReference<PositionsBrickPool> Positions;
}