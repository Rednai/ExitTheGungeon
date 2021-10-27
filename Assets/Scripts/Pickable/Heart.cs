using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heart : Pickable
{
    public float healAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
