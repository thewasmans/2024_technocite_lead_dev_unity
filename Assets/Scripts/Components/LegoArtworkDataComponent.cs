using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct LegoArtworkDataComponent : IComponentData
{
    public float3 Point;
    public int Id;
    public float TimeAnimation;
}
