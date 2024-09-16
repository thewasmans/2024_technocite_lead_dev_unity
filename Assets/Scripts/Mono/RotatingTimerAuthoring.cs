using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class RotatingTimerAuthoring : MonoBehaviour
{
    public float rotationSpeed;
    public float rotationTimeout;

    private class Baker : Baker<RotatingTimerAuthoring>
    {
        public override void Bake(RotatingTimerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new RotatingSpeedComponentData{
               Value = authoring.rotationSpeed
            });
            
            AddComponent(entity, new TimerComponentData{
               Value = authoring.rotationTimeout
            });
        }
    }
}