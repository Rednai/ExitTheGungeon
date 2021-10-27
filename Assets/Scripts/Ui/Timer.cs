using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Text>().text = TimerController.instance.getTimer();
    }
}
