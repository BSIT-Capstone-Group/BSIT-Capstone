/*
 * Date Created: Wednesday, September 1, 2021 1:09 AM
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
    public class Vehicle : Controller {
        public virtual Wheel[] wheels {
            get => gameObject.GetComponentsInChildren<Wheel>().Where((w) => w.enabled).ToArray();

        }

        public float maxMotorTorque = 20.0f;
        public float maxBrakeTorque = 100.0f;
        public float maxSteerAngle = 30.0f;
        public Transform offsetTransform;
        public List<Light> headLights = new List<Light>();
        public List<Light> brakeLights = new List<Light>();


        public override void FixedUpdate() {
            base.FixedUpdate();
            PoseWheels();

        }

        public virtual void PoseWheels() {
            foreach(var wc in wheels) {
                wc.Pose();
                var orot = Quaternion.identity.eulerAngles;
                if(offsetTransform) {
                    orot = offsetTransform.rotation.eulerAngles;

                }

                wc.transform.position *= timeScale;
                wc.transform.rotation *= Quaternion.Euler(
                    orot * timeScale
                );

            }

        }

        public virtual void SetHeadLights(bool on) {
            foreach(var l in headLights) l.gameObject.SetActive(on);

        }

        public virtual void SetBrakeLights(bool on) {
            foreach(var l in brakeLights) l.gameObject.SetActive(on);

        }

        public virtual void Accelerate(float torque) {
            var wheels = this.wheels.Where((w) => w.hasMotor);
            float dt = torque / wheels.Count();
            foreach(var wheel in wheels) wheel.Motor(dt);

        }

        public virtual void Brake(float torque) {
            var wheels = this.wheels.Where((w) => w.canBrake);
            float dt = torque / wheels.Count();
            foreach(var wheel in wheels) wheel.Brake(dt);

        }

        public virtual void Steer(float angle) {
            var wheels = this.wheels.Where((w) => w.canSteer);
            float da = angle / wheels.Count();
            foreach(var wheel in wheels) wheel.Steer(da);

        }

    }

}