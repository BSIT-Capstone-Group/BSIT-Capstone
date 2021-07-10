/*
 * Date Created: Saturday, July 3, 2021 3:15 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core {
    [Serializable]
    public class OnValueChange<T0, T1> : UnityEvent<T0, T1, T1> {}
    [Serializable]
    public class OnStringValueChange<T> : OnValueChange<T, string> {}
    [Serializable]
    public class OnIntValueChange<T> : OnValueChange<T, int> {}
    [Serializable]
    public class OnFloatValueChange<T> : OnValueChange<T, float> {}
    [Serializable]
    public class OnBoolValueChange<T> : OnValueChange<T, bool> {}

    [Serializable]
    public class OnDataTextAssetChange<T> : OnValueChange<IController<T>, TextAsset>
        where T : class, IData {}
    [Serializable]
    public class OnDataChange<T> : OnValueChange<IController<T>, T>
        where T : class, IData {}

    public interface ISerializable<T> where T : class, IData {
        void SetData(TextAsset textAsset);
        void SetData(T data);
        void SetData();
        T GetData();

    }

    public interface IController<T> : IData, ISerializable<T> where T : class, IData {
        string controllerName { get; }
        bool inspectorHasUpdate { get; set; }
        TextAsset dataTextAsset { get; set; }
        T data { get; set; }
        
        OnDataTextAssetChange<T> onDataTextAssetChange { get; }
        OnDataChange<T> onDataChange { get; }
        void OnDataTextAssetChange(TextAsset old, TextAsset @new);
        void OnDataChange(T old, T @new);

        
        void Log(string message);
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

    public class Controller<T> : MonoBehaviour, IController<T> where T : class, IData {
        public const string BoxGroupName = "Core.Controller";
        public const string EventFoldoutName = "Events";

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
                var o = dataTextAsset; var n = value;
                _dataTextAsset = m_dataTextAsset = value;
                onDataTextAssetChange.Invoke(this, o, n);
                OnDataTextAssetChange(o, n);

            }

        }
        public virtual T data {
            get => default;
            set {
                var o = data; var n = value;
                onDataChange.Invoke(this, o, n);
                OnDataChange(o, n);

            }

        }

        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnDataTextAssetChange<T> _onDataTextAssetChange = new OnDataTextAssetChange<T>();
        public OnDataTextAssetChange<T> onDataTextAssetChange => _onDataTextAssetChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnDataChange<T> _onDataChange = new OnDataChange<T>();
        public OnDataChange<T> onDataChange => _onDataChange;
        

        public Controller() {
            _controllerName = Helper.GetName(this, 3);
            data = default;

        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = gameObject.GetComponent<LocalizedTextAssetEvent>(true);
            Helper.AddPersistentListener(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

        }

        public virtual void OnInspectorHasUpdate() {
            dataTextAsset = m_dataTextAsset;

        }

        public virtual void OnEnable() {
            

        }

        public virtual void OnDisable() {
            

        }

        public virtual void Awake() {
            

        }

        public virtual void Start() {
            inspectorHasUpdate = true;

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

        public virtual void OnDataTextAssetChange(TextAsset old, TextAsset @new) {

            
        }

        public virtual void OnDataChange(T old, T @new) {


        }

        public static void Log(params object[] objs) => UnityEngine.Debug.Log(string.Join(", ", objs));

        public virtual void Log(string message) => Controller<T>.Log(message);

        public virtual void SetData(TextAsset textAsset) => SetData(textAsset.Parse<T>());

        public virtual void SetData(T data) => this.data = data;
        
        public virtual void SetData() => SetData(dataTextAsset);

        public virtual T GetData() => default;

        public T1 Instantiate<T1>(GameObject parent) {
            return gameObject.Instantiate(parent).GetComponent<T1>();

        }

        public T1 Instantiate<T1>() => Instantiate<T1>(null);

    }

}