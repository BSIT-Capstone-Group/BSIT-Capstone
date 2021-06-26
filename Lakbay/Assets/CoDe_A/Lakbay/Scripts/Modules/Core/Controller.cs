/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
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

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Core {
    public interface IController {
        bool highlighted { get; set; }

        void Localize();
        void OnNeedsUpdate();
        void SetData(Data data);
        void SetData<T>(TextAsset textAsset) where T : Data;
        void SetData(TextAsset textAsset);
        T GetData<T>() where T : Data;
        void OnEnable();
        void Awake();
        void Start();
        void OnValidate();
        void Update();
        void FixedUpdate();
        void OnCollisionEnter(Collision other);
        void OnTriggerEnter(Collider other);

    }

    /// <summary>The base class for anything attachable to a <see cref="GameObject"/>.</summary>
    public abstract class Controller : MonoBehaviour, IController {
        [SerializeField, NaughtyAttributes.ReadOnly, Label("Name"), BoxGroup("Controller")]
        protected string _controllerName = "Controller";
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Controller")]
        protected bool _needsUpdate = true;
        [SerializeField, Label("Data Text Asset"), BoxGroup("Controller")]
        private TextAsset __dataTextAsset;
        [SerializeField, Label("Highlighted"), BoxGroup("Controller")]
        private bool __highlighted = false;

        public bool highlighted {
            get {
                Outline outline = GetComponent<Outline>();
                return outline ? outline.enabled : false;

            }
            set {
                if(value == highlighted) return;
                Outline outline = GetComponent<Outline>();
                outline.enabled = value;

            }

        }


        public string controllerName => _controllerName;
        public bool needsUpdate => _needsUpdate;
        protected TextAsset _dataTextAsset;
        public TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(value == _dataTextAsset) return;
                _dataTextAsset = value;
                __dataTextAsset = value;

            }

        }
        
        public Controller() : base() => _controllerName = Helper.GetName(this, 3);

        [ContextMenu("Localize")]
        public virtual void Localize() {}

        public virtual void OnNeedsUpdate() {
            dataTextAsset = __dataTextAsset;

            highlighted = __highlighted;

        }

        public virtual void SetData(Data data) {}

        public virtual void SetData<T>(TextAsset textAsset) where T : Data {
            SetData(Helper.Parse<T>(textAsset));

        }

        public virtual void SetData(TextAsset textAsset) {
            SetData(Helper.Parse<Data>(textAsset));

        }
        
        public virtual T GetData<T>() where T : Data { return default; }

        public virtual void OnEnable() {
            
        }

        public virtual void Awake() {
            
        }

        public virtual void Start() {
            _needsUpdate = true;
            
        }

        public virtual void OnValidate() {
            _needsUpdate = true;
            
        }

        public virtual void Update() {
            if(_needsUpdate) {
                OnNeedsUpdate();
                _needsUpdate = false;

            }

        }

        public virtual void FixedUpdate() {
            
        }

        public virtual void OnCollisionEnter(Collision other) {
            
        }

        public virtual void OnTriggerEnter(Collider other) {
            
        }

    }

}