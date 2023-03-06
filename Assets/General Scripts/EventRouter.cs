using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/EventRouter")]
public class EventRouter : ScriptableObject {
    // Allow observers to subscribe to event (OnEvent += )
    public UnityAction OnEvent;

    public void Notify() { 
        // Notify all observers of event
        OnEvent?.Invoke();
    }
}