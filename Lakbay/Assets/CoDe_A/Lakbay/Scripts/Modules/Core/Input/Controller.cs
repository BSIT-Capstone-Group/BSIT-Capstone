/*
 * Date Created: Wednesday, June 30, 2021 9:13 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem;

using NaughtyAttributes;

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Input {
    public static class System {
        public static bool enabled {
            get => keyboardEnabled || mouseEnabled || touchscreenEnabled;
            set => keyboardEnabled = mouseEnabled = touchscreenEnabled = value;

        }

        public static bool keyboardEnabled = true;
        public static bool mouseEnabled = true;
        public static bool touchscreenEnabled = true;

        public static Keyboard keyboard => keyboardEnabled ? InputSystem.GetDevice<Keyboard>() : null;
        public static Mouse mouse => keyboardEnabled ? InputSystem.GetDevice<Mouse>() : null;
        public static Touchscreen touchscreen => keyboardEnabled ? InputSystem.GetDevice<Touchscreen>() : null;

    }

    public interface IController : Core.IController {
        new Data data { get; set; }

    }

    public class Controller : Core.Controller, IController {
        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;

            }

        }


        public Controller() : base() {
            (this as IController).data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => (this as IController).data = new Data(textAsset);

        public static void SetKeyboardInput(bool enabled) => System.keyboardEnabled = enabled;

        public static void SetMouseInput(bool enabled) => System.mouseEnabled = enabled;

        public static void SetTouchscreenInput(bool enabled) => System.touchscreenEnabled = enabled;

        public static void SetInput(bool keyboardEnabled=true, bool mouseEnabled=true, bool touchscreenEnabled=true) {
            SetKeyboardInput(keyboardEnabled);
            SetMouseInput(mouseEnabled);
            SetTouchscreenInput(touchscreenEnabled);

        }

        public static void SetInput(bool enabled) {
            System.enabled = enabled;

        }

    }

}