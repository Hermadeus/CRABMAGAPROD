using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        public void CreateCrabFormation(EntityData data, Vector3 _position)
        {
            if (APgameManager.crabFormationOnBattle.Count >= 3)
            {
                return;
            }

            CrabFormation _crabFormation = Pool<CrabFormation>(Vector3.zero) as CrabFormation;
            APgameManager.crabFormationOnBattle.Add(_crabFormation);

            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    CrabUnit crabUnit = PoolEntity<CrabUnit>(data, new Vector3(_position.x * (i / 2f), 0, _position.z * (y / 2f)));

                    _crabFormation.CrabUnits.Add(crabUnit);
                    crabUnit.crabFormationReference = _crabFormation;
                }
            }
        }

        public void InvokeLeader()
        {
            if(APgameManager.leaderOnBattle == null && playerData.leader_slot != null)
                APgameManager.leaderOnBattle = PoolEntity<Leader>(playerData.leader_slot, new Vector3(doubleTouch.RayPoint.x, 0, doubleTouch.RayPoint.z));
        }

        public T PoolEntity<T>(EntityData _entityData, Vector3 _position) where T : Entity
        {
            T entity = Pool<T>(_position) as T;
            entity.entityData = _entityData;
            entity.OnPool();

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
        void GetAllIPoolable()
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