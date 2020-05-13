using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Entities Dictionary")]
    public class EntitiesDictionary : SerializedScriptableObject
    {
        public Dictionary<CrabUnitType, CrabUnitData> crabUnitsDic = new Dictionary<CrabUnitType, CrabUnitData>();

        public CrabUnitData GetCrabUnitData(CrabUnitType crabUnitType)
        {
            CrabUnitData crabUnitData = null;
            crabUnitsDic.TryGetValue(crabUnitType, out crabUnitData);
            return crabUnitData;
        }

        public Dictionary<LeaderType, LeaderData> leaderDic = new Dictionary<LeaderType, LeaderData>();

        public Dictionary<EnemyType, EnemyData> enemyDic = new Dictionary<EnemyType, EnemyData>();
    }

    public enum CrabUnitType
    {
        COELACRAB,
        CRABRATTER, 
        CHRAMAN,
        CRABOUFFON,
        CRABERSERK,
        CRABLINDE,
        NECRABANCIEN,
        CRABARDE,
        NINCRAB
    }

    public enum LeaderType
    {
        CRABZILLA, //Tire un rayon laser sur une ligne entière infligeant 2 fois sa force aux ennemis touchés.
    }

    public enum EnemyType
    {
        ENNEMIS_AGILE_DE_BASE,
        ENNEMIS_RESISTANTE_DE_BASE,
        ENNEMIS_FORCE_DE_BASE
    }
}