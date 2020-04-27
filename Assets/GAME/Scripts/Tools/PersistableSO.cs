using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using QRTools;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using QRTools.Utilities.Singleton;

namespace CrabMaga
{
    public class PersistableSO : MonoBehaviour
    {
        [Header("Meta")]
        public string persisterName;
        [Header("Scriptable Objects")]
        public List<ScriptableObject> objectsToPersist;

        protected void OnEnable()
        {
            SaveOnEnable();
        }

        public void SaveOnEnable()
        {
            for (int i = 0; i < objectsToPersist.Count; i++)
            {
                if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i)))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i), FileMode.Open);
                    JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToPersist[i]);
                    file.Close();
                    Debug.Log("save enable");
                }
                else
                {
                    //Do Nothing
                }
            }
        }

        public void SaveOnDisable()
        {
            for (int i = 0; i < objectsToPersist.Count; i++)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i));
                var json = JsonUtility.ToJson(objectsToPersist[i]);
                bf.Serialize(file, json);
                file.Close();
                Debug.Log("save disable");
            }
        }

        public void Save()
        {
            //SaveOnEnable();
            SaveOnDisable();
        }

        protected void OnDisable()
        {
            SaveOnDisable();
        }

        public static PersistableSO Instance;
        private void Awake()
        {
            Instance = this;
        }
    }
}