using UnityEngine;

using Sirenix.OdinInspector;

namespace QRTools.Utilities.Observer
{
    public abstract class Observable : SerializedMonoBehaviour, IObservable
    {
        public IObserver Observer = default;
        
        public void Add(IObserver observer)
        {
            observer.Observables.Add(this);
        }

        public void Remove(IObserver observer)
        {
            observer.Observables.Remove(this);
        }

        public abstract void Notify();
    }
}