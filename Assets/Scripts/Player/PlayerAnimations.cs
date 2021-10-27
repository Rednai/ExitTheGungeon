using System;
using System.Collections;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private GameObject aim;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        aim = GameObject.Find("Aim");
    }

    public void setSpeed(Vector2 movementInput)
    {
        animator.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    public void playRoll()
    {
        AudioManager.instance.Play("Roll");
        animator.SetTrigger("Roll");
        StartCoroutine(hideWeapon());
    }

    public void playDeath()
    {
        AudioManager.instance.Play("Death");
        animator.SetTrigger("Death");
        aim.SetActive(false);
        StartCoroutine(displayGameOver());
    }

    public void playHit()
    {
        AudioManager.instance.Play("Hit");
        animator.SetTrigger("Hit");
    }

    private IEnumerator hideWeapon()
    {
        yield return new WaitForEndOfFrame();
        aim.SetActive(false);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length - 0.1f);
        aim.SetActive(true);
    }

    private IEnumerator displayGameOver()
    {
        yield return new WaitForSeconds(0.1f);
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return new WaitForEndOfFrame();
        }
        InGameMenu.instance.GameOver();
    }
}
