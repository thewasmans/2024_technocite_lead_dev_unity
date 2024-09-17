using System.Numerics;
using Unity.Entities;
using UnityEngine;

public struct CubePositionComponentData : IComponentData
{
    public float X;
    public float Y;
    public float Amplitude;
    public float Frequency;
}