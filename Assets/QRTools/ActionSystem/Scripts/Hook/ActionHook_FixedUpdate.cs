namespace QRTools.Actions
{
    public class ActionHook_FixedUpdate : ActionHookBase
    {
        private void FixedUpdate()
        {
            if (actions != null)
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }
        }
    }
}
