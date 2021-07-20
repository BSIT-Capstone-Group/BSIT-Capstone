/*
 * Date Created: Friday, July 16, 2021 6:56 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Utilities {
    public interface IList2D<T> : IList<T> {

        
    }

    [Serializable]
    public class List2D<T> : IList2D<T> {
        [SerializeField]
        protected List<T> _children = new List<T>();
 
        public T this[int index] {
            get => _children[index];
            set => _children[index] = value;

        }

        public int Count => _children.Count;

        public bool IsReadOnly => false;

        public void Add(T item) {
            _children.Add(item);

        }

        public void Clear() {
            _children.Clear();

        }

        public bool Contains(T item) {
            return _children.Contains(item);

        }

        public void CopyTo(T[] array, int arrayIndex) {
            _children.CopyTo(array, arrayIndex);

        }

        public IEnumerator<T> GetEnumerator() {
            return (_children as IList<T>).GetEnumerator();

        }

        public int IndexOf(T item) {
            return _children.IndexOf(item);

        }

        public void Insert(int index, T item) {
            _children.Insert(index, item);

        }

        public bool Remove(T item) {
            return _children.Remove(item);

        }

        public void RemoveAt(int index) {
            _children.RemoveAt(index);

        }

        IEnumerator IEnumerable.GetEnumerator() {
            return (_children as IEnumerable).GetEnumerator();

        }

        public List2D() {}

        public List2D(int capacity) { _children = new List<T>(capacity); }

        public List2D(IEnumerable<T> collection) { _children = new List<T>(collection); }

        public List2D(params T[] items) : this(items.AsEnumerable<T>()) { }

    }

}