using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RyanHipplesArchitecture.SO_Events
{
    [AddComponentMenu("GameEventListeners/GameEventListener (bool)")]
    public class GameEventListener_bool : GameEventListener<bool, GameEvent_bool, UnityEvent_bool> { }
}