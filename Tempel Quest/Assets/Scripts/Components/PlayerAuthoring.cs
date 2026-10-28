using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    private class Baker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<PlayerComponent>(entity);

            AddComponent(entity, new MoveRequestComponent
            {
                Direction = Unity.Mathematics.int2.zero,
                IsPending = false
            });
        }
    }
}
