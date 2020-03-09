using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_Awake : ActionHookBase
    {
        private void Awake()
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Execute();
            }
        }
    }
}
