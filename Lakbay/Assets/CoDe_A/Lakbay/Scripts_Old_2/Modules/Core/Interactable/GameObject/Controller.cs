/*
 * Date Created: Wednesday, June 30, 2021 9:52 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using GameObject_ = UnityEngine.GameObject;

using NaughtyAttributes;

using Code_A_Old_2.Lakbay.Utilities;
using Outline_ = Code_A_Old_2.Lakbay.Utilities.Outline;
using UnityEngine.UI;
using IC = Code_A_Old_2.Lakbay.Modules.Core.Interactable.GameObject.IController;

namespace Code_A_Old_2.Lakbay.Modules.Core.Interactable.GameObject {
    public interface IController : Interactable.IController {
        bool showGameObjectData { get; }
        new Data data { get; set; }
        GameObject_ outlineTarget { get; set; }

        void OnCollide(Collider collider);
        void RegenerateOutline();

    }

    public class Controller : Interactable.Controller, IController {
        public new const string BoxGroupName = "GameObject.Controller";

        public override bool showInteractableData => false;
        public virtual bool showGameObjectData => true;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showGameObjectData")]
        protected Data m_gameObjectData;
        protected Data _gameObjectData;
        Data IController.data {
            get => _gameObjectData;
            set {
                value ??= new Data();
                if(value == As<IC>().data) return;
                (this as Interactable.IController).data = value;
                if(As<IC>().data != null) As<IC>().data.controller = null;
                value.controller = this;
                _gameObjectData = m_gameObjectData = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        private GameObject_ m_outlineTarget;
        protected GameObject_ _outlineTarget;
        public GameObject_ outlineTarget {
            get => _outlineTarget;
            set {
                if(value == outlineTarget) return;
                _outlineTarget = m_outlineTarget = value;

            }

        }


        public Controller() : base() {
            As<IC>().data = null;

        }

        public override void Awake() {
            base.Awake();

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            As<IC>().data = m_gameObjectData;
            outlineTarget = m_outlineTarget;
            As<IC>().data?.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => As<IC>().data = new Data(textAsset);

        
        public override void OnCollisionEnter(Collision other) {
            if(As<IC>().data.canCollide) {
                OnCollide(other.collider);
                if(As<IC>().data.oneTimeCollision) As<IC>().data.canCollide = false;

            }

        }

        public override void OnTriggerEnter(Collider other) {
            if(As<IC>().data.canCollide) {
                OnCollide(other);
                if(As<IC>().data.oneTimeCollision) As<IC>().data.canCollide = false;

            }


        }

        public override void Update() {
            base.Update();
            if(outlineTarget) {
                if(!outlineTarget.TryGetComponent<Outline_>(out var c)) {
                    c = outlineTarget.AddComponent<Outline_>();

                }

                c.OutlineColor = As<IC>().data.outline.color.asColor;
                c.OutlineMode = As<IC>().data.outline.mode;
                c.OutlineWidth = As<IC>().data.outline.width;

            }

        }

        public virtual void OnCollide(Collider collider) {
            

        }

        public virtual void RegenerateOutline() {
            if(!outlineTarget) return;

            var oldOutline = outlineTarget.GetComponent<Outline_>();
            if(!oldOutline) return;

            Destroy(oldOutline);
            var newOutline = oldOutline.gameObject.AddComponent<Outline_>();

            newOutline.OutlineColor = oldOutline.OutlineColor;
            newOutline.OutlineMode = oldOutline.OutlineMode;
            newOutline.OutlineWidth = oldOutline.OutlineWidth;
            newOutline.enabled = false;

        }

    }

}