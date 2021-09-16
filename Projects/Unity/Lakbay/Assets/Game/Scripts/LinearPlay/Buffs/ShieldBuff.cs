/*
 * Date Created: Friday, September 3, 2021 1:27 PM
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
    public class ShieldBuff : Buff {
        public override void OnAdd(IBuffable buffable) {
            base.OnAdd(buffable);
            
        }

        public override void OnLinger(IBuffable buffable) {
            base.OnLinger(buffable);

        }

        public override void OnRemove(IBuffable buffable) {
            base.OnRemove(buffable);

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var barricade = collider.GetComponentInParent<Spawns.BarricadeSpawn>();
            if(barricade) {
                barricade.damaging = false;

            }

        }

    }

}