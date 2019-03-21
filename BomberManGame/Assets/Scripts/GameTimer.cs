using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private Text textClock;
    float TimerDuration;
    float TimerStartTime;
    public float TimeAllowed=240f;
    void Start()
    {
        textClock = GetComponent<Text>();
        TimerReset(TimeAllowed);
 
    }

    void Update()
    {
        string msg = "Time up.";
        int timeLeft = (int)TimeRemaining();
        if (timeLeft > 0)
        {
            msg = LeadingZero(timeLeft);
        }
        textClock.text = msg;

    }

    void TimerReset(float delay)
    {
        TimerDuration = delay;
        TimerStartTime = Time.time;
    }

    float TimeRemaining()
    {
        float elapsedTime = Time.time - TimerStartTime;
        float timeLeft = TimerDuration - elapsedTime;
        return timeLeft;
    }

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
}
