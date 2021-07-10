/*
 * Date Created: Wednesday, June 30, 2021 8:21 AM
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
using IC = Code_A_Old_2.Lakbay.Modules.Core.Interactable.IController;

namespace Code_A_Old_2.Lakbay.Modules.Core.Interactable {
    public interface IController : Core.IController {
        bool showInteractableData { get; }
        new Data data { get; set; }

        void DetectInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen);
        void OnHighlightedChange(bool value);

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Interactable.Controller";

        public override bool showCoreData => false;
        public virtual bool showInteractableData => true;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showInteractableData")]
        protected Data m_interactableData;
        protected Data _interactableData;
        Data IController.data {
            get => _interactableData;
            set {
                value ??= new Data();
                if(value == As<IC>().data) return;
                (this as Core.IController).data = value;
                if(As<IC>().data != null) As<IC>().data.controller = null;
                value.controller = this;
                _interactableData = m_interactableData = value;

            }

        }
    

        public Controller() : base() {
            As<IC>().data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            As<IC>().data?.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => As<IC>().data = new Data(textAsset);

        public override void Update() {
            base.Update();
            if(As<IC>().data.canDetectInput) DetectInput(
                Input.System.keyboard, Input.System.mouse, Input.System.touchscreen
            );

        }

        public virtual void DetectInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen) {
            

        }

        public virtual void OnHighlightedChange(bool value) {


        }

    }

}