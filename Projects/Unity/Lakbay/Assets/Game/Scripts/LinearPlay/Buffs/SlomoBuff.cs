/*
 * Date Created: Wednesday, September 8, 2021 12:11 PM
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

namespace Ph.CoDe_A.Lakbay.LinearPlay.Buffs {
    public class SlomoBuff : Buff {
        public float factor = 0.50f;

        public override void OnAdd(IBuffable buffable) {
            base.OnAdd(buffable);
            var player = buffable as Player;
            player.travelSpeed *= factor;
            
        }

        public override void OnLinger(IBuffable buffable) {
            base.OnLinger(buffable);

        }

        public override void OnRemove(IBuffable buffable) {
            base.OnRemove(buffable);
            var player = buffable as Player;
            player.travelSpeed /= factor;

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);

        }
        

    }

}