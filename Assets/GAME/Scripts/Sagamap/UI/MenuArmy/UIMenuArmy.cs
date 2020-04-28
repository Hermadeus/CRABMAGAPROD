using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using TMPro;

namespace CrabMaga
{
    public class UIMenuArmy : UIMenu
    {
        public LanguageManager languageManager;

        public List<UITileArmy> tilesUnits = new List<UITileArmy>();
        public List<UITileArmy> tilesLeader = new List<UITileArmy>();
        public List<UITileArmy> tilesEnemies = new List<UITileArmy>();

        public TextMeshProUGUI entityname, entityLevel, description, speed, pv, dps, actif, price;
        public Image thumbnail;
        public GameObject buttonUpdate;

        public CanvasGroup unitmenu, leadermenu, enemiesmenu;

        public UIMenuArmyButtonSelection[] tabsButton;

        public UITileArmy currentTileSelected;

        public override void Init()
        {
            base.Init();

            UnitMenuOpen();
            UpdateInfoUnit(tilesUnits[0]);
            tilesUnits[0].Select();
        }

        public void UpdateMenu(UITileArmy tile)
        {
            switch (tile.tileArmy)
            {
                case UITileArmy.TypeTileArmy.UNIT:
                    UpdateInfoUnit(tile);
                    break;
                case UITileArmy.TypeTileArmy.LEADER:
                    UpdateInfoLeader(tile);
                    break;
                case UITileArmy.TypeTileArmy.ENEMY:
                    UpdateInfoEnemies(tile);
                    break;
            }
        }

        public void UpdateInfoUnit(UITileArmy tile)
        {
            buttonUpdate.SetActive(true);
            entityLevel.gameObject.SetActive(true);
            actif.gameObject.SetActive(true);
            price.gameObject.SetActive(true);

            UpdateInfo(tile);
            UpdateInfoUnitAndLeader(tile);
        }

        public void UpdateInfoLeader(UITileArmy tile)
        {
            buttonUpdate.SetActive(true);
            entityLevel.gameObject.SetActive(true);
            actif.gameObject.SetActive(true);
            price.gameObject.SetActive(true);

            UpdateInfo(tile);
            UpdateInfoUnitAndLeader(tile);
        }

        public void UpdateInfoEnemies(UITileArmy tile)
        {
            buttonUpdate.SetActive(false);
            entityLevel.gameObject.SetActive(false);
            actif.gameObject.SetActive(false);
            price.gameObject.SetActive(false);

            UpdateInfo(tile);
        }

        void UpdateInfo(UITileArmy tile)
        {
            entityname.text = tile.entityData.entityName.GetCurrentText(languageManager.LanguageEnum);
            description.text = tile.entityData.entityDescription.GetCurrentText(languageManager.LanguageEnum);
            speed.text = "Speed :" + tile.entityData.speedEnum.ToString();
            pv.text ="PV :" + tile.entityData.startHealth.ToString();
            dps.text = "DPS :" + tile.entityData.DamagePerSeconds.ToString();
            thumbnail.sprite = tile.thumbnail.sprite;
        }

        void UpdateInfoUnitAndLeader(UITileArmy tile)
        {
            entityLevel.text = tile.entityData.currentLevel.ToString();
            actif.text = tile.entityData.passifDescription.GetCurrentText(languageManager.LanguageEnum);
            price.text = tile.entityData.currentPriceUpdate.ToString();
        }

        public void DeselectAllUnit()
        {
            for (int i = 0; i < tilesUnits.Count; i++)
            {
                tilesUnits[i].IsSelected = false;
            }
        }

        public void DeselectAllLeader()
        {
            for (int i = 0; i < tilesLeader.Count; i++)
            {
                tilesLeader[i].IsSelected = false;
            }
        }

        public void DeselectAllEnemies()
        {
            for (int i = 0; i < tilesEnemies.Count; i++)
            {
                tilesEnemies[i].IsSelected = false;
            }
        }

        public void UnitMenuOpen()
        {
            for (int i = 0; i < tabsButton.Length; i++)
            {
                tabsButton[i].UnSelect();
            }

            tabsButton[0].Select();

            unitmenu.alpha = 1;
            leadermenu.alpha = 0;
            enemiesmenu.alpha = 0;

            unitmenu.blocksRaycasts = true;
            leadermenu.blocksRaycasts = false;
            enemiesmenu.blocksRaycasts = false;

            unitmenu.interactable = true;
            leadermenu.interactable = false;
            leadermenu.interactable = false;
        }

        public void LeaderMenuOpen()
        {
            for (int i = 0; i < tabsButton.Length; i++)
            {
                tabsButton[i].UnSelect();
            }
            tabsButton[1].Select();

            unitmenu.alpha = 0;
            leadermenu.alpha = 1;
            enemiesmenu.alpha = 0;

            unitmenu.blocksRaycasts = false;
            leadermenu.blocksRaycasts = true;
            enemiesmenu.blocksRaycasts = false;

            unitmenu.interactable = false;
            leadermenu.interactable = true;
            leadermenu.interactable = false;
        }

        public void EnemyMenuOpen()
        {
            for (int i = 0; i < tabsButton.Length; i++)
            {
                tabsButton[i].UnSelect();
            }
            tabsButton[2].Select();

            unitmenu.alpha = 0;
            leadermenu.alpha = 0;
            enemiesmenu.alpha = 1;

            unitmenu.blocksRaycasts = false;
            leadermenu.blocksRaycasts = false;
            enemiesmenu.blocksRaycasts = true;

            unitmenu.interactable = false;
            leadermenu.interactable = false;
            leadermenu.interactable = true;
        }
    }
}