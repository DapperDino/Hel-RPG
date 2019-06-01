using Hel.Events.CustomEvents;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hel.Events.Listeners
{
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

    public abstract class BaseGameEventListener<T1, T2, E, UER> : MonoBehaviour,
        IGameEventListener<T1, T2> where E : BaseGameEvent<T1, T2> where UER : UnityEvent<T1, T2>
    {
        [SerializeField] private E gameEvent = null;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse = null;

        private void OnEnable()
        {
            if (gameEvent == null) return;

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) return;

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T1 first, T2 second)
        {
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(first, second);
            }
        }
    }
}
