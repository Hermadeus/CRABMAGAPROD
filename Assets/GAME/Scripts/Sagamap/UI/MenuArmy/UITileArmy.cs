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
        public Outline outline;

        public GameObject levelGO;
        public TextMeshProUGUI level;

        bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                if (value)
                {
                    outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 1);
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
                    outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 0);
                }
            }
        }

        public enum TypeTileArmy { UNIT, LEADER, ENEMY}
        public TypeTileArmy tileArmy = TypeTileArmy.UNIT;


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
                    levelGO.SetActive(true);
                    level.text = "LVL " + entityData.currentLevel.ToString();
                    break;
                case TypeTileArmy.LEADER:
                    levelGO.SetActive(true);
                    level.text = "LVL " + entityData.currentLevel.ToString();
                    break;
                case TypeTileArmy.ENEMY:
                    levelGO.SetActive(false);
                    break;
            }

            thumbnail.sprite = entityData.entityicon;
        }

        public void Select()
        {
            menuArmy.currentTileSelected = this;

            switch (tileArmy)
            {
                case TypeTileArmy.UNIT:
                    menuArmy.DeselectAllUnit();
                    break;
                case TypeTileArmy.LEADER:
                    menuArmy.DeselectAllLeader();
                    break;
                case TypeTileArmy.ENEMY:
                    menuArmy.DeselectAllUnit();
                    break;
            }

            IsSelected = true;
            menuArmy.UpdateMenu(this);
        }        
    }
}