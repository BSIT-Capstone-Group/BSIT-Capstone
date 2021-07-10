using System.Diagnostics;
/*
 * Date Created: Friday, July 2, 2021 2:18 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld.Core.Base {
    [Serializable]
    public abstract class Event<T0, T1> 
        where T0 : Controller
        where T1 : Data {
        [Serializable]
        public class OnDataChange : UnityEvent<T0, T1, T1> {}

    }

    public interface IController : Raw.IController, ISerializable {
        string controllerName { get; }
        bool inspectorHasUpdate { get; set; }
        TextAsset dataTextAsset { get; set; }
        Data data { get; set; }
        Event<Controller, Data>.OnDataChange onDataChange { get; }

        void OnDataChange(Data oldValue, Data newValue);

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

    public class Controller : Raw.Controller, IController {
        public const string BoxGroupName = "Base.Controller";
        
        [BoxGroup(BoxGroupName)]
        [SerializeField, ReadOnly]
        protected string _controllerName;
        public virtual string controllerName => _controllerName;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ReadOnly]
        protected bool _inspectorHasUpdate;
        public virtual bool inspectorHasUpdate {
            get => _inspectorHasUpdate;
            set {
                if(value == inspectorHasUpdate) return;
                _inspectorHasUpdate = value;
                if(inspectorHasUpdate) {
                    OnInspectorHasUpdate();
                    inspectorHasUpdate = false;
                    
                }

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected TextAsset m_dataTextAsset;
        protected TextAsset _dataTextAsset;
        public virtual TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(value == dataTextAsset) return;
                _dataTextAsset = m_dataTextAsset = value;

            }

        }
        public virtual Data data {
            get => new Data(this);
            set {
                value ??= new Data();
                onDataChange.Invoke(this, data, data);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event<Controller, Data>.OnDataChange _onDataChange = new Event<Controller, Data>.OnDataChange();
        public virtual Event<Controller, Data>.OnDataChange onDataChange => _onDataChange;


        public Controller() : base() {
            var self = this as IController;
            _controllerName = Helper.GetName(this, 3);
            _inspectorHasUpdate = false;
            self.data = null;

        }

        public virtual void OnDataChange(Data oldValue, Data newValue) {


        }

        public virtual void SetData(TextAsset textAsset) {
            var self = this as IController;
            self.data = new Data(textAsset);

        }

        [ContextMenu("Set Data")]
        public virtual void SetData() {
            SetData(dataTextAsset ?? new TextAsset("label:"));
            // Debug.Log(data.AsYaml());
            // SetData(new TextAsset(data.AsYaml()));
            
        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = gameObject.GetComponent<LocalizedTextAssetEvent>(true);
            Helper.AddPersistentListener(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

        }

        public virtual void OnInspectorHasUpdate() {
            var self = this as IController;
            dataTextAsset = m_dataTextAsset;

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

        public void Log(string message) => Controller.Log(message);

        public static void Log(params object[] objs) => UnityEngine.Debug.Log(string.Join(", ", objs));

    }

}