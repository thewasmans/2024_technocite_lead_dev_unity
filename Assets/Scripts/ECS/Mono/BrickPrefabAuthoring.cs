using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEngine;

public class BrickPrefabAuthoring : MonoBehaviour
{
    public ArtworkSO Artwork;
    public float Scale;

    [Range(0,10)]
    public float SpeedAnimation;

    public Unity.Mathematics.Random random = Unity.Mathematics.Random.CreateFromIndex(0);
    public Vector3 SpawnPosition;

    private class Baker : Baker<BrickPrefabAuthoring>
    {
        public override void Bake(BrickPrefabAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            List<BrickTransform> brickTransforms = new ReplaceBrick(){
                BricksSOs = authoring.Artwork.PrefabsBricks
            }.Parse(authoring.Artwork.TransformBricks);
            // transforms = transforms.OrderBy(t => t.Transform.position.y);

            authoring.Artwork.PrefabsBricks.ToList().ForEach(p => p.Entity = GetEntity(p.Prefab, TransformUsageFlags.Dynamic));

            AddComponent(entity, new BrickDataComponent()
            {
                Scale = authoring.Artwork.ScaleBrick,
                SpeedAnimation = authoring.SpeedAnimation,
                Transforms = TransformBrickPool.CreateArrayPositionsBrickGropuped(brickTransforms),
                BrickPrefabs =  PrefabBrickPool.CreateArrayPrefabsBricks(authoring.Artwork.PrefabsBricks),
                SpawnPosition = authoring.SpawnPosition,
                Brick_1x1 = GetEntity(authoring.Artwork.PrefabsBricks[0].Prefab, TransformUsageFlags.Dynamic),
                Brick_1x2 = GetEntity(authoring.Artwork.PrefabsBricks[1].Prefab, TransformUsageFlags.Dynamic),
                Brick_2x1 = GetEntity(authoring.Artwork.PrefabsBricks[2].Prefab, TransformUsageFlags.Dynamic),
                Brick_2x2 = GetEntity(authoring.Artwork.PrefabsBricks[3].Prefab, TransformUsageFlags.Dynamic),
                Brick_2x4 = GetEntity(authoring.Artwork.PrefabsBricks[4].Prefab, TransformUsageFlags.Dynamic),
                Brick_4x2 = GetEntity(authoring.Artwork.PrefabsBricks[5].Prefab, TransformUsageFlags.Dynamic),
            });
        }
    }
}