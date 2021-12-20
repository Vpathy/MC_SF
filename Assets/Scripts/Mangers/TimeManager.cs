/***********************************************************************************************
 *Time Manager for Timer control
 *Reads duration which is addition of both countdown duration and gameplay duration
 * Tick is the udpate loop than will be run in Game Manager
 ************************************************************************************************/




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

    /// <summary>
    /// Currently not needed
    /// </summary>

    //private void OnEnable()
    //{
        
    //}


    ////private void OnDisable()
    ////{
    //   // _timer.TimerComplete -= OnComplete;
    //}

    //private void OnComplete()
    //{
    //     // EventManager.TriggerEvent(CName.gameEND);
    //}


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
