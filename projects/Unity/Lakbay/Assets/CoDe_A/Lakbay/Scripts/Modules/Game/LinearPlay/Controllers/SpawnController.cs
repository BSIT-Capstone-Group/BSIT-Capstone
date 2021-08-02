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


    public interface ISpawnController : Core.IFocusable, ISpawnable {


    }

    public class SpawnController : Controller, ISpawnController {
        [SerializeField]
        private string _key = "";
        public string key { get => _key; set => _key = value; }
        [SerializeField]
        private List<string> _notKeys = new List<string>();
        public List<string> notKeys { get => _notKeys; set => _notKeys = value; }
        [SerializeField]
        private float _chance = 1.0f;
        public float chance { get => _chance; set => _chance = value; }
        [SerializeField]
        private AxisInfo _column = new AxisInfo(-1);
        public AxisInfo column { get => _column; set => _column = value; }
        [SerializeField]
        private AxisInfo _row = new AxisInfo(-1);
        public AxisInfo row { get => _row; set => _row = value; }


        public virtual string OnPlot(in List<List2D<List2D<string>>> rows, in Vector2Int location, float chance) {
            return key;

        }

        public virtual GameObject OnSpawn(in List<List<GameObject>> rows, in Vector2Int location) {
            // gameObject.TryDestroyComponent<SpawnController>();
            return gameObject;

        }

        public virtual Tuple<GameObject, Focus> OnFocus() {
            return default;

        }

        public virtual GameObject OnUnfocus() {
            return default;

        }
    }

}