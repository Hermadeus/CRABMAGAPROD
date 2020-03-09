using UnityEngine;
using UnityEngine.Events;

namespace QRTools.Variables
{
    [AddComponentMenu("GameEventListener", 0)]
    public class GameEventListener : MonoBehaviour
    {
        #region Properties & Variables
        [Tooltip("Description of this listener")]
        [SerializeField] [TextArea(3, 5)] private string description;

        [Tooltip("Name of this listener")]
        public string ListenerName;

        [Tooltip("Select a game event to add link all listeners")]
        public GameEvent Event;

        [Tooltip("Response of the event")]
        public UnityEvent Response;

        [Tooltip("Uncheck : Don't play this listener when the event is raise")]
        public bool isActive = true;
        #endregion

        #region RunTime Methodss
        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }
        #endregion

        #region Public Methods
        public void OnEventRaised()
        {
            if (!isActive) return;

            Response.Invoke();
        }

        public void Desactivate()
        {
            isActive = false;
        }

        public void Activate()
        {
            isActive = true;
        }
        #endregion
    }
}
