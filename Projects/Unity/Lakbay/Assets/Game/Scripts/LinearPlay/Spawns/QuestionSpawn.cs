/*
 * Date Created: Thursday, September 9, 2021 6:59 AM
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

namespace Ph.CoDe_A.Lakbay.LinearPlay.Spawns {
    public class QuestionSpawn : Spawn {
        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var player = collider.gameObject.GetComponentInParent<Player>();
            if(player) {
                // player.AddBuff(buff);
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