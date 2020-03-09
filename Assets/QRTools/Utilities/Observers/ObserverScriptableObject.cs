using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace QRTools.Utilities.Observer
{
    [CreateAssetMenu(menuName = "QRTools/Utilities/Observer")]
    public class ObserverScriptableObject : SerializedScriptableObject, IObserver
    {
        public List<IObservable> observables = new List<IObservable>();
        public List<IObservable> Observables { get => observables; set => observables = value; }

        private void OnEnable()
        {
            Observables.Clear();
        }

        public void UpdateObservable()
        {
            for (int i = 0; i < Observables.Count; i++)
                observables[i]?.Notify();
        }
    }
}