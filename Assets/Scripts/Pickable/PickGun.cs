using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGun : Pickable
{
    public string weaponName;
    public float timeEquiped = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.instance.Play("Reload");
            collision.gameObject.GetComponent<Player>().equipWeapon(weaponName, timeEquiped);
            Destroy(gameObject);
        }
    }
}
