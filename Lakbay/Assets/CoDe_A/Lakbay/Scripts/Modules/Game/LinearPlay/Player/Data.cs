/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Player {
    // [CreateAssetMenu(fileName="Player", menuName="ScriptableObjects/SpawnManagerScriptableObject")]
    public class Data : Core.Data {
        public float travelDuration = 60.0f;
        public float travelSpeed = 1.0f;
        public float slideDuration = 0.15f;
        public float slideSpeed = 1.0f;
        public bool isTraveling = false;
        public int laneIndex = 1;

    }

}
