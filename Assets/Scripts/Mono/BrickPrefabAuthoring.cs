using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using UnityEngine;

public class BrickPrefabAuthoring : MonoBehaviour
{
    public GameObject PrefabBrick;
    public GameObject artworkPoints;
    public float Scale;
    public float Timing;
    public int Steps;
    public bool EnableAnimation;
    public Unity.Mathematics.Random random = Unity.Mathematics.Random.CreateFromIndex(0);

    private class Baker : Baker<BrickPrefabAuthoring>
    {
        public override void Bake(BrickPrefabAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            Transform[] transforms = authoring.artworkPoints.GetComponentsInChildren<Transform>();
            transforms = transforms.OrderBy(t => t.position.y).ToArray();

            AddComponent(entity, new BrickDataComponent(){
                entityBrick = GetEntity(authoring.PrefabBrick, TransformUsageFlags.Dynamic),
                Scale = authoring.Scale,
                Positions = PositionsBrick.CreateArrayPositionsBrick(transforms),
                EnableAnimation = authoring.EnableAnimation,
                Steps = authoring.Steps,
                Timing = authoring.Timing
            });
        }
    }
}