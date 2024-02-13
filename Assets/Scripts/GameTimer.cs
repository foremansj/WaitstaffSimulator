using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] float clockSpeed = 2f;
    float seconds;
    float minutes;
    float hours = 5f;
    float runningTime;

    private void Start() {
        runningTime = 0f;
    }
    private void Update() {
        if(Time.timeScale > 0) {
            runningTime += Time.deltaTime;
        }
    }

    public float GetRunningTime() {
        return runningTime;
    }

    private void RunningTimer()
    {
        seconds += Time.deltaTime * clockSpeed;
        if(seconds > 59.5f)
        {
            minutes += 1f;
            //seconds = -0.4f; this was used to clean up a slight hitch on the timer UI, maybe reconsider it
            seconds = 0f;
        }
        if(minutes > 59.5f)
        {
            hours += 1;
            minutes = 0f;
        }
        /*if(timeText != null)
        {
            timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds) + " PM";
        }

        if(hours + (minutes + (seconds / 60)) / 60 >= 11.5)
        {
            levelManager.LoadGameOver();
        }*/
    }

    public float GetCurrentGameTime()
    {
        return (hours / 60) / 60 + (minutes / 60) + seconds;
    }
}
