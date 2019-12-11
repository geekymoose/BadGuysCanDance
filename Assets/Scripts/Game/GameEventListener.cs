using UnityEngine;
using UnityEngine.Events;

// Event system inspired from https://www.youtube.com/watch?v=raQ3iHhE_Kk
// Unite Austin 2017 - Game Architecture with Scriptable Objects

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
