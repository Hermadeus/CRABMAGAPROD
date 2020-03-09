using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Variables
{    
    public class ListVariable<T> : ScriptableObject
    {
        #region Properties & Variables
        [Tooltip("Developper description")]
        [SerializeField] [TextArea(3, 5)] private string description;

        [Tooltip("Current list")]
        [SerializeField] private List<T> _list = new List<T>();
        public List<T> ListValue
        {
            get
            {
                return _list;
            }
            set
            {
                _list = value;
            }
        }

        [Tooltip("Event call when you add value to the list")]
        public GameEvent onAdded;

        [Tooltip("Event call when you remove value to the list")]
        public GameEvent onRemoved;

        [Tooltip("Event call when you clear the list")]
        public GameEvent onCleared;

        [Tooltip("check if you want to reset the list at the start of the game")]
        [SerializeField] private bool resetValue = false;

        [Tooltip("check if you want to save list into a savedList at the start of the game")]
        [SerializeField] private bool saveList = true;
        
        private List<T> savedList = new List<T>();

        public float Count { get => ListValue.Count; }
        #endregion

        #region RunTime Methods
        private void OnEnable()
        {
            if (saveList) SaveList();

            if (resetValue)
                ListValue.Clear();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add a value to the <see cref="ListValue"/>
        /// </summary>
        /// <param name="element"></param>
        public virtual void Add(T element)
        {
            ListValue.Add(element);
            onAdded.Raise();
        }

        public virtual void Add(params T[] element)
        {
            foreach (T e in element)
                ListValue.Add(e);

            onAdded.Raise();
        }

        /// <summary>
        /// Remove all values of <see cref="ListValue"/>
        /// </summary>
        public virtual void Clear()
        {
            ListValue.Clear();

            onCleared.Raise();
        }

        /// <summary>
        /// Remove a value of <see cref="ListValue"/>
        /// </summary>
        /// <param name="element"></param>
        public virtual void Remove(T element)
        {
            ListValue.Remove(element);

            onRemoved.Raise();
        }

        public virtual void Remove(params T[] elements)
        {
            foreach (T element in elements)
                ListValue.Remove(element);

            onRemoved.Raise();
        }

        /// <summary>
        /// Remove a value at the <paramref name="index"/> of the <see cref="ListValue"/>
        /// </summary>
        /// <param name="index"></param>
        public virtual void RemoveAt(int index)
        {
            ListValue.RemoveAt(index);

            onRemoved.Raise();
        }

        /// <summary>
        /// Remove all values between the index <paramref name="from"/> to <paramref name="to"/> of the <see cref="ListValue"/>
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public virtual void RemoveRange(int from, int to)
        {
            ListValue.RemoveRange(from, to);

            onRemoved.Raise();
        }

        public T GetElement(int index) => ListValue[index];
        public T SetElement(T element, int index) => ListValue[index] = element;

        /// <summary>
        /// Reset <see cref="ListValue"/> in <see cref="savedList"/>
        /// </summary>
        public void ResetList()
        {
            if (!saveList)
                throw new System.Exception(string.Format("You haven't save this list, pls check saveList"));
            else
            {
                _list.Clear();
                _list = savedList;
            }
        }

        /// <summary>
        /// Save <see cref="ListValue"/> in <see cref="savedList"/>
        /// </summary>
        public void SaveList()
        {
            for (int i = 0; i < _list.Count; i++)
                savedList.Add(_list[i]);
        }

        public T[] GetElements(int from, int to)
        {
            List<T> elements = new List<T>();
            for (int i = from; i < to; i++)
                elements.Add(ListValue[i]);

            T[] t = elements.ToArray();
            return t;
        }

        /// <summary>
        /// Get an element and remove from the list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElementWithoutReplacement(int index)
        {
            T element = _list[index];
            RemoveAt(index);
            return element;
        }

        /// <summary>
        /// get elements and remove from the list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T[] GetElementsWithoutReplacement(params int[] index)
        {
            List<T> elements = new List<T>();
            for (int i = 0; i < index.Length; i++)
            {
                elements.Add(_list[index[i]]);
                RemoveAt(index[i]);
            }

            T[] t = elements.ToArray();
            return t;
        }

        /// <summary>
        /// Get randomly an element of the list
        /// </summary>
        /// <returns></returns>
        public T GetRandomElement()
        {
            int x = Random.Range(0, _list.Count + 1);
            return _list[x];
        }

        /// <summary>
        /// Get randomly multiple elements
        /// </summary>
        /// <param name="nbrValuesToReturn"></param>
        /// <returns></returns>
        public T[] GetRandomElements(int nbrValuesToReturn)
        {
            List<T> elements = new List<T>();

            for (int i = 0; i < nbrValuesToReturn; i++)
                elements.Add(GetRandomElement());

            T[] t = elements.ToArray();
            return t;
        }

        /// <summary>
        /// Get randomly an element without replacement
        /// </summary>
        /// <returns></returns>
        public T GetRandomElementWithoutReplacement()
        {
            T element = GetRandomElement();
            Remove(element);
            return element;
        }

        /// <summary>
        /// Get randomly multiple elements whitout replacement
        /// </summary>
        /// <param name="nbrValuesToReturn"></param>
        /// <returns></returns>
        public T[] GetRandomElementsWithoutReplacement(int nbrValuesToReturn)
        {
            T[] elements = GetRandomElements(nbrValuesToReturn);
            Remove(elements);
            return elements;
        }
        #endregion
    }
}
