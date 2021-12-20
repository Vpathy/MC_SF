using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class TimeManager : GenericSingleton<TimeManager>
{
    [SerializeField] private float _duration;
    [SerializeField] private float _initialCountDown;
    public Timer _timer; 
    // Start is called before the first frame update
   public void Init()
    {
        GameManager.Instance.OnTimerStart = new UnityAction(InitializeCountdown);
        EventManager.AddListener(CName.gameStart, InitializeCountdown);
        _duration = GameManager.Instance.TotalDuration + GameManager.Instance.CountDownDuration;
        _initialCountDown = GameManager.Instance.CountDownDuration;

    }

    private void OnEnable()
    {
        
    }


    private void OnDisable()
    {
       // _timer.TimerComplete -= OnComplete;
    }

    private void OnComplete()
    {
         // EventManager.TriggerEvent(CName.gameEND);
    }


    // Update is called once per frame
   public void TimerUpdate()
    {
        if(_timer!=null)
        {
          _timer.Tick(Time.deltaTime);
        }
    }

    public void InitializeCountdown()
    {
        _timer = new Timer(_duration, _initialCountDown);
    }

}
