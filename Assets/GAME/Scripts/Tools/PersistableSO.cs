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
    public class PersistableSO : SerializedMonoBehaviour
    {
        [Header("Meta")]
        public string persisterName;
        [Header("Scriptable Objects")]
        public List<ISavable> objectsToPersist = new List<ISavable>();

        private void OnEnable()
        {
            Load();
        }

        private void OnDisable()
        {
            Save();
        }

        public void Load()
        {
            for (int i = 0; i < objectsToPersist.Count; i++)
            {
                objectsToPersist[i].Load();
            }
            Debug.Log("LOAD");

        }

        public void Save()
        {
            for (int i = 0; i < objectsToPersist.Count; i++)
            {
                objectsToPersist[i].Save();
            }

            PlayerPrefs.Save();

            Debug.Log("SAVE");
        }

        public static PersistableSO Instance;
        private void Awake()
        {
            Instance = this;

            Save();
            Load();
        }
    }

    public interface ISavable
    {
        void Save();
        void Load();
    }
}