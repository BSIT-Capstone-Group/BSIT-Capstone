/*
 * Date Created: Monday, July 19, 2021 7:17 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Road.Spawn {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIController = Core.Interactable.IController<Data>;
    using BaseController = Core.Interactable.Controller<Data>;
    using Base = Core.Interactable;

    public interface IController : Base.IController {
        string[] keys { get; }
        List<GameObject> gameObjects { get; set; }

        string[] OnPlot(List<List<List<string[]>>> rows, Vector2Int point, float chance);
        GameObject OnSpawn(Vector2Int point);
        
    }

    public class Controller : Base.Controller, IController {
        public new const string BoxGroupName = "Spawn.Controller";
        public virtual string[] keys => new string[] {"Spawn"};

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected List<GameObject> _gameObjects = new List<GameObject>();
        public virtual List<GameObject> gameObjects {
            get => _gameObjects;
            set {
                if(Helper.SetInvoke(this, ref _gameObjects, value).Item1) {

                }

            }

        }

        public virtual string[] OnPlot(List<List<List<string[]>>> rows, Vector2Int point, float chance) {
            return keys;

        }

        public virtual GameObject OnSpawn(Vector2Int point) {
            return gameObjects?.PickRandomly();

        }

    }

    public interface IController<T> : Base.IController<T>, IController
        where T : IData {
        
    }

    public class Controller<T> : Controller, IController<T>
        where T : IData, new() {
        public new const string BoxGroupName = "Spawn.Controller";

        public override TextAsset dataTextAsset {
            get => base.dataTextAsset;
            set => Core.Controller<T>.SetDataTextAsset(this, ref _dataTextAsset, value);

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T _data;
        public virtual T data {
            get => _data;
            set => Core.Controller<T>.SetData(this, ref _data, value);

        }


        public Controller() : base() {
            data = new T();

        }

        [ContextMenu("Reset")]
        public override void Reset() => data = new T();


        public override string[] OnPlot(List<List<List<string[]>>> rows, Vector2Int point, float chance) {
            var col = from r in rows select r[point.x];
            var row = rows[point.y];
            int hash = this.GetType().GetHashCode();

            int colCount = col.Select(
                (c) => c.Count(
                    (s) => s.GetType().GetHashCode() == hash
                )
            ).Sum();
            int rowCount = row.Select(
                (c) => c.Count(
                    (s) => s.GetType().GetHashCode() == hash
                )
            ).Sum();

            int colMax = data.column.max <= -1 ? rows.GetCount() : data.column.max;
            int rowMax = data.row.max <= -1 ? row.GetCount() : data.row.max;

            var colRange = data.column.range == null || data.column.range.GetCount() == 0 ?
                Enumerable.Range(0, row.GetCount()) : data.column.range;
            var rowRange = data.row.range == null || data.row.range.GetCount() == 0 ?
                Enumerable.Range(0, rows.GetCount()) : data.row.range;

            // print(colRange.First(), colRange.Last());
            // print(rowRange.First(), rowRange.Last());

            var cchance = data.chance <= -1 ? 1.0f : data.chance;

            if(
                chance <= cchance &&
                colCount < colMax && rowCount < rowMax &&
                colRange.Contains(point.x) &&
                rowRange.Contains(point.y)
            ) {
                return base.OnPlot(rows, point, chance);

            }

            return new string[] {};

        }

    }

}