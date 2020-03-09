using UnityEditor;
using UnityEngine;

namespace QRTools.Variables
{
    [ExecuteInEditMode]
    public class Variable<T> : ScriptableObject
    {
        #region Properties & Variables
        [TextArea(3, 5)]
        [SerializeField]
        [Tooltip("Description of this variable")]
        private string description;

        [SerializeField]
        [Tooltip("Edit the initial value of this variable, Value take this value")]
        private T initialValue = default;

        [SerializeField]
        [Tooltip("Current value of this variable")]
        protected T _value;

        public virtual T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        [SerializeField]
        [Tooltip("Recorded value at the end of the game")]
        private T oldValue;

        [Tooltip("Event each time Value change")]
        public GameEvent OnValueChanged;

        [Tooltip("Can't set the value if it's check")]
        [SerializeField]
        private bool constante = false;

        [Tooltip("reset value at the start of game")]
        [SerializeField]
        private bool resetValue = true;

        [Tooltip("reset old value at the end of the game")]
        [SerializeField]
        private bool resetOldValue = true;

        [HideInInspector] public bool playOnValueChange = true;
        #endregion

        #region RunTime Methods
        private void OnEnable()
        {
            if (resetValue)
                Value = initialValue;

#if UNITY_EDITOR
            if (resetOldValue)
                EditorApplication.playModeStateChanged += SetOldValue;
#endif
        }
        #endregion

        #region Public Methods
        public virtual T SetValue(T value) => Value = value;
        public virtual T GetValue() => Value;
        #endregion

        #region Private Methods
#if UNITY_EDITOR
        protected void SetOldValue(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
                oldValue = Value;
        }
#endif
        
        protected void PlayEvent()
        {
            if (!playOnValueChange) return;

            if (!constante && OnValueChanged != null)
                OnValueChanged.Raise();
            else if (constante)
                Debug.LogWarning(string.Format("{0} : you can't set a constante, make sure this is a variable", name));
        }
        #endregion

    }
}
