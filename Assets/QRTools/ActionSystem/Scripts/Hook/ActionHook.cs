using UnityEngine;

namespace QRTools.Actions {
    public class ActionHook : MonoBehaviour
    {
        [TextArea(3, 5)] [SerializeField] string description;

        public Action[]
            actions_Awake,
            actions_Start,
            actions_Update,
            actions_FixedUpdate;

        public bool isBreak = false;

        private void Awake()
        {
            if(actions_Awake != null)
                for (int i = 0; i < actions_Awake.Length; i++)
                {
                    actions_Awake[i].Execute();
                }
        }

        private void Start()
        {
            if (actions_Start != null)
                for (int i = 0; i < actions_Start.Length; i++)
                {
                    actions_Start[i].Execute();
                }
        }

        private void Update()
        {
            if (isBreak)
                return;

            if (actions_Update != null)
                for (int i = 0; i < actions_Update.Length; i++)
                {
                    actions_Update[i].Execute();
                }
        }

        private void FixedUpdate()
        {
            if (isBreak)
                return;

            if (actions_FixedUpdate != null)
                for (int i = 0; i < actions_FixedUpdate.Length; i++)
                {
                    actions_FixedUpdate[i].Execute();
                }
        }
    }
}
