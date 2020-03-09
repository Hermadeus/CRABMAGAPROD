using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New GameObject", menuName = "QRTools/Variables/GameObject", order = 3)]
    public class GameObjectVariable : Variable<GameObject>
    {
        #region Properties & Variables
        public override GameObject Value
        {
            get => base.Value;
            set
            {
                PlayEvent();
            }
        }
        #endregion
    }
}
