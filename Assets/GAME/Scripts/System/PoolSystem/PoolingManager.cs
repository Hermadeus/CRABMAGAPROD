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

        public EntityData data;
        public InputTouch InputTouch;

        private void Awake()
        {
        }

        public void PoolTest()
        {
            PoolEntity<CrabUnit>(data, new Vector3(InputTouch.RayPoint.x, 0, InputTouch.RayPoint.z));

        }

        public T PoolEntity<T>(EntityData _entityData, Vector3 _position) where T : Entity
        {
            T entity = Pool<T>(_position) as T;
            entity.entityData = _entityData;
            entity.OnPool();

            return entity;
        }

        public IPoolable Pool(IPoolable obj, Vector3 _position)
        {
            obj.OnPool();
            return obj;
        }

        public IPoolable Pool<T> (Vector3 _position, bool onPool = false) where T : MonoBehaviour
        {
            for (int i = 0; i < poolables.Count; i++)
            {
                if (poolables[i] is T)
                {
                    T _obj = poolables[i] as T;
                    _obj.transform.position = _position;
                    _obj.transform.parent = null;

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

        [Button]
        void GetAllIPoolable()
        {
            poolables.Clear();

            var _poolables = FindObjectsOfType<Entity>();

            for (int i = 0; i < _poolables.Length; i++)
            {
                if (_poolables[i] is IPoolable)
                    poolables.Add(_poolables[i]);
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