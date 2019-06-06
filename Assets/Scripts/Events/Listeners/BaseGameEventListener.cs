using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hel.Events.Listeners
{
    /// <summary>
    /// Used to listen for calls from a specific custom game event.
    /// </summary>
    /// <typeparam name="T">The type of data that is passed to the listener.</typeparam>
    /// <typeparam name="E">The custom game event to listen for.</typeparam>
    /// <typeparam name="UER">The custom Unity Event where function calls are invoked.</typeparam>
    public class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [Required] [SerializeField] private E gameEvent = null;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse = null;

        private void OnEnable()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}
