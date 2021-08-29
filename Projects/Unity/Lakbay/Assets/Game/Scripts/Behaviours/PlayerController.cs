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

        public bool canTravel = false;
        public float travelSpeed = 20.0f;
        public float slideSpeed = 30.0f;
        public float slideDistance = 4.0f;
        protected Vector3 _slideTargetPosition = Vector3.zero;

        public virtual Controller OnPause() {
            return this;
        }

        public virtual Controller OnPlay() {
            return this;

        }

        public override void Update() {
            base.Update();

            if(IInput.keyboard.spaceKey.wasPressedThisFrame) {
                ToggleCoroutine(Travel(), "travel");

            }

            if(IInput.keyboard.leftArrowKey.wasPressedThisFrame) {
                StartCoroutine(Slide(-1), "slide", true);

            }

            if(IInput.keyboard.rightArrowKey.wasPressedThisFrame) {
                StartCoroutine(Slide(1), "slide", true);

            }

        }

        public override void FixedUpdate() {
            base.FixedUpdate();

        }

        public virtual IEnumerator Travel() {
            while(true) {
                rigidbody.OffsetPosition(Vector3.forward * deltaTime * travelSpeed);
                yield return new WaitForFixedUpdate();

            }

        }

        protected virtual IEnumerator _Slide(int amount) {
            var ixpos = rigidbody.position.x;
            var txpos = ixpos + (slideDistance * amount);
            var dir = amount < 0 ? Vector3.left : Vector3.right;
            while(amount < 0 ? rigidbody.position.x > txpos : rigidbody.position.x < txpos) {
                rigidbody.OffsetPosition(dir * deltaTime * slideSpeed);
                yield return new WaitForFixedUpdate();

            }

            var pos = rigidbody.position;
            pos.x = txpos;
            rigidbody.position = pos;

        }

        public virtual IEnumerator Slide(int amount) => MakeCoroutine("slide", _Slide(amount));

    }

}