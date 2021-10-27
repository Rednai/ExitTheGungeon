using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTime : MonoBehaviour
{
    public Slider slider;

    private float currentTime;
    private float maxTime;

    public void setTime(float time)
    {
        currentTime = time;
        maxTime = time;
        gameObject.SetActive(true);
    }

    public void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        slider.value = currentTime / maxTime;
    }
}
