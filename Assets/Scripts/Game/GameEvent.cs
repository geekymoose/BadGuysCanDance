using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 1)]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; --i)
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        if(!this.listeners.Contains(listener))
        {
            this.listeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListener listener)
    {
        if (this.listeners.Contains(listener))
        {
            this.listeners.Remove(listener);
        }
    }
}
