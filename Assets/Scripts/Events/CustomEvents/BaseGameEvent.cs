using UnityEngine;
using System.Collections.Generic;
using Hel.Events.Listeners;

namespace Hel.Events.CustomEvents
{
    /// <summary>
    /// Used as a middle-man between custom event listeners and invokers.
    /// </summary>
    /// <typeparam name="T">The type of data that is passed with the event call.</typeparam>
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for(int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(item);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if(!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if(eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}
