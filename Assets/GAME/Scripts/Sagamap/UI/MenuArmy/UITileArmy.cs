using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using TMPro;

namespace CrabMaga
{
    public class UITileArmy : UIElement
    {
        public EntityData entityData = default;
        public UIMenuArmy menuArmy;

        public Image thumbnail;

        public GameObject levelGO;
        public TextMeshProUGUI level;

        [SerializeField] bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                if (value)
                {
                    outline.SetActive(true);
                    switch (tileArmy)
                    {
                        case TypeTileArmy.UNIT:
                            menuArmy.UpdateInfoUnit(this);
                            break;
                        case TypeTileArmy.LEADER:
                            menuArmy.UpdateInfoLeader(this);
                            break;
                        case TypeTileArmy.ENEMY:
                            menuArmy.UpdateInfoEnemies(this);
                            break;
                    }
                }
                else
                {
                    outline.SetActive(false);
                }
            }
        }

        public CodeColor codeColor;
        public Image lvlBack;

        public GameObject cadenas;

        public enum TypeTileArmy { UNIT, LEADER, ENEMY}
        public TypeTileArmy tileArmy = TypeTileArmy.UNIT;
        public GameObject outline;

        public override void Init()
        {
            base.Init();

            UpdateTile();

            switch (tileArmy)
            {
                case TypeTileArmy.UNIT:
                    menuArmy.tilesUnits.Add(this);
                    break;
                case TypeTileArmy.LEADER:
                    menuArmy.tilesLeader.Add(this);
                    break;
                case TypeTileArmy.ENEMY:
                    menuArmy.tilesEnemies.Add(this);
                    break;
            }
        }

        public void UpdateTile()
        {
            switch (tileArmy)
            {
                case TypeTileArmy.UNIT:
                    //levelGO.SetActive(true);
                    level.text = "LVL " + entityData.currentLevel.ToString();
                    break;
                case TypeTileArmy.LEADER:
                    //levelGO.SetActive(true);
                    level.text = "LVL " + entityData.currentLevel.ToString();
                    break;
                case TypeTileArmy.ENEMY:
                    //levelGO.SetActive(false);
                    break;
            }

            thumbnail.sprite = entityData.entityicon;

            if (entityData.isLock)
            {
                if(cadenas != null)
                    cadenas.SetActive(true);
            }

            lvlBack.color = codeColor.GetColor(entityData.Triforce);
        }

        public void Unlock()
        {
            if (cadenas != null)
                cadenas.SetActive(false);
        }

        public void Select()
        {
            menuArmy.currentTileSelected = this;

            menuArmy.DeselectAllUnit();
            menuArmy.DeselectAllLeader();
            menuArmy.DeselectAllEnemies();

            switch (tileArmy)
            {
                case TypeTileArmy.UNIT:
                    break;
                case TypeTileArmy.LEADER:
                    break;
                case TypeTileArmy.ENEMY:
                    break;
            }

            IsSelected = true;
            menuArmy.UpdateMenu(this);

            if (entityData.isLock)
            {
                menuArmy.ameliorationButton.Islock = true;
                menuArmy.price.text = "50";
            }
            else
            {
                menuArmy.ameliorationButton.Islock = false;
            }
        }        
    }
}