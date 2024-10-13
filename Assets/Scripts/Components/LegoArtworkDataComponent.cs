using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct LegoArtworkDataComponent : IComponentData
{
    /// <summary>
    /// 3D Position in the World Space
    /// </summary>
    public float3 Point;

    /// <summary>
    /// Brick are grouped by criteria define early. Per example Grouped by the y position
    /// </summary>
    public float IdGroup; 

    /// <summary>
    /// Brick index from all bricks
    /// </summary>
    public float Index;
    
    /// <summary>
    /// Normalize index form 0 to 1
    /// </summary>
    public float IndexNormalized;
    
    public float3 Rotation;
}
