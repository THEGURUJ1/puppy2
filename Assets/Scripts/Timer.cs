/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Timer : MonoBehaviour
{
    public static Timer instance;
    bool TimerActive= false;
    int currentTime=120;
    public GameObject currentTimeText;
    void Start()
    {
        currentTimeText.GetComponent<Text>().text = currentTime.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(TimerActive==true && currentTime>0)
        {
            StartCoroutine(TimerTake());
        }
        }

    public void StartStopwatch()
    {
        TimerActive=true;
    }
    IEnumerator TimerTake()
    {
        
        yield return new WaitForSeconds(1);
        currentTime-=1;
        currentTimeText.GetComponent<Text>().text= currentTime.ToString();

    }
}*/
