using Unity.Entities;
using UnityEngine;

public class SelectableAuthoring : MonoBehaviour
{
    private class Baker : Baker<SelectableAuthoring>
    {
        public override void Bake(SelectableAuthoring authoring)
        {
            //var entities = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery<RefRO<Selectable>>();
            // foreach (var (entity) in entities)
            // {
                
            // }
        }
    }
}