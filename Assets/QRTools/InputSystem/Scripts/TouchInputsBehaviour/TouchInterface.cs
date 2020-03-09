using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    public interface ITouchInput
    {
        void Execute(InputTouch inputTouch);
    }
}