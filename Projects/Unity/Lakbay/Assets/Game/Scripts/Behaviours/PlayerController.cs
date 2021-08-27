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
        public float travelSpeed = 2.0f;
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
                canTravel = !canTravel;

            }

            if(IInput.keyboard.leftArrowKey.wasPressedThisFrame) {
                _slideTargetPosition = rigidbody.position;
                Slide(-1);

            }

            if(IInput.keyboard.rightArrowKey.wasPressedThisFrame) {
                Slide(1);

            }

        }

        public override void FixedUpdate() {
            base.Update();
            if(canTravel) Travel();

        }

        public virtual void Travel() {
            rigidbody.MovePosition(rigidbody.position + Vector3.forward * deltaTime * travelSpeed);

        }

        public virtual void Slide(int amount) {
            rigidbody.MovePosition(rigidbody.position + (Vector3.right * slideDistance * amount) * deltaTime * travelSpeed);

        }

    }

}