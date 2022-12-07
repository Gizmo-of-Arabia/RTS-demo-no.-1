// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace RyanHipplesArchitecture.SO_Events
{
    public abstract class GameEvent<T> : GameEvent
    {
        // The list of listeners that this event will notify if it is raised, with or without a parameter.
        protected readonly List<IGameEventListener<T>> eventListenersWithParam =
            new List<IGameEventListener<T>>();

        public void Raise(T value)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();

            for (int i = eventListenersWithParam.Count - 1; i >= 0; i--)
                eventListenersWithParam[i].OnEventRaised(value);

            //Debug.Log("Active listeners, no param: " + eventListeners.Count);
            //Debug.Log("Active listeners, with param: " + eventListenersWithParam.Count);



        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!eventListenersWithParam.Contains(listener))
                eventListenersWithParam.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (eventListenersWithParam.Contains(listener))
                eventListenersWithParam.Remove(listener);
        }
    }

    [CreateAssetMenu(menuName = "Events/Game Event (no param)")]
    public class GameEvent : ScriptableObject
    {

        // The list of listeners that this event will notify if it is raised.
        protected readonly List<IGameEventListener> eventListeners =
            new List<IGameEventListener>();

        public void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();

            //Debug.Log("Active listeners, no param: " + eventListeners.Count);
        }

        public void RegisterListener(IGameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}