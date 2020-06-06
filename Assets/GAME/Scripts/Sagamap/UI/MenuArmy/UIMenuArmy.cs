using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using TMPro;

using Math = System.Math;

namespace CrabMaga
{
    public class UIMenuArmy : UIMenu, ISavable
    {
        public LanguageManager languageManager;

        public List<UITileArmy> tilesUnits = new List<UITileArmy>();
        public List<UITileArmy> tilesLeader = new List<UITileArmy>();
        public List<UITileArmy> tilesEnemies = new List<UITileArmy>();

        public TextMeshProUGUI entityname, entityLevel, description,  actif, price;
        public UIMenuArmyStat dps, effectif, speed, cost, health, type; 
        public Image thumbnail;
        public GameObject buttonUpdate;
        public AmeliorationButton ameliorationButton;

        public CanvasGroup unitmenu, leadermenu, enemiesmenu;

        public UIMenuArmyButtonSelection[] tabsButton;

        public UITileArmy currentTileSelected;

        public Image lockObj = default;

        public Image backgroundFondFenetreDesc;
        public Sprite backgroundForce, backgroundAgile, backgroundRes;

        public Image iconUnitType;
        public Sprite iconForce, iconAgile, iconRes;

        public override void Init()
        {
            base.Init();

            UnitMenuOpen();
            UpdateInfoUnit(tilesUnits[0]);
            tilesUnits[0].Select();

            Save();
            Load();
        }

        public void UpdateMenu(UITileArmy tile)
        {
            for (int i = 0; i < tilesUnits.Count; i++)
            {
                tilesUnits[i].UpdateTile();
            }
            for (int i = 0; i < tilesLeader.Count; i++)
            {
                tilesLeader[i].UpdateTile();
            }
            for (int i = 0; i < tilesEnemies.Count; i++)
            {
                tilesEnemies[i].UpdateTile();
            }

            switch (tile.tileArmy)
            {
                case UITileArmy.TypeTileArmy.UNIT:
                    UpdateInfoUnit(tile);
                    break;
                case UITileArmy.TypeTileArmy.LEADER:
                    UpdateInfoLeader(tile);
                    DeselectAllUnit();
                    break;
                case UITileArmy.TypeTileArmy.ENEMY:
                    UpdateInfoEnemies(tile);
                    DeselectAllUnit();
                    break;
            }

            UpdateBackGround(tile);
        }

        private void UpdateBackGround(UITileArmy tile)
        {
            switch (tile.entityData.Triforce)
            {
                case Triforce.AGILE:
                    backgroundFondFenetreDesc.sprite = backgroundAgile;
                    iconUnitType.sprite = iconAgile;
                    break;
                case Triforce.FORCE:
                    backgroundFondFenetreDesc.sprite = backgroundForce;
                    iconUnitType.sprite = iconForce;

                    break;
                case Triforce.RESISTANT:
                    backgroundFondFenetreDesc.sprite = backgroundRes;
                    iconUnitType.sprite = iconRes;

                    break;
            }
        }

        public void UpdateInfoUnit(UITileArmy tile)
        {
            buttonUpdate.SetActive(true);
            entityLevel.gameObject.SetActive(true);
            actif.gameObject.SetActive(true);
            price.gameObject.SetActive(true);
            health.gameObject.SetActive(false);
            type.gameObject.SetActive(true);

            if(tile.tileArmy == UITileArmy.TypeTileArmy.UNIT)
            {
                dps.SetOn();
                effectif.SetOn();
                speed.SetOn();
                cost.SetOn();
                health.SetOff();
                type.SetOn();
            }

            UpdateInfo(tile);
            UpdateInfoUnitAndLeader(tile);
        }

        public void UpdateInfoLeader(UITileArmy tile)
        {
            if(tile.tileArmy == UITileArmy.TypeTileArmy.LEADER)
            {
                buttonUpdate.SetActive(true);
                entityLevel.gameObject.SetActive(true);
                actif.gameObject.SetActive(true);
                price.gameObject.SetActive(true);
                effectif.gameObject.SetActive(false);
                cost.gameObject.SetActive(false);
                health.gameObject.SetActive(true);
                type.gameObject.SetActive(true);
            }


            UpdateInfo(tile);
            UpdateInfoUnitAndLeader(tile);
        }

        public void UpdateInfoEnemies(UITileArmy tile)
        {
            if(tile.tileArmy == UITileArmy.TypeTileArmy.ENEMY)
            {
                buttonUpdate.SetActive(false);
                entityLevel.gameObject.SetActive(false);
                actif.gameObject.SetActive(false);
                price.gameObject.SetActive(false);
                effectif.gameObject.SetActive(false);
                cost.gameObject.SetActive(false);
                health.gameObject.SetActive(false);
            }

            UpdateInfo(tile);
        }

        void UpdateInfo(UITileArmy tile)
        {
            entityname.text = tile.entityData.entityName.GetCurrentText(languageManager.LanguageEnum);
            description.text = tile.entityData.entityDescription.GetCurrentText(languageManager.LanguageEnum);
            speed.value.text = tile.entityData.speedEnum.ToString();
            type.value.text = tile.entityData.Triforce.ToString();
            switch (tile.entityData.Triforce)
            {
                case Triforce.AGILE:
                    type.logo.sprite = iconAgile;
                    break;
                case Triforce.FORCE:
                    type.logo.sprite = iconForce;
                    break;
                case Triforce.RESISTANT:
                    type.logo.sprite = iconRes;
                    break;
            }

            if (tile.entityData.upgradeTabs != null)
            {
                dps.value.text = Math.Round(tile.entityData.DamagePerSeconds, 2).ToString();
                health.value.text = tile.entityData.startHealth.ToString();
            }
            else
            {
                dps.value.text = Math.Round(tile.entityData.DamagePerSeconds, 2).ToString();
            }

            if (tile.tileArmy == UITileArmy.TypeTileArmy.LEADER || tile.tileArmy == UITileArmy.TypeTileArmy.UNIT)
            {
                if (tile.entityData.upgradeTabs[tile.entityData.currentLevel].damage < tile.entityData.upgradeTabs[tile.entityData.currentLevel + 1].damage
                    || tile.entityData.upgradeTabs[tile.entityData.currentLevel].attackSpeed < tile.entityData.upgradeTabs[tile.entityData.currentLevel + 1].attackSpeed)
                    dps.upgradeIcon.SetActive(true);
                else
                    dps.upgradeIcon.SetActive(false);

                if (tile.entityData.upgradeTabs[tile.entityData.currentLevel].costformation < tile.entityData.upgradeTabs[tile.entityData.currentLevel + 1].costformation)
                    cost.upgradeIcon.SetActive(true);
                else
                    cost.upgradeIcon.SetActive(false);

                if (tile.entityData.upgradeTabs[tile.entityData.currentLevel].formationX < tile.entityData.upgradeTabs[tile.entityData.currentLevel + 1].formationX
                    || tile.entityData.upgradeTabs[tile.entityData.currentLevel].formationY < tile.entityData.upgradeTabs[tile.entityData.currentLevel + 1].formationY)
                    effectif.upgradeIcon.SetActive(true);
                else
                    effectif.upgradeIcon.SetActive(false);

                if (tile.entityData.upgradeTabs[tile.entityData.currentLevel].health < tile.entityData.upgradeTabs[tile.entityData.currentLevel + 1].health)
                    health.upgradeIcon.SetActive(true);
                else
                    health.upgradeIcon.SetActive(false);
            }

            thumbnail.sprite = tile.thumbnail.sprite;
        }

        void UpdateInfoUnitAndLeader(UITileArmy tile)
        {
            if(tile.entityData is CrabUnitData)
            {
                CrabUnitData d = tile.entityData  as CrabUnitData;
                effectif.value.text = (d.formationX + d.formationY).ToString();
                cost.value.text = d.upgradeTabs[(d.currentLevel)].costformation.ToString();
            }

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

        public EntityData[] datas;

        public void Load()
        {
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].currentLevel = PlayerPrefs.GetInt(datas[i].entityName.textAnglais + "Level");
            }
        }

        public void Save()
        {
            for (int i = 0; i < datas.Length; i++)
            {
                PlayerPrefs.SetInt(datas[i].entityName.textAnglais + "Level", datas[i].currentLevel);
            }
        }
    }

}