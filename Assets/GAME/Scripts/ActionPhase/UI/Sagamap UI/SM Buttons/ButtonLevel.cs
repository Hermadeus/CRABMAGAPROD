using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using QRTools.UI;

namespace CrabMaga
{
    public class ButtonLevel : UIButton
    {
        public LevelData levelData = default;

        public override void OnClickButton()
        {
            base.OnClickButton();

            SceneManager.LoadScene(levelData.sceneLevel);
        }
    }
}