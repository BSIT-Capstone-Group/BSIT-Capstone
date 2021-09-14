/*
 * Date Created: Friday, September 3, 2021 2:24 PM
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
    public class BuffSpawn : Spawn {
        public Buff buff;
        public Trigger trigger;

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            if(trigger && trigger.source) {
                var ts = trigger.source;
                var player = (ts as PlayerTriggerSource)
                    ?.player;
                if(player) {
                    player.AddBuff(buff);
                    Destroy(gameObject);

                }

            }

        }

        public override Spawn OnSpawn(List<Spawn>[] spawns, int index) {
            return this;

        }

    }

}