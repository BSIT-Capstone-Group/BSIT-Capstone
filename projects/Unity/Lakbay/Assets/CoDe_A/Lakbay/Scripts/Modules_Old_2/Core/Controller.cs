/*
 * Date Created: Sunday, July 4, 2021 11:44 AM
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
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld2.Core {
    using Event = Utilities.Event;

    public interface IInterface {}

    public interface IController : IInterface {
        string controllerName { get; }
        TextAsset dataTextAsset { get; set; }
        bool inspectorHasUpdate { get; set; }

        void Log(string message);
        
        void OnCollide(Collider collider);
        void OnInspectorHasUpdate();
        void Localize();

        void OnEnable();
        void OnDisable();
        void Awake();
        void Start();
        void OnValidate();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void OnCollisionEnter(Collision collision);
        void OnTriggerEnter(Collider collider);


        void OnDataTextAssetChange(TextAsset old, TextAsset @new);
        void OnLabelChange(string old, string @new);
        void OnDescriptionChange(string old, string @new);

        T0 Instantiate<T0>(GameObject parent);
        T0 Instantiate<T0>(Transform parent);
        T0 Instantiate<T0>();
        
        Event.OnValueChange<IController, TextAsset> onDataTextAssetChange { get; }

    }

    public interface IController<T> : IController
        where T : IData {
        T data { get; set; }

        void OnDataChange(T old, T @new);

        Event.OnValueChange<IController<T>, T> onDataChange { get; }

    }

    public class Controller<T> : MonoBehaviour, IController<T>
        where T : class, IData, new() {
        public const string BoxGroupName = "Core.Controller";
        

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
        protected TextAsset m_dataTextAsset;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected TextAsset _dataTextAsset;
        public virtual TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(value == dataTextAsset) return;
                var o = dataTextAsset; var n = value;
                // _dataTextAsset = m_dataTextAsset = value;
                _dataTextAsset = value;
                if(dataTextAsset && data != null) data.Load(dataTextAsset);
                // if(n == null) data.Load(new TextAsset("label:"));
                onDataTextAssetChange.Invoke(this, o, n);
                OnDataTextAssetChange(o, n);

            }

        }
        protected T m_data;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T _data;
        public virtual T data {
            get => _data;
            set {
                // value ??= new T();
                if(value == data) return;
                var o = data; var n = value;
                // _data = m_data = value;
                _data = value;
                onDataChange.Invoke(this, o, n);
                OnDataChange(o, n);

            }

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event.OnValueChange<IController<T>, T> _onDataChange = new Event.OnValueChange<IController<T>, T>();
        public virtual Event.OnValueChange<IController<T>, T> onDataChange => _onDataChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Event.OnValueChange<IController, TextAsset> _onDataTextAssetChange = new Event.OnValueChange<IController, TextAsset>();
        public virtual Event.OnValueChange<IController, TextAsset> onDataTextAssetChange => _onDataTextAssetChange;


        public Controller() : base() {
            _controllerName = Helper.GetName(this, 3);
            data = new T();

        }

        public virtual void Log(string message) => print(message);

        public virtual void OnInspectorHasUpdate() {
            // dataTextAsset = m_dataTextAsset;
            // data = m_data;
            // data?.OnInspectorHasUpdate();

            if(m_dataTextAsset != dataTextAsset) {
                var o = m_dataTextAsset; var n = dataTextAsset;
                if(dataTextAsset && data != null) data.Load(dataTextAsset);
                onDataTextAssetChange.Invoke(this, o, n);
                OnDataTextAssetChange(o, n);
                m_dataTextAsset = dataTextAsset;

            }
            if(m_data != data) {
                var o = m_data; var n = data;
                onDataChange.Invoke(this, o, n);
                OnDataChange(o, n);
                m_data = data;

            }
            data?.OnInspectorHasUpdate();

        }


        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = gameObject.GetComponent<LocalizedTextAssetEvent>(true);
            Helper.AddPersistentListener(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

        }

        public virtual void OnCollide(Collider collider) {
            

        }

        public virtual void OnEnable() {
            // inspectorHasUpdate = true;
            

        }

        public virtual void OnDisable() {
            

        }

        public virtual void Awake() {
            

        }

        public virtual void Start() {
            // inspectorHasUpdate = true;

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

        public virtual void OnCollisionEnter(Collision collision) {
            OnCollide(collision.collider);

        }

        public virtual void OnTriggerEnter(Collider collider) {
            OnCollide(collider);

        }

        public virtual void OnLabelChange(string old, string @new) {
            

        }

        public virtual void OnDescriptionChange(string old, string @new) {
            

        }

        public void OnDataChange(T old, T @new) {
            

        }

        public void OnDataTextAssetChange(TextAsset old, TextAsset @new) {
            

        }

        public T0 Instantiate<T0>(GameObject parent) {
            var go = gameObject.Instantiate(parent);
            // var go = Instantiate(this, parent.transform);
            var c = go.GetComponent<T0>();
            try {
                (c as IController).inspectorHasUpdate = true;
            } catch {}
            return c;

        }

        public T0 Instantiate<T0>(Transform parent) => Instantiate<T0>(parent.gameObject);

        public T0 Instantiate<T0>() => Instantiate<T0>(default(GameObject));

    }

}