/*
 * Date Created: Thursday, August 26, 2021 6:29 AM
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
    public class Spawn : Controller {
        public float chance = 0.25f;
        public int interval = 3;
        public int maxCount = 2;

        public virtual Spawn OnSpawn(List<Spawn>[] spawns, int index) {
            return this;

        }

    }

}