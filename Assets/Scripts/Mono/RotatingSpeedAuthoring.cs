using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotatingSpeedAuthoring : MonoBehaviour
{
    public float rotationSpeed;

    private class Baker : Baker<RotatingSpeedAuthoring>
    {
        public override void Bake(RotatingSpeedAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new RotatingSpeedComponentData{
               Value = authoring.rotationSpeed
            });
        }
    }
}
