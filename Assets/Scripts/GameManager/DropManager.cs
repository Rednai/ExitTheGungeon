using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    [System.Serializable]
    public struct Object
    {
        public GameObject pickable;
        public float dropChance;
    }
    public Object[] objects;

    static public DropManager instance;

    void Start()
    {
        instance = this;
    }

    public void DropAnItem(Vector3 position)
    {
        foreach (Object obj in objects)
        {
            float randomValue = Random.value;
            if (randomValue < obj.dropChance)
            {
                Instantiate(obj.pickable, position, Quaternion.identity);
                return;
            }
        }
    }
}
