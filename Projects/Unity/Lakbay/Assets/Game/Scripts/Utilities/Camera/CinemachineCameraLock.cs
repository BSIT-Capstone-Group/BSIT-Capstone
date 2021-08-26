/*
 * Date Created: Wednesday, August 25, 2021 5:03 PM
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
using UnityEngine.InputSystem;

using Cinemachine;
using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Utilities.Camera {
    [Serializable]
    public struct ValueLock {
        public bool locked;
        public float value;

    }

    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class CinemachineCameraLock : CinemachineExtension {
        public ValueLock xPosition;
        public ValueLock yPosition;
        public ValueLock zPosition;

        public ValueLock xRotation;
        public ValueLock yRotation;
        public ValueLock zRotation;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage,
            ref CameraState state,
            float deltaTime
        ) {
            if(stage == CinemachineCore.Stage.Body) {
                var position = state.RawPosition;
                position.x = xPosition.locked ? xPosition.value : position.x;
                position.y = yPosition.locked ? yPosition.value : position.y;
                position.z = zPosition.locked ? zPosition.value : position.z;
                state.RawPosition = position;

            } else if(stage == CinemachineCore.Stage.Aim) {
                var qRotation = state.RawOrientation;
                var rotation = qRotation.eulerAngles;
                rotation.x = xRotation.locked ? xRotation.value : rotation.x;
                rotation.y = yRotation.locked ? yRotation.value : rotation.y;
                rotation.z = zRotation.locked ? zRotation.value : rotation.z;
                qRotation.eulerAngles = rotation;
                state.RawOrientation = qRotation;

            }

        }
    }

}