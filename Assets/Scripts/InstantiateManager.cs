using System;
using Unity.Entities;
using Unity.Transforms;

partial class InstantiateManager : SystemBase
{
    public bool Instantiate = false;
    
    protected override void OnUpdate()
    {
        if(Instantiate)
        {
            Unity.Collections.NativeArray<Entity> entities = EntityManager.GetAllEntities();

            foreach (var entity in entities)
            {
                WaveMovingComponentData data = SystemAPI.GetSingleton<WaveMovingComponentData>();
                if(EntityManager.HasComponent<CubePositionComponentData>(entity))
                {
                    var dataEntity = EntityManager.GetComponentData<CubePositionComponentData>(entity);

                    EntityManager.SetComponentData(entity, new CubePositionComponentData(){
                        X = dataEntity.X,
                        Y = dataEntity.Y,
                        Amplitude = data.Amplitude,
                        Frequency = data.Frequency,
                    });
                }
            }
        }
        else
            InstantiateCubes();
    }


    public void InstantiateCubes()
    {
        Instantiate = true;
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

                EntityManager.AddComponentData(entity, new CubePositionComponentData(){
                    X = i,
                    Y = j,
                    Amplitude = data.Amplitude,
                    Frequency = data.Frequency,
                });
            }
        }
    }
}
