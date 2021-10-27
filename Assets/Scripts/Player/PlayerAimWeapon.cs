using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAimWeapon : MonoBehaviour
{
    public string defaultWeapon = "Glock";

    private GameObject aim;
    private Transform aimTransform;
    private Transform spriteTransform;
    private bool facingRight = true;

    void Awake()
    {
        aim = GameObject.Find("Aim");
        aimTransform = aim.transform;
        spriteTransform = transform.Find("Sprite");
    }

    public void playerAim(Vector3 aimPoint)
    {
        Vector3 aimDirection = (aimPoint - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        if ((angle > 90 || angle < -90) && facingRight)
        {
            flip();
        }
        if (angle < 90 && angle > -90 && !facingRight) {
            flip();
        }
    }

    private void flip()
    {
        // Sprite flip
        Vector3 scale = spriteTransform.localScale;
        scale.x *= -1;
        spriteTransform.localScale = scale;

        // Weapon flip
        scale = aimTransform.localScale;
        scale.y *= -1;
        aimTransform.localScale = scale;

        facingRight = !facingRight;
    }

    public void handleShoot(PlayerMovement.State state)
    {
        if (state == PlayerMovement.State.Normal)
        {
            aim.GetComponentInChildren<IGun>()?.Shoot();
        }
    }

    public void equipWeapon(string name, float time)
    {
        StopAllCoroutines();

        unequipCurrentWeapon();
        aim.transform.Find(name).gameObject.SetActive(true);

        StartCoroutine(resetWeapon(time));
    }

    private void unequipCurrentWeapon()
    {
        GameObject currentWeapon = null;

        for (int i = 0; i < aim.transform.childCount; i++)
            if (aim.transform.GetChild(i).gameObject.activeSelf == true)
                currentWeapon = aim.transform.GetChild(i).gameObject;

        if (!currentWeapon)
            return;
        currentWeapon.SetActive(false);
    }

    private IEnumerator resetWeapon(float time)
    {
        yield return new WaitForSeconds(time);
        unequipCurrentWeapon();
        aim.transform.Find(defaultWeapon).gameObject.SetActive(true);
    }
}
