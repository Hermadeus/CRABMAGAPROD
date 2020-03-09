using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Utilities.Observer
{
    public class Observer : MonoBehaviour , IObserver
    {
        public List<IObservable> observables = new List<IObservable>();
        public List<IObservable> Observables { get => observables; set => observables = value; }

        public void UpdateObservable()
        {
            for (int i = 0; i < Observables.Count; i++)
                observables[i]?.Notify();
        }
    }
}