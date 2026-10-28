using Unity.Entities;
using Unity.Mathematics;
using UnityEngine.InputSystem;
using UnityEngine;

[UpdateBefore(typeof(GridMoveSystem))]
public partial struct InputSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {

    }

    public void OnUpdate(ref SystemState state)
    {
        Keyboard keyboard = Keyboard.current;
        int2 direction = int2.zero;

        if(keyboard == null) 
        {
            return;
        }

        if(keyboard.wKey.wasPressedThisFrame || keyboard.upArrowKey.wasPressedThisFrame) 
        {
            direction = new int2(0, 1);
        }
        else if(keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame) 
        {
            direction = new int2(-1, 0);
        }
        else if(keyboard.sKey.wasPressedThisFrame || keyboard.downArrowKey.wasPressedThisFrame) 
        {
            direction = new int2(0, -1);
        }
        else if(keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame) 
        {
            direction = new int2(1, 0);
        }

        if(math.all(direction == int2.zero)) 
        {
            return;
        }

        foreach (var request in SystemAPI.Query<RefRW<MoveRequestComponent>>()
                    .WithAll<PlayerComponent>())
        {

            if(request.ValueRO.IsPending)
            {
                continue;
            }

            request.ValueRW.Direction = direction;
            request.ValueRW.IsPending = true;

        }
    }

    public void OnDestroy(ref SystemState state)
    {

    }
}
