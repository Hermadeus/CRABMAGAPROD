namespace QRTools.Actions
{
    public class ActionHook_Update : ActionHookBase
    {
        private void Update()
        {
            if (actions != null)
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }
        }
    }
}
