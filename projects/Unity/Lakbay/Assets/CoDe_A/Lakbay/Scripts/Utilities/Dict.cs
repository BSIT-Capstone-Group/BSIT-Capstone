using System.Runtime.CompilerServices;
/*
 * Date Created: Friday, July 16, 2021 8:55 PM
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
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IDict<TKey, TValue> : IDictionary<TKey, TValue> {

        
    }

    [Serializable]
    public class Dict<TKey, TValue> : IDict<TKey, TValue> {
        public class EntryKeyEqualityComparer : IEqualityComparer<Entry> {
            public bool Equals(Entry x, Entry y) {
                return CompareKey(x.key, y.key);

            }

            public int GetHashCode(Entry obj) {
                return obj.GetHashCode();

            }

        }

        public class EntryValueEqualityComparer : IEqualityComparer<Entry> {
            public bool Equals(Entry x, Entry y) {
                return CompareValue(x.value, y.value);

            }

            public int GetHashCode(Entry obj) {
                return obj.GetHashCode();

            }

        }

        public class EntryEqualityComparer : IEqualityComparer<Entry> {
            public readonly static EntryKeyEqualityComparer KeyComparer = new EntryKeyEqualityComparer();
            public readonly static EntryValueEqualityComparer ValueComparer = new EntryValueEqualityComparer();

            public bool Equals(Entry x, Entry y) {
                return KeyComparer.Equals(x, y) && ValueComparer.Equals(x, y);

            }

            public int GetHashCode(Entry obj) {
                return obj.GetHashCode();

            }

        }

        protected static readonly EntryKeyEqualityComparer KeyComparer = new EntryKeyEqualityComparer();
        protected static readonly EntryValueEqualityComparer ValueComparer = new EntryValueEqualityComparer();
        protected static readonly EntryEqualityComparer Comparer = new EntryEqualityComparer();

        [Serializable]
        public struct Entry {
            public TKey key;
            public TValue value;

            public Entry(TKey key, TValue value=default) {
                this.key = key;
                this.value = value;

            }

        }

        [SerializeField]
        protected List<Entry> _entries = new List<Entry>();

        public TValue this[TKey key] {
            get => _entries.Find((e) => CompareKey(e.key, key)).value;
            set {
                var r = _entries.Find((e) => CompareKey(e.key, key));
                r.value = value;

            }

        }

        public ICollection<TKey> Keys => _entries.Select((e) => e.key) as ICollection<TKey>;

        public ICollection<TValue> Values => _entries.Select((e) => e.value) as ICollection<TValue>;

        public int Count => _entries.Count;

        public bool IsReadOnly => false;

        public void Add(TKey key, TValue value) {
            _entries.Add(new Entry(key, value));

        }

        public void Add(KeyValuePair<TKey, TValue> item) {
            Add(item.Key, item.Value);

        }

        public void Clear() {
            _entries.Clear();

        }

        public bool Contains(KeyValuePair<TKey, TValue> item) {
            return _entries.Contains(new Entry(item.Key, item.Value), Comparer);

        }

        public bool ContainsKey(TKey key) {
            return _entries.Contains(new Entry(key), KeyComparer);

        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) {
            for(int i = 0; i < _entries.Count; i++) {
                var e = _entries[i];
                array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(e.key, e.value);

            }

        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
            foreach(var e in _entries) {
                yield return new KeyValuePair<TKey, TValue>(e.key, e.value);

            }

        }

        public bool Remove(TKey key) {
            var r = _entries.Find((e) => CompareKey(e.key, key));
            return _entries.Remove(r);

        }

        public bool Remove(KeyValuePair<TKey, TValue> item) {
            var r = _entries.Find((e) => CompareKey(e.key, item.Key) && CompareValue(e.value, item.Value));
            return _entries.Remove(r);

        }

        public bool TryGetValue(TKey key, out TValue value) {
            value = default;
            var r = ContainsKey(key);
            if(r) value = this[key];

            return r;

        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();

        }

        protected static bool CompareKey(TKey key1, TKey key2) {
            return key1.Equals(key2);

        }

        protected static bool CompareValue(TValue value1, TValue value2) {
            return value1.Equals(value2);

        }

        public Dict() {}

        public Dict(IDictionary<TKey, TValue> dictionary) {
            foreach(var i in dictionary) _entries.Add(new Entry(i.Key, i.Value));
            
        }

        public Dict(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs) {
            foreach(var k in keyValuePairs) _entries.Add(new Entry(k.Key, k.Value));

        }

        public Dict(params KeyValuePair<TKey, TValue>[] keyValuePairs) : this(keyValuePairs.AsEnumerable()) {}

        public Dict(IEnumerable<Tuple<TKey, TValue>> tuples) {
            foreach(var t in tuples) _entries.Add(new Entry(t.Item1, t.Item2));
            
        }
        
        public Dict(params Tuple<TKey, TValue>[] tuples) : this(tuples.AsEnumerable()) {

        }

    }

}