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

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var player = collider.gameObject.GetComponentInParent<Player>();
            if(player) {
                player.AddBuff(buff);
                Destroy(gameObject);

            }

        }

        public override void OnTriggerExit(Collider collider) {
            base.OnTriggerExit(collider);
            var player = collider.gameObject.GetComponent<Player>();
            if(player) {

            }

        }

        public override Spawn OnSpawn(List<Spawn>[] spawns, int index) {
            return this;

        }

    }

}