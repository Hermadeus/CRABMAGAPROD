namespace QRTools.Actions
{
    public class ActionHook_Start : ActionHookBase
    {
        private void Start()
        {
            if(actions != null)
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }
        }
    }
}
