using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New int", menuName = "QRTools/Variables/Int", order = 1)]
    [ExecuteInEditMode]
    public class IntVariable : Variable<int>
    {
        #region Properties & Variables
        public override int Value
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
