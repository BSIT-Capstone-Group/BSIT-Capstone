/*
 * Date Created: Tuesday, July 13, 2021 1:14 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
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

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Core.Interactable {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IController : Minimal.IController {
        void OnCollision(Collider collider);

        void OnPlayingChange(bool old, bool @new);
        void OnHandlingInputsChange(bool old, bool @new);
        
    }

    public interface IController<T> : Minimal.IController<T>, IController
        where T : IData {
        
    }

    public class Controller<T> : Minimal.Controller<T>, IController<T>
        where T : IData, new() {
        public override void OnCollisionEnter(Collision collision) {
            base.OnCollisionEnter(collision);
            if(data.maxCollisionCount < 0 ||
                (data.maxCollisionCount >= 0 &&
                data.collisionCount != data.maxCollisionCount)) {
                    OnCollision(collision.collider);
                    data.collisionCount++;

            }

        }

        public override void OnTriggerEnter(Collider collider) {
            base.OnTriggerEnter(collider);
            if(data.maxCollisionCount < 0 ||
                (data.maxCollisionCount >= 0 &&
                data.collisionCount != data.maxCollisionCount)) {
                    OnCollision(collider);
                    data.collisionCount++;

            }

        }

        public virtual void OnCollision(Collider collider) {


        }

        public virtual void OnPlayingChange(bool old, bool @new) {
            

        }

        public virtual void OnHandlingInputsChange(bool old, bool @new) {
            

        }

    }

}