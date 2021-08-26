/*
 * Date Created: Thursday, August 26, 2021 6:36 AM
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
    public class PlayerController : Controller, IPlayable {
        public float speed = 2.0f;

        public virtual Controller OnPause() {
            return this;
        }

        public virtual Controller OnPlay() {
            return this;

        }

        public override void FixedUpdate() {
            base.Update();
            rigidbody.transform.Translate(Vector3.forward * deltaTime * speed);

        }

    }

}