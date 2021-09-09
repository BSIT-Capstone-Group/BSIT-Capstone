/*
 * Date Created: Wednesday, September 8, 2021 10:57 AM
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
    public class RandomBuffSpawn : RandomSpawn {
        public override Spawn OnSpawn(List<Spawn>[] spawns, int index) {
            spawns[index].Clear();
            return base.OnSpawn(spawns, index);

        }

    }

}