using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcSpawn : MonoBehaviour
{
    public GameObject NpcToSpawn;

    void Start()
    {
        StartCoroutine(spawnNpc());
    }

    private IEnumerator spawnNpc()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(NpcToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
