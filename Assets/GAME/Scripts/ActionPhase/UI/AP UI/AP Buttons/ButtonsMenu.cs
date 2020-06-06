using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class ButtonsMenu : MonoBehaviour
    {
        public Image sagamap, shop, army, options;

        public Sprite sagamap_on, sagamap_off, shop_on, shop_off, army_on, army_off, options_on, options_off;

        public SceneManaging sceneManaging;
        public Button returnToSG;

        private void Awake()
        {
            returnToSG.onClick.AddListener(sceneManaging.ReturnToSagaMap);
        }

        public void Sagamap()
        {
            DeselectAll();
            sagamap.sprite = sagamap_on;
        }

        public void Shop()
        {
            DeselectAll();
            shop.sprite = shop_on;
        }

        public void Army()
        {
            DeselectAll();
            army.sprite = army_on;
        }

        public void Options()
        {
            DeselectAll();
            options.sprite = options_on;
        }

        public void DeselectAll()
        {
            sagamap.sprite = sagamap_off;
            shop.sprite = shop_off;
            army.sprite = army_off;
            options.sprite = options_off;
        }
    }
}