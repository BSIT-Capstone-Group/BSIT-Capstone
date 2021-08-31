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
    public class Player : Controller, IPlayable {
        public float travelSpeed = 20.0f;
        public float slideSpeed = 30.0f;
        public float slideDistance = 4.0f;
        public Animator vehicleAnimator;
        public List<Light> headLights = new List<Light>();
        public List<Light> brakeLights = new List<Light>();

        public virtual Controller OnPause() {
            return this;

        }

        public virtual Controller OnPlay() {
            return this;

        }

        public override void Awake() {
            base.Awake();
            SetBrakeLights(!HasCoroutine("travel"));

        }

        public override void Update() {
            base.Update();

            if(IInput.keyboard.spaceKey.wasPressedThisFrame) {
                Travel();
                SetBrakeLights();

            }

            if(IInput.keyboard.leftArrowKey.wasPressedThisFrame) Slide(-1);
            if(IInput.keyboard.rightArrowKey.wasPressedThisFrame) Slide(1);

        }

        public float dir = 1;

        public override void FixedUpdate() {
            base.FixedUpdate();
            rigidbody.AddRelativeForce(
                rigidbody.mass * Vector3.forward * travelSpeed * timeScale
            );
            // // rigidbody.AddRelativeForce(
            // //     rigidbody.mass * (Vector3.right * dir) * slideSpeed * timeScale
            // // );
            // // var wc = gameObject.GetComponentInChildren<WheelCollider>();
            // // wc.transform.Rotate(new Vector3(90.0f, wc.transform.rotation.x, wc.transform.rotation.y));
            // // wc.GetWorldPose(out var pos, out var quat);
            // // print(pos, quat.eulerAngles);
            foreach(var wc in gameObject.GetComponentsInChildren<WheelCollider>()) {
                wc.GetWorldPose(out var pos, out var quat);
                wc.gameObject.transform.position = pos;
                wc.gameObject.transform.rotation = quat * Quaternion.Euler(0.0f, -90.0f, 0.0f);

            }

        }

        // public Animation wheelAnimation;

        protected virtual void _Travel() {
            rigidbody.AddRelativeForce(
                rigidbody.mass * Vector3.forward * travelSpeed * timeScale
            );
            foreach(var wc in gameObject.GetComponentsInChildren<WheelCollider>()) {
                wc.GetWorldPose(out var pos, out var quat);
                wc.gameObject.transform.position = pos;
                wc.gameObject.transform.rotation = quat * Quaternion.Euler(0.0f, -90.0f, 0.0f);

            };

        }

        public virtual void Travel() {}

        protected virtual IEnumerator _Slide(int amount) {
            if(amount < 0) vehicleAnimator.SetTrigger("slidLeft");
            else if(amount > 0) vehicleAnimator.SetTrigger("slidRight");
            // vehicleAnimator.speed += Mathf.Abs(amount) * Time.deltaTime * slideSpeed * timeScale;

            rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
            var ixpos = rigidbody.position.x;
            var txpos = ixpos + (slideDistance * amount);
            var dir = amount < 0 ? Vector3.left : Vector3.right;
            while(amount < 0 ? rigidbody.position.x > txpos : rigidbody.position.x < txpos) {
                rigidbody.OffsetPosition(dir * Time.deltaTime * slideSpeed * timeScale);
                // rigidbody.AddForce(
                //     rigidbody.mass * dir * slideSpeed * timeScale
                // );
                // // rigidbody.WakeUp();
                // print("sliding");
                yield return new WaitForFixedUpdate();

            }

            // var vel = rigidbody.velocity;
            // rigidbody.velocity = new Vector3(0.0f, vel.y, vel.z);
            var pos = transform.position;
            pos.x = txpos;
            transform.Move(pos);
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
            vehicleAnimator.SetInteger("slideAmount", 0);

        }

        public virtual void Slide(int amount) => StartCoroutine(
            MakeCoroutine("slide", _Slide(amount)), "slide", true
        );

        public virtual void SetHeadLights(bool on) {
            foreach(var l in headLights) l.gameObject.SetActive(on);

        }

        public virtual void SetBrakeLights(bool on) {
            foreach(var l in brakeLights) l.gameObject.SetActive(on);

        }

        public virtual void SetBrakeLights() => SetBrakeLights(!HasCoroutine("travel"));

    }

}