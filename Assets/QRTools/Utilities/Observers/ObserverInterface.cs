using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Utilities.Observer
{
    public interface IObservable
    {
        void Add(IObserver observer);
        void Remove(IObserver observer);
        void Notify();
    }

    public interface IObserver
    {
        List<IObservable> Observables { get; set; }
        void UpdateObservable();
    }
}