using UnityEngine;

public class DistanceCheckAuthoring : MonoBehaviour
{
    public float MaxDistance;

    private class Baker : Unity.Entities.Baker<DistanceCheckAuthoring>
    {
        public override void Bake(DistanceCheckAuthoring authoring)
        {
            var entity = GetEntity(Unity.Entities.TransformUsageFlags.Dynamic);

            AddComponent(entity, new DistanceCheckDataComponent(){
                MaxDistance = authoring.MaxDistance
            });
        }
    }
}