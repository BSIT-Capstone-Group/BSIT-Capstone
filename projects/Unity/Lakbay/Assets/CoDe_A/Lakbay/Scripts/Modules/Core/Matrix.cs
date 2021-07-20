/*
 * Date Created: Tuesday, July 20, 2021 2:45 PM
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

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IMatrix<T> : IEnumerable<List<T>> {
        Vector2Int count { get; } 
        int totalCount { get; }
        List<List<T>> xs { get; set; }

        bool Get(Vector2Int point, out T value);
        bool Get(int x, int y, out T value);
        T Get(int x, int y);
        bool Set(Vector2Int point, T value, out T oldValue);
        bool Set(int x, int y, T value, out T oldValue);
        bool Set(Vector2Int point, T value);
        bool Set(int x, int y, T value);
        
    }

    [Serializable]
    public struct Matrix<T> : IMatrix<T> {
        public Vector2Int count {
            get {
                int x = xs != null ? xs.Max((r) => r != null ? r.GetCount() : 0) : 0;
                int y = xs != null ? xs.GetCount() : 0;
                return new Vector2Int(x, y);

            }

        }
        public int totalCount => count.x * count.y;
        
        private List<List<T>> _xs;
        public List<List<T>> xs { get => _xs; set => _xs = value; }


        public List<T> this[int x] {
            get => xs[x];
            set => xs[x] = value;

        }

        public T this[int x, int y] {
            get => this[x][y];
            set => this[x][y] = value;

        }

        public Matrix(params List<T>[] xs) {
            this._xs = xs.ToList();

        }

        public void AddX(int count, int size=0) {
            if(xs == null) xs = new List<List<T>>();
            for(int i = 0; i < count; i++) xs.Add(
                new List<T>(
                    from _ in Enumerable.Range(0, size) select default(T)
                )
            );

        }

        public bool Get(Vector2Int point, out T value) {
            value = default;

            if(point.x >= 0 && point.x < count.x) {
                if(point.y >= 0 && point.y < count.y) {
                    value = xs[point.x][point.y];
                    return true;

                }

            }

            return false;

        }

        public bool Get(int x, int y, out T value) => Get(new Vector2Int(x, y), out value);

        public T Get(Vector2Int point) {
            Get(point, out T value);
            return value;

        }

        public T Get(int x, int y) => Get(new Vector2Int(x, y));

        public bool Set(Vector2Int point, T value, out T oldValue) {
            if(Get(point, out oldValue)) {
                if(point.y >= 0 && point.y < count.y) {
                    xs[point.x][point.y] = value;
                    return true;

                }

            }

            return false;

        }

        public bool Set(int x, int y, T value, out T oldValue) {
            return Set(new Vector2Int(x, y), value, out oldValue);

        }

        public bool Set(Vector2Int point, T value) => Set(point, value, out T _);

        public bool Set(int x, int y, T value) => Set(new Vector2Int(x, y), value);

        public IEnumerator<List<T>> GetEnumerator() => xs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}