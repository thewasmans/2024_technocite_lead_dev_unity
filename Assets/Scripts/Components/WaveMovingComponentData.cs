using System.Numerics;
using Unity.Entities;
using UnityEngine;

public struct WaveMovingComponentData : IComponentData
{
    public System.Numerics.Vector2 SizeGrid;
    public float SpeedWave;
    public float Amplitude;
    public float Frequency;
    public Entity Entity;
}