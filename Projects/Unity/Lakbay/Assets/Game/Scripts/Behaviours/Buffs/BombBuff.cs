/*
 * Date Created: Tuesday, September 7, 2021 5:46 PM
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

namespace Ph.CoDe_A.Lakbay.Behaviours.Buffs {
    public class BombBuff : Buff {
        public override void OnAdd(IBuffable buffable) {
            
        }

        public override void OnLinger(IBuffable buffable) {

        }

        public override void OnRemove(IBuffable buffable) {

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var barricade = collider.GetComponentInParent<Spawns.BarricadeSpawn>();
            if(barricade) {
                barricade.damaging = false;
                barricade.Break((r) => {
                    r.AddExplosionForce(
                        500.0f, r.transform.position, 100.0f
                    );

                });

            }

        }

    }

}