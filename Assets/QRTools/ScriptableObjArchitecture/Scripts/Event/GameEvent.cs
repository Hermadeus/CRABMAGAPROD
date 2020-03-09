using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

using Sirenix.OdinInspector;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New event", menuName = "QRTools/Variables/Events/Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        #region Properties & Variables
        [Tooltip("Description of this GameEvent")]
        [SerializeField] [TextArea(3, 5)] private string description;

        [HideInInspector] public List<GameEventListener> listeners = new List<GameEventListener>();

        [HideInInspector] public bool inGame = false;
        #endregion

        #region Runtime Methods
        private void OnEnable()
        {
#if UNITY_EDITOR
            CheckGameMode();
#endif
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Play all listeners of this GameEvent
        /// </summary>
        [Button("Raise")]
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void Raise(string listenerName)
        {
            FindListener(listenerName).OnEventRaised();
        }

        /// <summary>
        /// Add listener to this GameEvent
        /// </summary>
        /// <param name="listener"></param>
        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        /// <summary>
        /// Remove listeners to this GameEvent
        /// </summary>
        /// <param name="listener"></param>
        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void UnregisterListener(string name)
        {
            listeners.Remove(FindListener(name));
        }

        /// <summary>
        /// Find a listener with its name in this GameEvent
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public GameEventListener FindListener(string _name)
        {
            GameEventListener g = null;

            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                if (_name == listeners[i].ListenerName)
                    g = listeners[i];
            }

            if (g == null) throw new Exception(string.Format("Can't find {0} in {1}", _name, name));

            return g;
        }
        #endregion

        #region Private Methods
#if UNITY_EDITOR
        private void CheckGameMode()
        {
            EditorApplication.playModeStateChanged += GreyButton;
        }

        private void GreyButton(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                inGame = true;

            if (state == PlayModeStateChange.ExitingPlayMode)
                inGame = false;
        }
#endif
        #endregion
    }
}
