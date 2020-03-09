using System.Collections.Generic;
using System;
using System.Linq;

using Random = UnityEngine.Random;
using UnityEngine;

namespace QRTools.Functions
{
    public class ListWithoutReplacement<T>
    {
        #region Variable & Properties
        public List<T>
            list = new List<T>(),
            savedList = new List<T>();

        public int Count
        {
            get => list.Count;
        }
        #endregion

        #region Constructeur
        public ListWithoutReplacement()
        {
            SaveList();
        }

        public ListWithoutReplacement(List<T> listToInject)
        {
            InitList(listToInject);
        }
        #endregion

        /// <summary>
        /// Inject a list in the <see cref="list"/>
        /// </summary>
        /// <param name="listToInject"></param>
        public void InitList(List<T> listToInject)
        {
            for (int i = 0; i < listToInject.Count; i++)
                this.list.Add(listToInject[i]);

            SaveList();
        }

        /// <summary>
        /// Save the list into saved_list
        /// </summary>
        public void SaveList()
        {
            for (int i = 0; i < list.Count; i++)
                savedList.Add(list[i]);
        }

        /// <summary>
        /// Reset the list with the saved_list
        /// </summary>
        public void ResetList()
        {
            for (int i = 0; i < savedList.Count; i++)
                list.Add(savedList[i]);
        }

        /// <summary>
        /// Get an element and remove it from the list with an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetElementWithoutReplacement(int index)
        {
            var result = list[index];
            list.RemoveAt(index);
            return result;
        }

        /// <summary>
        /// Get an element and remove it from the list
        /// </summary>
        public T GetElementWithoutReplacement()
        {
            int i = Random.Range(0, Count);
            return GetElementWithoutReplacement(i);
        }

        /// <summary>
        /// Get an element and remove it from the list and reset the list if it's reach null;
        /// </summary>
        /// <param name="resetListIfEmpty"></param>
        /// <returns></returns>
        public T GetElementWithoutReplacement(bool resetListIfEmpty)
        {
            T obj = GetElementWithoutReplacement();

            if (resetListIfEmpty)
                if (Count <= 0)
                    ResetList();
            return obj;
        }
    }
}
