using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

partial class InstantiateBrickSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Enabled = false;

        BrickDataComponent brick = SystemAPI.GetSingleton<BrickDataComponent>();
        
        for (int i = 0; i < brick.Positions.Value.Values.Length; i++)
        {
            float3 position = brick.Positions.Value.Values[i];
            
            Entity entity = EntityManager.Instantiate(brick.entityBrick);

            EntityManager.AddComponentData(entity, new LegoArtworkDataComponent(){
                Point = position,
                Id = i
            });
            
            EntityManager.AddComponentData(entity,  new LocalTransform(){
                Position = new float3(0,0,0),
                Scale = 0,
                Rotation = quaternion.identity
            });
        }
    }
}