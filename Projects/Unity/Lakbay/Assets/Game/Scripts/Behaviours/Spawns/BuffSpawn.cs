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

        public override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
            var player = collision.gameObject.GetComponent<Player>();
            if(player) {
                player.AddBuff(buff);
                Destroy(gameObject);

            }

        }

        public override void OnCollisionExit(Collision collision) {
            base.OnCollisionExit(collision);
            var p = collision.gameObject.GetComponent<Player>();
            if(p) {

            }

        }

        public override Spawn OnSpawn(List<Spawn>[] spawns, int index) {
            var cur = spawns[index];
            cur.Clear();
            return this;

        }

    }

}