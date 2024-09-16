using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TweenColor : MonoBehaviour
{
    public Color colorA;
    public Color colorB;
    private class Baker : Baker<TweenColor>
    {
        public override void Bake(TweenColor authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ColorTweenComponentData{
               colorA = new float4(authoring.colorA.r, authoring.colorA.g, authoring.colorA.b, authoring.colorA.a),
               colorB = new float4(authoring.colorA.r, authoring.colorA.g, authoring.colorA.b, authoring.colorA.a)
            });
        }
    }
}