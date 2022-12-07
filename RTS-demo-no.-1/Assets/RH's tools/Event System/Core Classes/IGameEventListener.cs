using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanHipplesArchitecture.SO_Events
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }

    public interface IGameEventListener
    {
        void OnEventRaised();
    }
}
