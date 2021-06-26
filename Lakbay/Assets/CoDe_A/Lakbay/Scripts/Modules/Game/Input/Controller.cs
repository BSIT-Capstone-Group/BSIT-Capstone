/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
// using GInput = UnityEngine.Input;
using GInput = SimpleInput;
using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.Input {
    public static class System {
        public static Keyboard keyboard => InputSystem.GetDevice<Keyboard>();
        public static Mouse mouse => InputSystem.GetDevice<Mouse>();
        public static Touchscreen touchscreen => InputSystem.GetDevice<Touchscreen>();

    } 

    public interface IController : Core.IController {
        bool canReceiveInput { get; set; }
        void ReceiveInput();

    }

    public class Controller : Core.Controller, IController {
        [SerializeField, BoxGroup("Input.Controller")]
        protected bool _canReceiveInput = true;
        public virtual bool canReceiveInput {
            get => _canReceiveInput;
            set => _canReceiveInput = value;
            
        }


        public virtual void ReceiveInput() {}

        public override void Awake() {

        }

        public override void Update() {
            base.Update();
            if(canReceiveInput) ReceiveInput();
            
        }

    }

}