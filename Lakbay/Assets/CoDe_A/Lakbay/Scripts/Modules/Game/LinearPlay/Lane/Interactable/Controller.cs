/*
 * Date Created: Sunday, June 27, 2021 12:44 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Lane.Interactable {
    public interface IController : Input.IController {
        bool canBeCollided { get; set; }
        bool oneTimeCollision { get; set; }

        void OnCollide(Collision other);

    }

    public class Controller : Input.Controller, IController {
        [SerializeField, Label("Can Be Collided"), BoxGroup("Interactable.Controller")]
        private bool __canBeCollided = true;
        protected bool _canBeCollided = true;
        public bool canBeCollided {
            get => _canBeCollided;
            set {
                if(value == canBeCollided) return;
                _canBeCollided = value;
                __canBeCollided = value;

            }
            
        }

        [SerializeField, Label("One Time Collision"), BoxGroup("Interactable.Controller")]
        private bool __oneTimeCollision = true;
        protected bool _oneTimeCollision = true;
        public bool oneTimeCollision {
            get => _oneTimeCollision;
            set {
                if(value == oneTimeCollision) return;
                _oneTimeCollision = value;
                __oneTimeCollision = value;

            }
            
        }

        public override void OnNeedsUpdate() {
            base.OnNeedsUpdate();

            canBeCollided = __canBeCollided;
            oneTimeCollision = __oneTimeCollision;

        }

        public override void OnCollisionEnter(Collision other) {
            base.OnCollisionEnter(other);
            if(canBeCollided) {
                OnCollide(other);
                if(oneTimeCollision) canBeCollided = false;

            }

        }

        public virtual void OnCollide(Collision other) {


        }

    }

}