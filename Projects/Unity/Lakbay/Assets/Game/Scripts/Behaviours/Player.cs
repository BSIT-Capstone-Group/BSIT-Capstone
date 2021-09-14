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
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Player : Controller, IPlayable, IBuffable {
        public readonly List<Buff> buffs = new List<Buff>();

        public float travelSpeed = 30.0f;
        public int slideMinIndex = -1;
        public int slideMaxIndex = 1;
        protected int _slideIndex = 0;
        public virtual int slideIndex => _slideIndex;
        public float slideSpeed = 30.0f;
        public float slideDistance = 4.0f;
        public GameObject buffHolder;
        public Vehicle vehicle;
        public SpawnerRepeater spawnerRepeater;
        public virtual Animator vehicleAnimator => vehicle.GetComponent<Animator>();

        public virtual Controller OnPause() {
            return this;

        }

        public virtual Controller OnPlay() {
            return this;

        }

        public override void Awake() {
            base.Awake();

        }

        public override void Update() {
            base.Update();

            foreach(var buff in buffs) buff.OnLinger(this);

            vehicle.SetBrakeLights(!_fixedUpdateCallbacks.Contains(_Travel));
            if(spawnerRepeater) {
                // float speed = 1.0f / travelSpeed * 8.5f;
                // if(!_fixedUpdateCallbacks.Contains(_Travel)) speed = 0.0f;
                // print(speed);
                // spawnerRepeater.repeatSpeed = speed;

            }

        }

        public override void FixedUpdate() {
            base.FixedUpdate();

        }

        public override void OnInput(Keyboard keyboard) {
            base.OnInput(keyboard);

            if(keyboard.spaceKey.wasPressedThisFrame) {
                ToggleTravel();

            }

            if(keyboard.leftArrowKey.wasPressedThisFrame) SlideLeft();
            if(keyboard.rightArrowKey.wasPressedThisFrame) SlideRight();

        }

        protected virtual void _Travel() {
            rigidbody.OffsetPosition(Vector3.forward * travelSpeed * Time.deltaTime * timeScale);

        }

        public virtual void StartTravel() {
            if(!_fixedUpdateCallbacks.Contains(_Travel)) {
                _fixedUpdateCallbacks.Add(_Travel);

            }

        }

        public virtual void StopTravel() {
            if(_fixedUpdateCallbacks.Contains(_Travel)) {
                _fixedUpdateCallbacks.Remove(_Travel);

            }

        }

        public virtual void ToggleTravel() {
            if(_fixedUpdateCallbacks.Contains(_Travel)) StopTravel();
            else StartTravel();

        }

        protected virtual IEnumerator _Slide(float startXPosition, float endXPosition) {
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
            rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
            var dir = startXPosition > endXPosition ? Vector3.left : Vector3.right;
            while(
                dir.x == -1 ?
                transform.position.x > endXPosition :
                transform.position.x < endXPosition
            ) {
                rigidbody.OffsetPosition(dir * slideSpeed * Time.deltaTime * timeScale);
                yield return new WaitForFixedUpdate();

            }

            var pos = transform.position;
            pos.x  = endXPosition;
            transform.position = pos;
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
            rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;

        }

        public virtual void Slide(float startXPosition, float endXPosition) {
            if(!HasCoroutine("slide")) {
                if(startXPosition < endXPosition) {
                    if(slideIndex >= slideMaxIndex) return;
                    vehicleAnimator.SetTrigger("slidRight");
                    _slideIndex++;

                } else {
                    if(slideIndex <= slideMinIndex) return;
                    vehicleAnimator.SetTrigger("slidLeft");
                    _slideIndex--;

                }

                StartCoroutine(
                    MakeCoroutine("slide", _Slide(startXPosition, endXPosition)),
                    "slide"
                );

            }

        }

        public virtual void SlideLeft() {
            var startXPosition = transform.position.x;
            var endXPosition = startXPosition - slideDistance;
            Slide(startXPosition, endXPosition);

        }

        public virtual void SlideRight() {
            var startXPosition = transform.position.x;
            var endXPosition = startXPosition + slideDistance;
            Slide(startXPosition, endXPosition);

        }

        public virtual void AddBuff(params Buff[] buffs) {
            foreach(var buff in buffs) {
                if(!buff.STACKABLE) {
                    var existingBuff = this.buffs.Find(
                        (b) => b.GetType() == buff.GetType()
                    );
                    if(existingBuff) RemoveBuff(existingBuff);

                }

                var newBuff = Instantiate(buff, !buffHolder ? transform : buffHolder.transform);
                var type = typeof(Buff);

                this.buffs.Add(newBuff);
                newBuff.OnAdd(this);

                if(!newBuff.permanent) DoAfter(newBuff.duration, () => RemoveBuff(newBuff));

            }
            
        }

        public virtual void RemoveBuff(params Buff[] buffs) {
            foreach(var buff in buffs) {
                while(this.buffs.Contains(buff)) {
                    this.buffs.Remove(buff);
                    Destroy(buff.gameObject);
                    buff.OnRemove(this);

                }

            }

        }

    }

}