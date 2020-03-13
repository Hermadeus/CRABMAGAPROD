using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Utilities
{
    public interface ISavable
    {
        ISavableBundle SavableBundle { get; set; }

        void Save();
    }

    public interface ISavableBundle
    {
        List<ISavable> Savables { get; set; }

        void Load();
    }
}
