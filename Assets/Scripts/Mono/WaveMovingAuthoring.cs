using Unity.Entities;
using UnityEngine;

public class WaveMovingAuthoring : MonoBehaviour
{
    public Vector2 SizeGrid;
    public float SpeedWave;
    public float Amplitude;
    public float Frequency;
    public GameObject Prefab;

    private class Baker : Baker<WaveMovingAuthoring>
    {
        public override void Bake(WaveMovingAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new WaveMovingComponentData{
                Amplitude = authoring.Amplitude,
                Frequency = authoring.Frequency,
                SizeGrid = new System.Numerics.Vector2(authoring.SizeGrid.x, authoring.SizeGrid.y),
                SpeedWave = authoring.SpeedWave,
                Entity = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
            });
        } 
    }
}
