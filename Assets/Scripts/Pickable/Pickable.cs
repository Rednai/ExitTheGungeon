using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickable : MonoBehaviour
{
    public enum Type
    {
        Heart,
        Weapon
    }
    public Type type;
    public float timeBeforeDespawn = 8f;
    public float DelayBetweenFlash = 0.5f;
    public GameObject ItemDespawn;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DespawnAfterXSeconds());
    }

    private IEnumerator DespawnAfterXSeconds()
    {
        float timeLeft = timeBeforeDespawn / 2;

        yield return new WaitForSeconds(timeLeft);
        
        while (timeLeft > 0)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            timeLeft -= DelayBetweenFlash;
            yield return new WaitForSeconds(DelayBetweenFlash);
        }
        GameObject impact = Instantiate(ItemDespawn, transform.position, Quaternion.identity);
        Destroy(impact, 0.5f);
        Destroy(gameObject);
    }
}
