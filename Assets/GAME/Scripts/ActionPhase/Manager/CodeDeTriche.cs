using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

namespace CrabMaga
{
    public class CodeDeTriche : UIMenu
    {
        public HeaderMoney HeaderMoney;
        public UIMenuArmy menuArmy;
        public SceneManaging sceneManaging;

        public Button btnResouces;

        public Button firstDay, fiveDay, tenDay;

        public LevelData[] leveldatas;
        public LevelData level01;
        public LevelData levelInfini;

        public EntityData[] entityDatas;

        public EntityData craberserk;

        public SagamapManager sagamapManager;
        public TutoSagamap tutoSagamap;

        public void AddResources()
        {
            HeaderMoney.AddCrab(1000);
            HeaderMoney.AddPearl(1000);
            HeaderMoney.AddShell(1000);
        }

        public void FirstDay()
        {
            for (int i = 0; i < leveldatas.Length; i++)
            {
                leveldatas[i].asWin = false;
                leveldatas[i].isLock = true;

                leveldatas[i].star01 = false;
                leveldatas[i].star02 = false;
                leveldatas[i].star03 = false;
            }

            levelInfini.LevelIndex = 8;
            level01.isLock = false;

            for (int i = 0; i < entityDatas.Length; i++)
            {
                entityDatas[i].currentLevel = 1;
            }

            menuArmy.Save();

            craberserk.isLock = false;

            sagamapManager.SetPlayerPref();
            tutoSagamap.ResetPlayerPref();

            sceneManaging.RestartScene();
        }

        public void FiveDay()
        {
            for (int i = 0; i < leveldatas.Length; i++)
            {
                leveldatas[i].asWin = true;
                leveldatas[i].isLock = false;

                leveldatas[i].star01 = true;
                leveldatas[i].star02 = true;
                leveldatas[i].star03 = true;
            }

            levelInfini.LevelIndex = 20;
            level01.isLock = false;

            for (int i = 0; i < entityDatas.Length; i++)
            {
                entityDatas[i].currentLevel = 7;
            }

            HeaderMoney.AddCrab(2500);
            HeaderMoney.AddPearl(2500);
            HeaderMoney.AddShell(2500);

            menuArmy.Save();

            sceneManaging.RestartScene();
        }

        public void TenDay()
        {
            for (int i = 0; i < leveldatas.Length; i++)
            {
                leveldatas[i].asWin = true;
                leveldatas[i].isLock = false;

                leveldatas[i].star01 = true;
                leveldatas[i].star02 = true;
                leveldatas[i].star03 = true;
            }

            levelInfini.LevelIndex = 70;
            level01.isLock = false;

            for (int i = 0; i < entityDatas.Length; i++)
            {
                entityDatas[i].currentLevel = 22;
            }

            HeaderMoney.AddCrab(5000);
            HeaderMoney.AddPearl(5000);
            HeaderMoney.AddShell(5000);

            menuArmy.Save();

            sceneManaging.RestartScene();
        }
    }
}