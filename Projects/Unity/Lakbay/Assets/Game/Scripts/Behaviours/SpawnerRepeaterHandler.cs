/*
 * Date Created: Monday, August 30, 2021 7:43 AM
 * Author: NI.L.A
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

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class SpawnerRepeaterHandler : RepeaterHandler {
        public List<Spawn> spawns = new List<Spawn>();
        protected SpawnerRepeater[] _spawnerRepeaters;
        public virtual SpawnerRepeater[] spawnerRepeaters {
            get => _spawnerRepeaters;

        }

        public override void Awake() {
            base.Awake();
            _spawnerRepeaters = GetComponentsInChildren<SpawnerRepeater>();
            
        }

    }

}