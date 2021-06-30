/*
 * Date Created: Tuesday, June 29, 2021 6:48 PM
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

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core {
    public interface IController {
        string controllerName { get; }
        TextAsset dataTextAsset { get; set; }
        Data data { get; set; }
        string label { get; set; }
        string description { get; set; }
        bool inspectorHasUpdate { get; }


        void SetData(TextAsset textAsset);
        void Localize();
        void OnInspectorHasUpdate();

        void OnEnable();
        void OnDisable();
        void Awake();
        void Start();
        void OnValidate();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void OnCollisionEnter(Collision other);
        void OnTriggerEnter(Collider other);

    }

    public class Controller : MonoBehaviour, IController {
        public const string BoxGroupName = "Core.Controller";
        
        [BoxGroup(BoxGroupName)]
        [SerializeField, ReadOnly]
        protected string _controllerName;
        public string controllerName => _controllerName;
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Data Text Asset")]
        private TextAsset __dataTextAsset;
        protected TextAsset _dataTextAsset;
        public virtual TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(value == dataTextAsset) return;
                _dataTextAsset = value;
                __dataTextAsset = value;

            }

        }
        public virtual Data data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                label = value.label;
                description = value.description;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Label")]
        private string __label;
        protected string _label;
        public virtual string label {
            get => _label;
            set {
                if(value == label) return;
                _label = value;
                __label = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Description")]
        private string __description;
        protected string _description;
        public virtual string description {
            get => _description;
            set {
                if(value == description) return;
                _description = value;
                __description = value;

            }

        }
        protected bool _inspectorHasUpdate = false;
        public virtual bool inspectorHasUpdate {
            get => _inspectorHasUpdate;
            set {
                _inspectorHasUpdate = value;

                if(inspectorHasUpdate) {
                    OnInspectorHasUpdate();
                    inspectorHasUpdate = false;

                }

            }
            
        }


        public Controller() : base() {
            _controllerName = Helper.GetName(this, 3);
            data = null;

        }

        public virtual void SetData(TextAsset textAsset) {
            data = new Data(textAsset);

        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = GetComponent<LocalizedTextAssetEvent>();
            if(!c) {
                c = gameObject.AddComponent<LocalizedTextAssetEvent>();
            }
            
            Utilities.Helper.AddPersistentListener(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

        }

        public virtual void OnInspectorHasUpdate() {
            dataTextAsset = __dataTextAsset;
            label = __label;
            description = __description;

        }

        public virtual void OnEnable() {
            

        }

        public virtual void OnDisable() {
            

        }

        public virtual void Awake() {
            

        }

        public virtual void Start() {
            

        }

        public virtual void OnValidate() {
            inspectorHasUpdate = true;

        }

        public virtual void Update() {
            

        }

        public virtual void FixedUpdate() {
            

        }

        public virtual void LateUpdate() {
            

        }

        public virtual void OnCollisionEnter(Collision other) {
            

        }

        public virtual void OnTriggerEnter(Collider other) {
            

        }

        // public override void Awake() {
        //     base.Awake();
        //     (this as IController).data = new Data();

        // }

        // public override void OnNeedsUpdate() {
        //     base.OnNeedsUpdate();

        // }

        // public override void SetData(TextAsset data) => (this as IController).data = Helper.Parse<Data>(data);

    }

}