/*
 * Date Created: Sunday, July 25, 2021 8:59 AM
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
using CoDe_A.Lakbay.Modules.Core;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Controllers {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using RowList = List<List2D<List2D<string>>>;


    public interface ISpawnController :
        Core.IController,
        Core.IFocusable,
        Core.ISaveable,
        Core.ISaveable<ISpawn>,
        ISpawn {
        [YamlIgnore]
        List<GameObject> gameObjects { get; set; }

    }
    
    public class SpawnController : Controller, ISpawnController {
        [SerializeField]
        protected string _key = "";
        public virtual string key { get => _key; set => Helper.Set(ref _key, value); }
        [SerializeField]
        protected List<string> _notKeys = new List<string>();
        public virtual List<string> notKeys { get => _notKeys; set => Helper.Set(ref _notKeys, value); }
        [SerializeField]
        protected float _chance = 1.0f;
        public virtual float chance { get => _chance; set => Helper.Set(ref _chance, value); }
        [SerializeField]
        protected AxisInfo _column = new AxisInfo(-1);
        public virtual AxisInfo column { get => _column; set => Helper.Set(ref _column, value); }
        [SerializeField]
        protected AxisInfo _row = new AxisInfo(-1);
        public virtual AxisInfo row { get => _row; set => Helper.Set(ref _row, value); }
        [SerializeField]
        protected List<GameObject> _gameObjects = new List<GameObject>();
        public virtual List<GameObject> gameObjects { get => _gameObjects; set => Helper.Set(ref _gameObjects, value); }


        public virtual string OnPlot(in RowList rows, in Vector2Int location, float chance) {
            return key;

        }

        public virtual GameObject OnSpawn(in List<List<GameObject>> rows, in Vector2Int location) {
            if(gameObjects != null && gameObjects.Count != 0) return gameObjects.PickRandomly();
            return gameObject;

        }

        public virtual Tuple<GameObject, Focus> OnFocus() {
            return default;

        }

        public virtual GameObject OnUnfocus() {
            return default;

        }

        public virtual TextAsset OnSave() {
            var str = this.AsYaml<ISpawnController>();
            var ta = new TextAsset(str);
            print(str);
            return ta;

        }

        public virtual void OnLoad(TextAsset textAsset) {
            var s = textAsset.ParseYaml<SpawnController>();
            key = s.key;
            notKeys = s.notKeys;
            chance = s.chance;
            column = s.column;
            row = s.row;

        }

        public virtual void OnLoad(ISpawn data) {
            key = data.key;
            notKeys = data.notKeys;
            chance = data.chance;
            column = data.column;
            row = data.row;

        }

        [ContextMenu("Save")]
        public virtual void Save() => OnSave();

        [ContextMenu("Load")]
        public virtual void Load() => OnLoad(OnSave());

    }

}