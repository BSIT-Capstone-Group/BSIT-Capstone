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

namespace Ph.CoDe_A.Lakbay.LinearPlay {
    public class SpawnerRepeater : Repeater {
        public int spawnStartIndex = 1;
        public List<Spawn> spawns = new List<Spawn>();

        public override void Awake() {
            base.Awake();
            
        }

    }

}