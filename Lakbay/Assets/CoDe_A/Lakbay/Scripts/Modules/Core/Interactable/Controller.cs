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

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Interactable {
    public interface IController : Core.IController {
        new Data data { get; set; }
        bool canDetectInput { get; set; }
        bool highlighted { get; set; }
        Content.Data tutorialContent { get; set; }

        void DetectInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen);
        void OnHighlightedChange(bool value);

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Interactable.Controller";

        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;
                highlighted = value.highlighted;
                tutorialContent = value.tutorialContent;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Can Detect Input")]
        private bool __canDetectInput;
        protected bool _canDetectInput;
        public virtual bool canDetectInput {
            get => _canDetectInput;
            set {
                if(value == canDetectInput) return;
                _canDetectInput = __canDetectInput = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Highlighted")]
        private bool __highlighted;
        protected bool _highlighted;
        public virtual bool highlighted {
            get => _highlighted;
            set {
                if(value == highlighted) return;
                _highlighted = __highlighted = value;
                OnHighlightedChange(highlighted);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Tutorial Content")]
        private Content.Data __tutorialContent;
        protected Content.Data _tutorialContent;
        public virtual Content.Data tutorialContent {
            get => _tutorialContent;
            set {
                if(value == tutorialContent) return;
                _tutorialContent = __tutorialContent = value;

            }

        }


        public Controller() : base() {
            (this as IController).data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            canDetectInput = __canDetectInput;
            highlighted = __highlighted;
            tutorialContent = __tutorialContent;

        }

        public override void SetData(TextAsset textAsset) => (this as IController).data = new Data(textAsset);

        public override void Update() {
            base.Update();
            if(canDetectInput) DetectInput(
                Input.System.keyboard, Input.System.mouse, Input.System.touchscreen
            );

        }

        public virtual void DetectInput(Keyboard keyboard, Mouse mouse, Touchscreen touchscreen) {
            

        }

        public virtual void OnHighlightedChange(bool value) {


        }

    }

}