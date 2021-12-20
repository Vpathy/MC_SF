using System;
using System.Collections;
using System.Collections.Generic;

public class Timer
{
    public float _duration =0;
    float _countDown = 0;
    public event Action TimerComplete;
    public Timer(float duration, float countdown)
    {
        _duration = duration;
        _countDown = countdown;
    }


    public void Tick(float DeltaTime)
    {
        if(_duration <=0)
        {
            _duration = 0;
           // OnTimerComplete();
            return;
        }
        _duration -= DeltaTime;
    }

    public void OnTimerComplete()
    {
        TimerComplete?.Invoke();
    }
}
