using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/LeaderData")]
    public class LeaderData : EntityData
    {
        public Sprite thumbnail;

        public Sprite thumbnailToken = default;
        public Sprite thumbnailTokenUlt = default;
        public Sprite thumbnailTokenInUlt = default;
        public Sprite thumbnailTokenNone = default;

        public override void InitUpgradeTab()
        {
            int sz = 300;

            //upgradeTabs = new UpgradeTab[sz];

            string[,] s = ParseCSV();



            for (int i = 1; i < sz; i++)
            {
                string[] c1 = s[0, i].Split(';');
                float dmg = float.Parse(c1[1]);
                upgradeTabs[i].damage = dmg;

                string[] c2 = s[1, i].Split(';');
                float atkSpd = float.Parse(c2[1]);
                upgradeTabs[i].attackSpeed = atkSpd;

                string[] c3 = s[2, i].Split(';');
                int costF = int.Parse(c3[0]);
                upgradeTabs[i - 1].costformation = costF;

                upgradeTabs[i].attackSpeed = 2;
                
                int costU = int.Parse(c3[2]);
                upgradeTabs[i - 1].upgradeCost = costU;

                int h = int.Parse(c3[1]);
                upgradeTabs[i].health = h;

            }

            //Debug.Log(s.Length);

            //Debug.Log("Niveau : " + c1[0]);
            //Debug.Log("Damage : " + c1[1]);


#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}