using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using UnityEngine;

public struct TransformID
{
    public float Id;
    public Transform Transform;
}

public class BrickPrefabAuthoring : MonoBehaviour
{
    public GameObject[] PrefabBrick;
    public GameObject artworkPoints;
    public float Scale;
    public Unity.Mathematics.Random random = Unity.Mathematics.Random.CreateFromIndex(0);

    private class Baker : Baker<BrickPrefabAuthoring>
    {
        public override void Bake(BrickPrefabAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            Transform[] transforms = authoring.artworkPoints.GetComponentsInChildren<Transform>();
            transforms = transforms.OrderBy(t => t.position.y).ToArray();
            IEnumerable<IGrouping<float, Transform>> transformGroupByY = transforms.GroupBy(t => t.position.y);

            List<TransformID> transformsID = new List<TransformID>();

            foreach (var group in transformGroupByY)
            {
                foreach (var transform in group)
                {
                    transformsID.Add(new TransformID(){
                        Id = group.Key,
                        Transform = transform
                    });
                }
            }
 
            AddComponent(entity, new BrickDataComponent(){ 
                entityBrickRed = GetEntity(authoring.PrefabBrick[0], TransformUsageFlags.Dynamic),
                entityBrickGreen = GetEntity(authoring.PrefabBrick[1], TransformUsageFlags.Dynamic),
                entityBrickYellow = GetEntity(authoring.PrefabBrick[2], TransformUsageFlags.Dynamic),
                Scale = authoring.Scale,
                Positions = PositionsBrickPool.CreateArrayPositionsBrickGropuped(transformsID),
            });
        }
    }
}