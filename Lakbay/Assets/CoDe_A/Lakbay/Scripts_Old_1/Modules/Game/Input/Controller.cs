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

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.Input {
    public static class System {
        public static Keyboard keyboard => InputSystem.GetDevice<Keyboard>();
        public static Mouse mouse => InputSystem.GetDevice<Mouse>();
        public static Touchscreen touchscreen => InputSystem.GetDevice<Touchscreen>();

    }

    public interface IController : Core.IController {
        new Data data { get; set; }
        
    }

    public class Controller : Core.Controller, IController {
        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                data = value;
                canReceiveInput = value.canReceiveInput;

            }

        }

        public override void SetData(TextAsset data) => (this as IController).data = Helper.Parse<Data>(data);

    }

}