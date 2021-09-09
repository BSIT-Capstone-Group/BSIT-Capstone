/*
 * Date Created: Wednesday, September 8, 2021 10:53 AM
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
using UnityEngine.InputSystem;
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours.Spawns {
    public class RandomSpawn : Spawn {
        public List<Spawn> spawns;

        public override Spawn OnSpawn(List<Spawn>[] spawns, int index) {
            var spawn = this.spawns.PickRandomly();
            spawn.OnSpawn(spawns, index);
            return spawn;

        }

    }

}