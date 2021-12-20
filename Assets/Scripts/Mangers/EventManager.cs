using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventManager : GenericSingleton<EventManager>
{
    private Dictionary<string, UnityEvent> _eventDictionary = new Dictionary<string, UnityEvent>();

   

   public void Init()
    {
        if(_eventDictionary == null)
        {
            _eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }


    public static void AddListener(string _eventName,UnityAction _unityAction)
    {
        UnityEvent thisevent = null;
        if(Instance._eventDictionary.TryGetValue(_eventName,out thisevent))
        {
            thisevent.AddListener(_unityAction);
        }
        else
        {
            thisevent = new UnityEvent();
            Instance._eventDictionary.Add(_eventName, thisevent);
            thisevent.AddListener(_unityAction);

        }

    }

    public static void RemoveListener(string _eventName, UnityAction _unityAction)
    {
        UnityEvent thisevent = null;
        if (Instance._eventDictionary.TryGetValue(_eventName, out thisevent))
        {
            thisevent.RemoveListener(_unityAction);
        }
      

    }


    public static void TriggerEvent(string _eventName)
    {
        UnityEvent thisevent = null;
        if (Instance._eventDictionary.TryGetValue(_eventName, out thisevent))
        {
            thisevent?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
