/*
 * Date Created: Tuesday, September 14, 2021 7:19 PM
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
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    public class Trigger : Controller {
        protected Collider _sourceCollider;
        public virtual Collider sourceCollider => _sourceCollider;
        protected TriggerSource _source;
        public virtual TriggerSource source => _source;

        public override void Awake() {
            base.Awake();
            if(!GetComponent<Rigidbody>()) {
                var rigidbody = gameObject.AddComponent<Rigidbody>();
                rigidbody.isKinematic = true;

            }
            
            var colliders = GetComponentsInChildren<Collider>();
            if(colliders.Length > 0) {
                if(!colliders.Find((c) => c.isTrigger)) colliders.First().isTrigger = true;

            }

        }

        public override void Update() {
            base.Update();

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            _sourceCollider = collider;
            _source = collider?.GetComponentInParent<TriggerSource>();

        }

        public override void OnTriggerStay(Collider collider) {
            base.OnTriggerStay(collider);
            _sourceCollider = collider;
            _source = collider?.GetComponentInParent<TriggerSource>();

        }

        public override void OnTriggerExit(Collider collider) {
            base.OnTriggerExit(collider);
            if(collider == sourceCollider) _sourceCollider = null;
            if(collider?.GetComponentInParent<TriggerSource>() == source)
                _source = null;

        }

    }

}