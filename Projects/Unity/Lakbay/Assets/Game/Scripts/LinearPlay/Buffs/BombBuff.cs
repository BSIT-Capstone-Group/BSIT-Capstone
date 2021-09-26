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

namespace Ph.CoDe_A.Lakbay.LinearPlay.Buffs {
    public class BombBuff : Buff {
        public float explosionForce = 500.0f;

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
            var barricade = collider.GetComponentInParent<Spawns.ObstacleSpawn>();
            if(barricade) {
                barricade.damaging = false;
                barricade.Break((r) => {
                    r.AddExplosionForce(
                        explosionForce, r.transform.position, 100.0f
                    );

                });

            }

        }

    }

}