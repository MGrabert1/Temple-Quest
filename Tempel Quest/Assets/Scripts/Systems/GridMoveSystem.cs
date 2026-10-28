using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(InputSystem))]
public partial struct GridMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {

    }

    public void OnUpdate(ref SystemState state)
    {
       foreach (var (transform, request) in 
                SystemAPI.Query<
                    RefRW<LocalTransform>,
                    RefRW<MoveRequestComponent>>()
                    .WithAll<PlayerComponent>())
       {

            if(!request.ValueRO.IsPending)
            {
                continue;
            }

            int2 currentPosition = (int2)transform.ValueRO.Position.xy;
            int2 targetPosition = currentPosition + request.ValueRO.Direction;

            transform.ValueRW.Position.x = targetPosition.x;
            transform.ValueRW.Position.y = targetPosition.y;

            request.ValueRW.IsPending = false;
       }
    }

    public void OnDestroy(ref SystemState state)
    {

    }
}
