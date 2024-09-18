using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public EntityManager mgr;
    public GameObject Character;
    public UnityEngine.Ray Ray;

    void Start()
    {
        mgr = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var query = mgr.CreateEntityQuery(typeof(PhysicsWorldSingleton));
            var singleton = query.GetSingleton<PhysicsWorldSingleton>();

            var collisionWorld = singleton.CollisionWorld;

            var raycastInput = new RaycastInput()
            {
                Start = Ray.origin,
                End = Ray.GetPoint(999),
                // Filter = new CollisionFilter
                // {
                //     BelongsTo = ~0u,
                //     CollidesWith = 1u << 6, //6 offset for 1 bit 
                //     // GroupIndex = 0 
                // }
            };

            if(collisionWorld.CastRay(raycastInput, out Unity.Physics.RaycastHit hit))
            {
                Debug.Log("");
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Ray.origin, Ray.direction*10);
    }
}

public struct Selectable : IComponentData, IEnableableComponent
{

}
