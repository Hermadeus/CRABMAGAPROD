using UnityEngine;
using QRTools.Actions;

namespace QRTools.Debugging
{
    [CreateAssetMenu(fileName = "New Debug Action", menuName ="QRTools/Actions/Debug Action")]
    public class DebugAction : Action
    {
        [SerializeField, TextArea(3,5)] string message = default;

        public override void Execute()
        {
            Debug.Log(message);
        }

        public void OnDebug(string _message)
        {
            Debug.Log(_message);
        }
    }
}
