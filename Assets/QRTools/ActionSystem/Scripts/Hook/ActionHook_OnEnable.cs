namespace QRTools.Actions
{
    public class ActionHook_OnEnable : ActionHookBase
    {
        private void OnEnable()
        {
            if (actions != null)
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }
        }
    }
}