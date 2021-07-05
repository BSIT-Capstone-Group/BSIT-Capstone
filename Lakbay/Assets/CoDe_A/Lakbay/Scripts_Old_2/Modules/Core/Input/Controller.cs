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

using Code_A_Old_2.Lakbay.Utilities;
using IC = Code_A_Old_2.Lakbay.Modules.Core.Input.IController;

namespace Code_A_Old_2.Lakbay.Modules.Core.Input {
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
        bool showInputData { get; }
        new Data data { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Input.Controller";

        public override bool showCoreData => false;
        public virtual bool showInputData => true;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showInputData")]
        protected Data m_inputData;
        protected Data _inputData;
        Data IController.data {
            get => _inputData;
            set {
                value ??= new Data();
                if(value == As<IC>().data) return;
                As<Core.IController>().data = value;
                if(As<IC>().data != null) As<IC>().data.controller = null;
                value.controller = this;
                _inputData = m_inputData = value;

            }

        }


        public Controller() : base() {
            As<IC>().data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            As<IC>().data = m_inputData;
            As<IC>().data?.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => As<IC>().data = new Data(textAsset);

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