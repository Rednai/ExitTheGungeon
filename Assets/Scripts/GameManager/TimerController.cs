using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    public Text timerText;

    private TimeSpan timePlaying;
    private bool timerGoing = false;
    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;
        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    public string getTimer()
    {
        elapsedTime += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        return timePlaying.ToString("mm':'ss'.'ff");
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            timerText.text = getTimer();
            yield return null;
        }
    }
}
