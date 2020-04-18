using UnityEngine;
using UnityEngine.Events;

using QRTools.CameraSystem;

using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Input/Touch", order = 20)]
    public class InputTouch : InputAction
    {
        #region Fields
        [BoxGroup("References")]
        public ITouchInput touchType = default;

        [SerializeField, BoxGroup("References")] private CameraVariable camera = default;
        [BoxGroup("Ray")] public bool useRaycast = false;
        [ShowIf("@this.useRaycast == true"), BoxGroup("Ray")] public LayerMask mask;
        [ShowIf("@this.useRaycast == true"), BoxGroup("Ray")] public bool useInteraction = false;
        [ReadOnly, BoxGroup("Ray")] public Collider objectHit = null;
        private Ray ray;
        private RaycastHit hit;
        private bool asRayEnter = false;
        Collider previousObjectHit = null;

        private Touch touch;

        private Vector2 delta = new Vector2();
        #endregion

        #region Properties
        /// <summary>
        /// Reference à la touche de l'input.
        /// </summary>
        public Touch Touch
        {
            get => touch;
            set
            {
                touch = value;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        InputEnterPosition = touch.position;
                        Raycast();

                        onTouchEnter?.Invoke();
                        break;
                    case TouchPhase.Moved:
                        InputCurrentPosition = touch.position;
                        Raycast();

                        onTouchMoving?.Invoke();
                        break;
                    case TouchPhase.Stationary:
                        InputCurrentPosition = touch.position;
                        Raycast();

                        onTouchStay?.Invoke();
                        break;
                    case TouchPhase.Ended:
                        InputExitPosition = touch.position;
                        Raycast();

                        onTouchEnd?.Invoke();
                        break;
                    case TouchPhase.Canceled:
                        break;
                }
            }
        }
        /// <summary>
        /// Position quand le doigt entre en contact avec l'écran.
        /// </summary>
        public Vector2 InputEnterPosition { get; private set; }
        /// <summary>
        /// Position calculée en continue tant que le doigt est sur l'écran.
        /// </summary>
        public Vector2 InputCurrentPosition { get; private set; }
        /// <summary>
        /// Position quand le doigt quitte l'écran.
        /// </summary>
        public Vector2 InputExitPosition { get; private set; }
        /// <summary>
        /// Position dans le world_space d'un rayon émit dans l'axe de la camera et qui à pour position d'emission la position du doigt.
        /// </summary>
        public Vector3 RayPoint { get; private set; }
        /// <summary>
        /// DeltaPosition du doigt.
        /// </summary>
        public Vector2 DeltaPosition { get => Touch.deltaPosition; }
        /// <summary>
        /// RawPosition du doigt.
        /// </summary>
        public Vector2 RawPosition { get => Touch.rawPosition; }
        /// <summary>
        /// Références aux touches enregistrées par l'écran.
        /// </summary>
        public Touch[] Touches { get => Input.touches; }
        /// <summary>
        /// Direction vers laquelle se déplace le doigt.
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                return (InputEnterPosition - InputCurrentPosition).normalized;
            }
        }
        /// <summary>
        /// Vitesse du doigt lorsqu'il est en mouvement.
        /// </summary>
        public float TouchSpeed
        {
            get
            {
                delta = InputCurrentPosition - InputEnterPosition;
                delta.x = delta.x / Screen.width;
                delta.y = delta.y / Screen.height;
                return delta.magnitude / Time.deltaTime;
            }
        }
        #endregion

        #region Events
        [System.Serializable] public class OnPinch : UnityEvent<float> { }
        [System.Serializable] public class OnLongTap : UnityEvent<float> { }
        [System.Serializable] public class OnSwipe : UnityEvent<float> { }
        [System.Serializable] public class OnRayEvent : UnityEvent<Collider> { }

        [FoldoutGroup("Events"), Title("Current Events")]
        public UnityEvent onTouchEnter = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onTouchStay = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onTouchMoving = new UnityEvent();
        [FoldoutGroup("Events")]
        public UnityEvent onTouchEnd = new UnityEvent();
        [ShowIf("@this.touchType is DoubleTap"), FoldoutGroup("Events"), Title("DoubleTap Events")]
        public UnityEvent onDoubleTap = new UnityEvent();
        [ShowIf("@this.touchType is Swipe"), FoldoutGroup("Events"), Title("Swipe Events")]
        public OnSwipe onSwipeUp = new OnSwipe();
        [ShowIf("@this.touchType is Swipe"), FoldoutGroup("Events")]
        public OnSwipe onSwipeDown = new OnSwipe();
        [ShowIf("@this.touchType is Swipe"), FoldoutGroup("Events")]
        public OnSwipe onSwipeRight = new OnSwipe();
        [ShowIf("@this.touchType is Swipe"), FoldoutGroup("Events")]
        public OnSwipe onSwipeLeft = new OnSwipe();
        [ShowIf("@this.touchType is Pinch"), FoldoutGroup("Events"), Title("Pinch Events")]
        public OnPinch onPinch = new OnPinch();
        [ShowIf("@this.touchType is LongTap"), FoldoutGroup("Events"), Title("LongTap Events")]
        public UnityEvent onLongTapEnd = new UnityEvent();
        [ShowIf("@this.touchType is LongTap"), FoldoutGroup("Events")]
        public OnLongTap onLongTap = new OnLongTap();

        [ShowIf("@this.useRaycast == true"), FoldoutGroup("Events"), Title("Ray Events")]
        public OnRayEvent onRayEnter = new OnRayEvent();
        [ShowIf("@this.useRaycast == true"), FoldoutGroup("Events")]
        public OnRayEvent onRayStay = new OnRayEvent();
        [ShowIf("@this.useRaycast == true"), FoldoutGroup("Events")]
        public OnRayEvent onRayExit = new OnRayEvent();
        #endregion

        #region DoubleTap
        [ShowIf("@this.touchType is DoubleTap"), Tooltip("Nombre de tap avant de déclencher l'event onDoubleTap."), BoxGroup("DoubleTap Settings")]
        public int tapCount = 2;
        [HideInInspector] public bool asTap = false;
        #endregion

        #region Swipe
        [HideInInspector] public Vector2 fingerDown = new Vector2();
        [HideInInspector] public Vector2 fingerUp = new Vector2();
        [HideInInspector] public bool asSwipe = false;
        [ShowIf("@this.touchType is Swipe"), Tooltip("Active les events de swipe uniquement après avoir retirer le doigt."), BoxGroup("Swipe Settings")]
        public bool detectSwipeOnlyAfterRelease = false;
        [ShowIf("@this.touchType is Swipe"), Tooltip("Si false: Active l'event une unique fois, sinon active l'event tant que le doigt swipe."), BoxGroup("Swipe Settings")]
        public bool swipeInContinue = false;
        #endregion

        #region Pinch
        [ShowIf("@this.touchType is Pinch"), ReadOnly , Tooltip("Retourne la valeur de pinch"), BoxGroup("Pinch Settings")]
        public float pinchValue = 0f;
        [HideInInspector] public Vector2
            touchZeroPrevPos = new Vector2(),
            touchOnePrevPos = new Vector2();
        [HideInInspector] public float
            prevMagnitude,
            currentMagnitude;
        #endregion

        #region LongTap
        [ShowIf("@this.touchType is LongTap"), Tooltip("Temps avant que onLongTapEnd soit activé."), BoxGroup("LongTap Settings")]
        public float longTapTimer = 1f;
        public float longTapChargementVar { get; set; }

        [ShowIf("@this.touchType is LongTap"), ReadOnly, Range(0, 1), BoxGroup("LongTap Settings"), Tooltip("Temps normalisé du chargement du longTap.")]
        public float longTapChargement;
        #endregion

        #region Runtime Methods
        public override void Init()
        {
            asSwipe = false;
            pinchValue = 0;
            delta = Vector2.zero;

            asRayEnter = false;
            objectHit = null;
            previousObjectHit = null;
        }

        public override void Execute()
        {
            if (!isActive) return;

            if (Input.touchCount > 0)
            {
                Touch = Input.GetTouch(0);
                touchType?.Execute(this);
            }
        }
        #endregion

        #region Private Methods
        void Raycast()
        {
            if (camera == null || !useRaycast)
                return;

            ray = camera.value.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                RayPoint = hit.point;

                objectHit = hit.collider;

                if (objectHit != previousObjectHit)
                {
                    asRayEnter = false;
                    onRayExit?.Invoke(objectHit);

                    if (useInteraction)
                    {
                        var interactable = objectHit.GetComponents<Iinteractable>();
                        for (int i = 0; i < interactable.Length; i++)
                            interactable[i].Select();
                    }
                }

                if (!asRayEnter)
                {
                    onRayEnter?.Invoke(objectHit);
                    previousObjectHit = objectHit;
                    asRayEnter = true;
                }

                if(Touch.phase == TouchPhase.Ended)
                    objectHit = null;

                onRayStay?.Invoke(objectHit);
            }
        }
        #endregion

        #region DebugMethods
        public void DebugRayPoint() => Debug.Log(string.Format("RayPoint = {0}", RayPoint));
        public void DebugRayHit() => Debug.Log(string.Format("RayHit= {0}", objectHit.gameObject.name));
        public void DebugCurrentPos() => Debug.Log(string.Format("CurrentPos = {0}", InputCurrentPosition));
        public void DebugDeltaPosition() => Debug.Log(string.Format("DeltaPosition = {0}", DeltaPosition));
        public void DebugRawPosition() => Debug.Log(string.Format("RawPosition = {0}", RawPosition));
        public void DebugPinchValue() => Debug.Log(string.Format("PinchValue = {0}", pinchValue));
        public void DebugDirection() => Debug.Log(string.Format("Direction = {0}", Direction));
        public void DebugSpeed() => Debug.Log(string.Format("Speed = {0}", TouchSpeed));
        #endregion
    }

    public interface Iinteractable
    {
        void Select();

        void Deselect();
    }
}
