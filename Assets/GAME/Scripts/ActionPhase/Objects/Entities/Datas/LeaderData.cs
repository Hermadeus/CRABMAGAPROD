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

        public Sprite wheelThumbnail;

        public override void InitUpgradeTab()
        {
            int sz = 300;

            //upgradeTabs = new UpgradeTab[sz];

            string[,] s = ParseCSV();

            for (int i = 1; i < sz; i++)
            {
                string[] c1 = s[1, i].Split(';');
                float dmg = float.Parse(c1[0]);
                upgradeTabs[i].damage = dmg;

                string[] c2 = s[2, i].Split(';');
                float h = int.Parse(c2[0]);
                upgradeTabs[i].health = h;

                string[] c3 = s[3, i].Split(';');
                int costU = int.Parse(c3[0]);
                upgradeTabs[i - 1].upgradeCost = costU;

                upgradeTabs[i].attackSpeed = attackSpeed;
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