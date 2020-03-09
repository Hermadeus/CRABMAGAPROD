using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New float", menuName = "QRTools/Variables/Float", order = 0)]
    public class FloatVariable : Variable<float>
    {
        #region Properties & Variables
        public override float Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                PlayEvent();
            }
        }
        #endregion
    }
}