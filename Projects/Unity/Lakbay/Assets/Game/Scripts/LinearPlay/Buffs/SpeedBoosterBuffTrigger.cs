/*
 * Date Created: Wednesday, September 8, 2021 6:56 AM
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
    public class SpeedBoosterBuffTrigger : Controller {
        protected bool _triggered = false;
        protected Collider _triggeringCollider;
        public virtual bool triggered => _triggered;
        public virtual Collider triggeringCollider => _triggeringCollider;

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var barricade = collider.GetComponentInParent<Spawns.BarricadeSpawn>();
            if(barricade && barricade.damaging) {
                _triggeringCollider = collider;
                _triggered = true;
                barricade.damaging = false;

            }
            
        }

        public override void OnTriggerExit(Collider collider) {
            base.OnTriggerExit(collider);
            _triggeringCollider = null;
            _triggered = false;
            
        }

    }

}