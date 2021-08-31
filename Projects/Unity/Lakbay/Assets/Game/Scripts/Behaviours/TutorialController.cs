/*
 * Date Created: Thursday, August 26, 2021 7:04 AM
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
    public class TutorialController : Controller {
        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var go = collider.gameObject;
            var pc = go.GetComponent<Player>();
            // if(pc) {
            //     pc.timeScale = 0.05f;
            //     DoAfter(10.0f, () => pc.timeScale = 1.0f);

            // }

        }

        public override void OnTriggerExit(Collider collider) {
            base.OnTriggerExit(collider);
            var go = collider.gameObject;
            var pc = go.GetComponent<Player>();
            // if(pc) {
            //     // PlayController.instance.Pause(pc);
            //     // pc.deltaTime = 1.0f;

            // }

        }

    }

}