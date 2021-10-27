using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = GameManager.instance.enemiesKilled.ToString();
    }
}