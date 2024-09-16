using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct ColorTweenComponentData : IComponentData
{
    public float4 colorA;
    public float4 colorB;
}
