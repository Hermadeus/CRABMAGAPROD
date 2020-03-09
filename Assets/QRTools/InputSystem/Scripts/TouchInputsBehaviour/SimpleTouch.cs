using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Input/TouchType/SimpleTouch", order = 20)]
    public class SimpleTouch : ScriptableObject, ITouchInput
    {
        public void Execute(InputTouch inputTouch)
        {
            
        }
    }
}
