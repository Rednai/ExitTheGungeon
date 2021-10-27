using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputPc : MonoBehaviour, IPlayerInput
{
    public Vector2 movementInputVector { get; private set; }
    public Vector3 aimPointVector { get; private set; }
    public Action<Vector3> OnRollEvent { get; set; }
    public Action OnShootEvent { get; set; }
    public Action OnPauseEvent { get; set; }

    void Update()
    {
        GetAimPoint();
        GetMovementInput();
        GetRollInput();
        GetShootInput();
        GetPauseInput();
    }

    void GetMovementInput()
    {
        movementInputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void GetAimPoint()
    {
        Vector3 newAimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newAimPoint.z = 0f;
        aimPointVector = newAimPoint;
    }

    void GetRollInput()
    {
        if (Input.GetButtonDown("Roll"))
        {
            OnRollEvent(aimPointVector);
        }
    }

    void GetShootInput()
    {
        if (Input.GetButton("Shoot"))
        {
            OnShootEvent();
        }
    }

    void GetPauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPauseEvent();
        }
    }
}
