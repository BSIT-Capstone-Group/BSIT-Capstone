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

using Code_A_Old_2.Lakbay.Utilities;
using IC = Code_A_Old_2.Lakbay.Modules.Core.IController;

namespace Code_A_Old_2.Lakbay.Modules.Core {
    public interface IController {
        string controllerName { get; }
        TextAsset dataTextAsset { get; set; }
        bool showCoreData { get; }
        Data data { get; set; }
        bool inspectorHasUpdate { get; }

        T As<T>() where T: class, IController;

        void SetData(TextAsset textAsset);
        void SetData();
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
        [SerializeField]
        protected TextAsset m_dataTextAsset;
        protected TextAsset _dataTextAsset;
        public virtual TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(value == dataTextAsset) return;
                _dataTextAsset = value;
                m_dataTextAsset = value;

            }

        }
        public virtual bool showCoreData => true;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showCoreData")]
        protected Data m_coreData;
        protected Data _coreData;
        public virtual Data data {
            get => _coreData;
            set {
                value ??= new Data();
                if(value == data) return;
                if(data != null) data.controller = null;
                value.controller = this;
                _coreData = m_coreData = value;

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

        public virtual T As<T>() where T : class, IController  => (this as T);

        public virtual void SetData(TextAsset textAsset) {
            data = new Data(textAsset);

        }

        [ContextMenu("Set Data")]
        public virtual void SetData() {
            SetData(dataTextAsset ?? new TextAsset("label:"));
            Debug.Log(data.AsYaml());
            SetData(new TextAsset(data.AsYaml()));
            
        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = GetComponent<LocalizedTextAssetEvent>();
            if(!c) {
                c = gameObject.AddComponent<LocalizedTextAssetEvent>();
            }

            Helper.AddPersistentListener(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

        }

        public virtual void OnInspectorHasUpdate() {
            data = m_coreData;
            dataTextAsset = m_dataTextAsset;
            data?.OnInspectorHasUpdate();

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