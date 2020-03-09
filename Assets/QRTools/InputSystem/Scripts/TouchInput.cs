using UnityEngine;
using UnityEngine.Events;

using QRTools.Variables;

using Sirenix.OdinInspector;

using QRTools.CameraSystem;

namespace QRTools.Inputs
{
    [CreateAssetMenu(fileName = "New Touch", menuName = "QRTools/Input/Touch (old)", order = 20)]
    public class TouchInput : InputAction
    {
        #region Properties & Variables
        [SerializeField, EnumPaging] private TouchState touchType = default;

        [Tooltip("Delay between two click if DOUBLE_TAP"), ShowIf("touchType", TouchState.DOUBLE_TAP)]
        public float doubleClickTime = .2f;
        private float lastClickTime = .2f;

        [Title("Other events")]
        public UnityEvent onTouchEnter = new UnityEvent();
        public UnityEvent onTouchStay = new UnityEvent();
        public UnityEvent onTouchEnd = new UnityEvent();

        public UnityEvent onLongTouch = new UnityEvent();

        #region LongTouch
        [Tooltip("Delay between two click if SIMPLE_LONGPRESSED"), ShowIf("touchType", TouchState.SIMPLE_LONGPRESSED)]
        public float longPressedTime = .5f;
        [HideInInspector] public float longPressedTimer;
        #endregion

        #region Swipping
        public UnityEvent onSwipeRight = new UnityEvent();
        public UnityEvent onSwipeLeft = new UnityEvent();

        public UnityEvent onSwipeUp = new UnityEvent();
        public UnityEvent onSwipeDown = new UnityEvent();

        [Tooltip("Distance to run with your finger to activate the input")]
        public float swipeDistanceThresold = 50f;

        public CameraVariable cam;
        Ray ray;
        [HideInInspector] public RaycastHit hit;
        private Vector3 raypoint = new Vector3();
        public Vector3 Raypoint
        {
            get => raypoint;
            set => raypoint = value;
        }

        bool asSwipe = false;
        #endregion

        public TouchState TouchState
        {
            get { return touchType; }
            set
            {
                touchType = value;
                ChangeState(value);
            }
        }

        private Touch touch;
        public Touch Touch
        {
            get
            {
                return touch;
            }
            set
            {
                touch = value;
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        InputEnterPosition = touch.position;
                        Raycast();
                        break;
                    case TouchPhase.Moved:
                        InputCurrentPosition = touch.position;
                        Raycast();
                        break;
                    case TouchPhase.Stationary:
                        InputCurrentPosition = touch.position;
                        Raycast();
                        break;
                    case TouchPhase.Ended:
                        InputExitPosition = touch.position;
                        Raycast();
                        break;
                    case TouchPhase.Canceled:
                        break;
                }
            }
        }

        public delegate void PlayInput();
        PlayInput PlayCurrentInput;
        bool asTouch = false;
        bool asTouchEnd = false;

        private Vector2
            startPos = new Vector2(),
            endPos = new Vector2();

        public Vector2 InputEnterPosition { get; private set; }
        public Vector2 InputExitPosition { get; private set; }

        public Vector2 InputCurrentPosition { get; private set; }
        #endregion

        #region Runtime Methods
        public override void Init()
        {
            ChangeState(touchType);
        }

        public override void Execute()
        {
            if (!isActive)
                return;

            PlayCurrentInput?.Invoke();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Change state of TouchState
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(TouchState state)
        {
            RemoveDelegate();

            switch (state)
            {
                case TouchState.SIMPLE:
                    PlayCurrentInput += SimpleTouch;
                    break;
                case TouchState.SIMPLE_LONGPRESSED:
                    PlayCurrentInput += SimpleLongPressed;
                    break;

                case TouchState.DOUBLE_TAP:
                    PlayCurrentInput += DoubleTapDown;
                    break;

                case TouchState.SWIPE:
                    PlayCurrentInput += SimpleSwipe;
                    break;
                case TouchState.HORIZONTAL_SWIPE:
                    PlayCurrentInput += HorizontalSwipe;
                    break;
                case TouchState.VERTICAL_SWIPE:
                    PlayCurrentInput += VerticalSwipe;
                    break;
                case TouchState.SWIPE_PRESSED:
                    PlayCurrentInput += LongSwipe;
                    break;
            }
        }

        public void DebugPosition()
        {
            Debug.Log("EnterPosition = " + InputEnterPosition);
            Debug.Log("ExitPosition = " + InputExitPosition);
        }

        public void ForceEnter()
        {
            touch.phase = TouchPhase.Began;
            Swipe(touchType);
        }
        #endregion

        #region Delegate Methods
        void RemoveDelegate()
        {
            PlayCurrentInput = null;
        }

        void SimpleTouch() => Simple();
        void SimpleLongPressed() => LongTap();

        void HorizontalSwipe() => Swipe(TouchState.HORIZONTAL_SWIPE);
        void VerticalSwipe() => Swipe(TouchState.VERTICAL_SWIPE);
        void SimpleSwipe() => Swipe(TouchState.SWIPE);

        void DoubleTapDown() => DoubleTap(TouchPhase.Began);

        void LongSwipe() => SwipePressed();
        #endregion

        #region Private Methods
        void Swipe(TouchState state)
        {
            if (Input.touchCount == 1)
            {
                Touch = Input.touches[0];

                switch (Touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = Touch.position;
                        onTouchEnter?.Invoke();
                        break;
                    case TouchPhase.Moved:
                        onTouchStay?.Invoke();
                        endPos = Touch.position;

                        if (asSwipe) return;
                        AnalyseGesture(startPos, endPos, state);
                        break;
                    case TouchPhase.Ended:
                        asSwipe = false;
                        onTouchEnd?.Invoke();
                        break;
                }
            }
        }

        void SwipePressed()
        {
            if (Input.touchCount == 1)
            {
                Touch = Input.GetTouch(0);

                switch (Touch.phase)
                {
                    case TouchPhase.Began:
                        onTouchEnter?.Invoke();
                        break;
                    case TouchPhase.Moved:
                        endPos = Touch.position;
                        onTouchStay?.Invoke();
                        AnalyseGesture(startPos, endPos, TouchState.SWIPE_PRESSED);
                        break;
                    case TouchPhase.Stationary:
                        startPos = Touch.position;
                        break;
                    case TouchPhase.Ended:
                        asSwipe = false;
                        onTouchEnd?.Invoke();
                        break;
                }
            }
        }

        void AnalyseGesture(Vector2 start, Vector2 end, TouchState state)
        {
            if(Vector2.Distance(start, end) > swipeDistanceThresold)
            {
                asSwipe = true;

                switch (state)
                {
                    case TouchState.VERTICAL_SWIPE:
                        if (start.y < end.y && end.y - start.y > swipeDistanceThresold)
                            onSwipeUp?.Invoke();
                        else if (start.y > end.y && start.y - end.y > swipeDistanceThresold)
                            onSwipeDown?.Invoke();
                        break;

                    case TouchState.HORIZONTAL_SWIPE:
                        if (start.x < end.x && end.x - start.x > swipeDistanceThresold)
                            onSwipeRight?.Invoke();
                        else if (start.x > end.x && start.x - end.x > swipeDistanceThresold)
                            onSwipeLeft?.Invoke();
                        break;

                    case TouchState.SWIPE:
                        if (start.y < end.y && end.y - start.y > swipeDistanceThresold)
                            onSwipeUp?.Invoke();
                        else if (start.y > end.y && start.y - end.y > swipeDistanceThresold)
                            onSwipeDown?.Invoke();
                        if (start.x < end.x && end.x - start.x > swipeDistanceThresold)
                            onSwipeRight?.Invoke();
                        else if (start.x > end.x && start.x - end.x > swipeDistanceThresold)
                            onSwipeLeft?.Invoke();
                        break;

                    case TouchState.SWIPE_PRESSED:
                        if (end.y - start.y > swipeDistanceThresold)
                            onSwipeUp?.Invoke();
                        else if (start.y - end.y > swipeDistanceThresold)
                            onSwipeDown?.Invoke();
                        if (end.x - start.x > swipeDistanceThresold)
                            onSwipeRight?.Invoke();
                        else if (start.x - end.x > swipeDistanceThresold)
                            onSwipeLeft?.Invoke();
                        break;
                }
            }
        }        

        void LongTap()
        {
            if (Input.touchCount == 1)
            {
                Touch = Input.GetTouch(0);

                switch (Touch.phase)
                {
                    case TouchPhase.Began:
                        longPressedTimer = longPressedTime;
                        onTouchEnter?.Invoke();
                        asTouch = true;
                        asTouchEnd = false;
                        break;
                    case TouchPhase.Stationary:
                        longPressedTimer -= Time.deltaTime;

                        if (longPressedTimer < 0)
                            onTouchStay?.Invoke();

                        if (longPressedTimer <= 0 && asTouch)
                        {
                            onLongTouch?.Invoke();
                            asTouch = false;
                            asTouchEnd = true;
                        }
                        break;
                    case TouchPhase.Ended:
                        if (asTouchEnd)
                        {
                            onTouchEnd?.Invoke();
                            asTouchEnd = false;
                        }
                        break;
                }
            }
        }

        void DoubleTap(TouchPhase phase)
        {
            if (Input.touchCount == 1)
            {
                float timeSinceLastClick = Time.time - lastClickTime;
                Touch = Input.GetTouch(0);

                if (Touch.phase == phase && timeSinceLastClick <= doubleClickTime)
                {
                    lastClickTime = Time.time;
                    onTouchEnter?.Invoke();
                    InputEnterPosition = Touch.position;
                    InputExitPosition = Touch.position;
                }

                lastClickTime = Time.time;
            }
        }

        void Simple()
        {
            if (Input.touchCount == 1)
            {
                Touch = Input.GetTouch(0);

                switch (Touch.phase)
                {
                    case TouchPhase.Began:
                        onTouchEnter?.Invoke();
                        break;
                    case TouchPhase.Moved:
                        onTouchStay?.Invoke();
                        break;
                    case TouchPhase.Stationary:
                        onTouchStay?.Invoke();
                        break;
                    case TouchPhase.Ended:
                        onTouchEnd.Invoke();
                        break;
                    case TouchPhase.Canceled:
                        break;
                }
            }
        }

        void Raycast()
        {
            if (cam == null)
                return;

            ray = cam.value.ScreenPointToRay(touch.position);

            if(Physics.Raycast(ray, out hit))
            {
                Raypoint = hit.point;
            }
        }
        #endregion
    }

    #region Enums
    public enum TouchState
    {
        SIMPLE,
        SIMPLE_LONGPRESSED,

        DOUBLE_TAP,

        SWIPE,
        HORIZONTAL_SWIPE,
        VERTICAL_SWIPE,
        SWIPE_PRESSED
    }
    #endregion
}
