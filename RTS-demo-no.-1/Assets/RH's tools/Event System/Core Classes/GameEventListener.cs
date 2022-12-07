// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
//

namespace RyanHipplesArchitecture.SO_Events
{
    public abstract class GameEventListener<T, E, UER> : MonoBehaviour,
    IGameEventListener<T> where E : GameEvent<T> where UER : UnityEvent<T>
    {



        [Tooltip("Event to register with, has a parameter.")]
        [SerializeField] private E typedEvent;
        public E TypedEvent { get => typedEvent; set => typedEvent = value; }

        [Tooltip("Response to invoke when Event is raised, has a parameter.")]
        [SerializeField] private UER typedResponse;

        private void OnEnable()
        {
            TypedEvent?.RegisterListener(this);

        }

        private void OnDisable()
        {
            TypedEvent?.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            typedResponse.Invoke(value);


        }


    }


    [AddComponentMenu("GameEventListeners/GameEventListener (no param)")]
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [Tooltip("Event to register with.")]
        [SerializeField] protected GameEvent _event;

        [Tooltip("Response to invoke when Event is raised.")]
        [SerializeField] protected UnityEvent _response;

        public GameEvent Event { get => _event; set => _event = value; }

        private void OnEnable()
        {
            Event?.RegisterListener(this);

        }

        private void OnDisable()
        {
            Event?.UnregisterListener(this);
        }

        public void OnEventRaised()
        {
            _response.Invoke();

        }

    }
}