/*
 * Date Created: Friday, September 3, 2021 2:19 PM
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
    public class BarricadeSpawn : Spawn {
        public bool damaging = true;
        public bool broken = false;

        public override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
            var player = collision.gameObject.GetComponent<Player>();
            Damage(player);

        }

        public virtual void Break(Action<Rigidbody> action=null) {
            if(!broken) {
                rigidbody.Break(action);
                broken = true;

            }

        }

        public virtual void Damage(Player player) {
            if(player) {
                Break();

                if(damaging) {
                    // TODO: Logic for reducing lives goes here...
                    print($"Player hit a Barricade!");
                    damaging = false;

                }

            }

        }

    }

}