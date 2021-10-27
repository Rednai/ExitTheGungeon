using System;
using UnityEngine;

public interface IPlayerInput
{
    Vector2 movementInputVector { get; }
    Vector3 aimPointVector { get; }

    Action<Vector3> OnRollEvent { get; set; }
    Action OnShootEvent { get; set; }
    Action OnPauseEvent { get; set; }
}
