using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rollSpeed = 20f;
    public float rollDeceleration = 2f;
    public float rollSpeedStop = 5f;

    public enum State
    {
        Normal,
        Roll,
    }
    public State state { get; private set; }
    public Action playRollAnimation;

    private Rigidbody2D rb;
    private Vector2 rollDirection;
    private float currentRollSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Normal;
    }

    public void managePlayerMovement(Vector2 movementInput)
    {
        switch (state)
        {
            case State.Normal :
                movePlayer(movementInput);
                break;
            case State.Roll :
                handleRollSliding();
                break;
        }
    }

    public void movePlayer(Vector2 movementInput)
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }

    public void handleRoll(Vector3 aimPoint)
    {
        if (state == State.Normal)
        {
            rollDirection = (aimPoint - transform.position).normalized;
            state = State.Roll;
            currentRollSpeed = rollSpeed;
            playRollAnimation();
        }
    }

    private void handleRollSliding()
    {
        rb.MovePosition(rb.position + rollDirection * currentRollSpeed * Time.fixedDeltaTime);
        currentRollSpeed -= currentRollSpeed * rollDeceleration * Time.fixedDeltaTime;
        if (currentRollSpeed < rollSpeedStop)
        {
            state = State.Normal;
        }
    }

    public void freezeMovement()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
