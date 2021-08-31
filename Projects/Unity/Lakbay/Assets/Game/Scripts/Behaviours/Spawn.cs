/*
 * Date Created: Thursday, August 26, 2021 6:29 AM
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
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Spawn : Controller {
        public float chance = 0.25f;
        public int interval = 3;
        public int maxCount = 2;

        public override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
            var p = collision.gameObject.GetComponent<Player>();
            if(p) {
                var cols = gameObject.GetComponentsInChildren<Collider>();
                if(cols.Length > 0) {
                    float mass = rigidbody.mass / cols.Length;
                    float drag = rigidbody.drag / cols.Length;
                    foreach(var c in cols) {
                        var r = c.gameObject.GetComponent<Rigidbody>();
                        if(!r) r = c.gameObject.AddComponent<Rigidbody>();
                        r.mass = mass;
                        r.drag = drag;

                    }

                }

            }

        }

        public override void OnCollisionExit(Collision collision) {
            base.OnCollisionExit(collision);
            // print("exit");
            var p = collision.gameObject.GetComponent<Player>();
            if(p) {

            }

        }

    }

}