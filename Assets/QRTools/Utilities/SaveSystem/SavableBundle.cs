using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Utilities
{
    public abstract class SavableBundle : ScriptableObjectWithDescription, ISavableBundle
    {
        [SerializeField] List<ISavable> savables = new List<ISavable>();
        public List<ISavable> Savables { get => savables; set => savables = value; }

        public abstract void Load();
    }
}