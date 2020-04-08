using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Inputs;

namespace CrabMaga
{
    public class PoolingManager : SerializedMonoBehaviour
    {
        [BoxGroup("Pool Informations")]
        public List<IPoolable> poolables = new List<IPoolable>();

        [BoxGroup("Pool Informations")]
        public Vector3 pushPos = new Vector3(100, 0, 100);

        [BoxGroup("References")]
        public Transform QueueObject;
        [BoxGroup("References")]
        public AP_GameManager APgameManager = default;
        [BoxGroup("References")]
        public PlayerData playerData = default;
        [BoxGroup("References")]
        public InputTouch doubleTouch = default;
        [BoxGroup("References")]
        public Transform poolingParent = default;
        [BoxGroup("References")]
        public CrabCount crabCount = default;

        //public void CreateCrabFormation(CrabUnitData data, Vector3 _position)
        //{
        //    if (APgameManager.crabFormationOnBattle.Count >= APgameManager.levelData.maxCrab)
        //    {
        //        return;
        //    }

        //    CrabFormation _crabFormation = Pool<CrabFormation>(Vector3.zero) as CrabFormation;
        //    APgameManager.crabFormationOnBattle.Add(_crabFormation);

        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int y = 0; y < 3; y++)
        //        {
        //            CrabUnit crabUnit = PoolEntity<CrabUnit>(data, new Vector3(_position.x * (i / 2f), 0, _position.z * (y / 2f)));

        //            _crabFormation.CrabUnits.Add(crabUnit);
        //            crabUnit.crabFormationReference = _crabFormation;
        //        }
        //    }
        //}

        public void CreateCrabFormationWithType(Type crabType, Vector3 _position, int nbrCrabX, int nbrCrabY, float density)
        {
            if (APgameManager.crabFormationOnBattle.Count >= APgameManager.levelData.maxCrab)
            {
                return;
            }

            CrabFormation _crabFormation = Pool<CrabFormation>(Vector3.zero) as CrabFormation;
            APgameManager.crabFormationOnBattle.Add(_crabFormation);

            playerData.money -= nbrCrabX * nbrCrabY;
            crabCount.UpdateText();

            for (int i = 0; i < nbrCrabX; i++)
            {
                for (int y = 0; y < nbrCrabY; y++)
                {
                    CrabUnit crabUnit = PoolEntity(
                        crabType,
                        new Vector3(_position.x + (i * density), 0, _position.z + (y * density)),
                        poolingParent
                        ) as CrabUnit;

                    _crabFormation.CrabUnits.Add(crabUnit);
                    crabUnit.crabFormationReference = _crabFormation;
                }
            }

            _crabFormation.name = _crabFormation.CrabUnits[0].name.ToString() + " Formation";
        }

        public void InvokeLeader()
        {
            if (APgameManager.leaderOnBattle == null && playerData.leader_slot != null)
            {
                APgameManager.leaderOnBattle = PoolEntity(
                    playerData.leader_slot.unitType.GetType(),
                    new Vector3(doubleTouch.RayPoint.x, 0, doubleTouch.RayPoint.z),
                    poolingParent
                    ) as Leader;
            }
        }

        public void InvokeLeader(InputTouch touch)
        {
            if (APgameManager.leaderOnBattle == null && playerData.leader_slot != null)
            {
                APgameManager.leaderOnBattle = PoolEntity(
                    playerData.leader_slot.unitType.GetType(),
                    new Vector3(touch.RayPoint.x, 0, touch.RayPoint.z),
                    poolingParent
                    ) as Leader;
            }
        }

        public T PoolEntity<T>(EntityData _entityData, Vector3 _position) where T : Entity
        {
            T entity = Pool<T>(_position) as T;
            entity.entityData = _entityData;
            entity.OnPool();

            return entity;
        }

        public Entity PoolEntity(Type crabType, Vector3 _position, Transform parent = null)
        {
            Entity entity = Pool(_position, crabType, parent) as Entity;
            entity.OnPool();

            if(entity is Unit)
            {
                Unit u = entity as Unit;
                if(u.entityData.havePastille && u != null)
                    u.SetPastille();
            }

            if (entity is CrabUnit)
                APgameManager.crabUnitOnBattle.Add(entity as CrabUnit);

            return entity;
        }

        public IPoolable Pool<T> (Vector3 _position, Transform parent = null, bool onPool = false) where T : MonoBehaviour
        {
            if(poolables.Count > 0)
                for (int i = 0; i < poolables.Count; i++)
                {
                    if (poolables[i] is T)
                    {
                        T _obj = poolables[i] as T;
                        _obj.enabled = true;

                        _obj.transform.position = _position;

                        if (parent == null)
                            _obj.transform.parent = null;
                        else
                            _obj.transform.parent = parent;

                        if (onPool)
                            ((IPoolable)_obj).OnPool();

                        IPoolable _poolable = poolables[i];
                        poolables.RemoveAt(i);

                        return _poolable;
                    }
                }

            throw new System.Exception(string.Format(
                "Impossible de trouver le type de Poolable que tu recherches dans la liste de poolable."
                ));
        }

        public IPoolable Pool(Vector3 _position, Type typeToResearch, Transform parent = null, bool onPool = false)
        {
            if (poolables.Count > 0)
                for (int i = 0; i < poolables.Count; i++)
                {
                    if (poolables[i].GetType() == typeToResearch)
                    {
                        //Debug.Log("found");
                        
                        Entity _obj = (Entity)poolables[i];
                        _obj.enabled = true;

                        _obj.transform.position = _position;

                        if (parent == null)
                            _obj.transform.parent = null;
                        else
                            _obj.transform.parent = parent;

                        if (onPool)
                            ((IPoolable)_obj).OnPool();

                        IPoolable _poolable = poolables[i];
                        poolables.RemoveAt(i);

                        return _obj;
                    }
                }

            throw new System.Exception(string.Format(
                "Impossible de trouver le type de Poolable que tu recherches dans la liste de poolable."
                ));
        }

        public void Push(IPushable pushable)
        {
            if(pushable is IPoolable)
                poolables.Add(pushable as IPoolable);

            MonoBehaviour mb = pushable as MonoBehaviour;
            mb.transform.position = pushPos;
            pushable.OnPush();
            mb.enabled = false;
        }

        [Button]
        public void GetAllIPoolable()
        {
            poolables.Clear();

            var _poolables = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < _poolables.Length; i++)
            {
                if (_poolables[i] is IPoolable)
                    poolables.Add(_poolables[i] as IPoolable);
            }

            Debug.Log(poolables.Count + " poolables found");
        }
    }

    public interface IPoolable
    {
        void OnPool();
    }

    public interface IPushable
    {
        void OnPush();
    }
}