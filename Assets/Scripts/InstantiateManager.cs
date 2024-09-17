using System;
using Unity.Entities;
using Unity.Transforms;

partial class InstantiateManager : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;
        
        WaveMovingComponentData data = SystemAPI.GetSingleton<WaveMovingComponentData>();
        
        for (int i = 0; i < data.SizeGrid.X; i++)
        {
            for (int j = 0; j < data.SizeGrid.Y; j++)
            {
                var entity = EntityManager.Instantiate(data.Entity);
                
                EntityManager.SetComponentData(entity, new LocalTransform(){
                    Position = new Unity.Mathematics.float3(i, MathF.Sin(i * j), j),
                    Rotation = Unity.Mathematics.quaternion.identity,
                    Scale = 1
                });

                EntityManager.AddComponentData<CubePositionComponentData>(entity, new CubePositionComponentData(){
                    X = i,
                    Y = j,
                });
            }
        }
    }
}
