using Unity.Entities;
using Unity.Mathematics;

public struct MoveRequestComponent : IComponentData
{
    public int2 Direction;
    public bool IsPending;
}
