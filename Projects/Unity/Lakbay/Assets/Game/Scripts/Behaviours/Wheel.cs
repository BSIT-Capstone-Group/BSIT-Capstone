/*
 * Date Created: Wednesday, September 1, 2021 2:13 AM
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

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Wheel : Controller {
        public new virtual WheelCollider collider => GetComponent<WheelCollider>();

        public bool hasMotor = true;
        public bool canSteer = true;
        public bool canBrake = true;

        public override void FixedUpdate() {
            base.FixedUpdate();
            
        }

        public virtual void Pose(Vector3 offsetPosition, Quaternion offsetRotation) {
            transform.Pose(collider, offsetPosition, offsetRotation);

        }

        public virtual void Pose(Vector3 offsetPosition) {
            transform.Pose(collider, offsetPosition);

        }

        public virtual void Pose(Quaternion offsetRotation) {
            transform.Pose(collider, offsetRotation);

        }

        public virtual void Pose() {
            transform.Pose(collider);

        }

        public virtual void Steer(float angle) {
            if(canSteer) collider.steerAngle = angle;
            
        }

        public virtual void Motor(float torque) {
            if(hasMotor) collider.motorTorque = torque;
            
        }

        public virtual void Brake(float torque) {
            if(canBrake) collider.brakeTorque = torque;
            
        }

    }

}