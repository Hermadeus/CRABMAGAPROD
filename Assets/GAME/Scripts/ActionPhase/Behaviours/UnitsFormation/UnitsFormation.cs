using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/UnitsFormation")]
    public class UnitsFormation : ScriptableObject, IUnitFormationBehaviour
    {
        [SerializeField, ReadOnly] int maxEntitiesInUnit = 10;
        public int MaxEntitiesInUnit { get => maxEntitiesInUnit; set => maxEntitiesInUnit = value; }

        [SerializeField] List<Vector3> unitsPositionsOffset = new List<Vector3>();
        public List<Vector3> EntitiesPositionsOffset { get => unitsPositionsOffset; set => unitsPositionsOffset = value; }

        [SerializeField] FormationShape formationShape = FormationShape.SQUARE;
        public FormationShape FormationShape { get => formationShape; set => formationShape = value; }

        #region SQUARE
        [SerializeField, ShowIf("formationShape", FormationShape.SQUARE)]
        int
            entitiesInLenght = 5,
            entitiesInHeight = 2;
          
        [SerializeField, Range(0f,5f)]
        float
            lenghtOffset = 1f,
            heightOffset = 1f;
        #endregion

        [SerializeField, ShowIf("formationShape", FormationShape.TRIANGLE)]
        int lineOfUnits = 4;

        [Button]
        void GeneratePosition()
        {
            EntitiesPositionsOffset.Clear();

            switch (FormationShape)
            {
                #region SQUARE
                case FormationShape.SQUARE:
                    for (int z = 0; z < entitiesInHeight; z++)
                    {
                        for (int x = 0; x < entitiesInLenght; x++)
                        {
                            EntitiesPositionsOffset.Add(new Vector3(
                                x: ((lenghtOffset * x) - ((entitiesInLenght * lenghtOffset / 2f)) + (.5f * lenghtOffset)),
                                y: 0,
                                z: heightOffset * -z
                                ));
                        }
                    }

                    #endregion
                    break;
                case FormationShape.TRIANGLE:
                    #region TRIANGLE
                    int unitInLine = 0;

                    for (int z = 0; z < lineOfUnits; z++)
                    {
                        unitInLine++;
                        for (int x = 0; x < unitInLine; x++)
                        {
                            EntitiesPositionsOffset.Add(new Vector3(
                                x: ((lenghtOffset * x) - ((unitInLine * lenghtOffset / 2f)) + (.5f * lenghtOffset)),
                                y: 0,
                                z: heightOffset * -z
                                ));
                            MaxEntitiesInUnit++;
                        }
                    }
                    #endregion
                    break;

            }

            MaxEntitiesInUnit = EntitiesPositionsOffset.Count;
        }
    }

    public enum FormationShape
    {
        SQUARE,
        CIRCLE,
        TRIANGLE
    }
}