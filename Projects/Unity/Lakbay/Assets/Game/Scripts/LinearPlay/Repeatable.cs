/*
 * Date Created: Friday, August 27, 2021 9:37 AM
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

namespace Ph.CoDe_A.Lakbay.LinearPlay {
    using Core;

    public class Repeatable : Controller {
        protected bool _occupied = false;
        public virtual bool occupied => _occupied;
        protected bool _occupiedEver = false;
        public virtual bool occupiedEver => _occupiedEver;

        public override void Awake() {
            base.Awake();
            _occupied = true;

        }

        public virtual void OnRepeat(Repeater repeater, int index) {
            _occupiedEver = false;

        }

        public override void Update() {
            base.Update();
            // var collider = GetComponent<Collider>();
            // collider.isTrigger = true;

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            var source = collider.GetComponent<RepeatableTriggerSource>();
            if(source) {
                _occupied = true;
                if(!occupiedEver) {
                    _occupiedEver = true;

                }

            }

        }

        public override void OnTriggerStay(Collider collider) {
            base.OnTriggerStay(collider);
            var source = collider.GetComponent<RepeatableTriggerSource>();
            if(source) {
                _occupied = true;

            }

        }

        public override void OnTriggerExit(Collider collider) {
            base.OnTriggerExit(collider);
            var source = collider.GetComponent<RepeatableTriggerSource>();
            if(source) {
                _occupied = false;

            }

        }

    }

}