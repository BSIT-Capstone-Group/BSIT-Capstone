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
using UnityEngine.SceneManagement;

using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Core {
    public interface IController {
        Highlight highlight { get; set; }
        bool highlighted { get; set; }

        void Localize();
        void OnNeedsUpdate();
        // void Focus<T>() where T : IController;
        // void Unfocus<T>() where T : IController;
        void Focus();
        void Unfocus();
        void SetData(IData data);
        void SetData<T>(TextAsset textAsset) where T : IData;
        void SetData(TextAsset textAsset);
        T GetData<T>() where T : IData;
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
    public class Controller : MonoBehaviour, IController {
        [SerializeField, NaughtyAttributes.ReadOnly, Label("Name"), BoxGroup("Controller")]
        protected string _controllerName = "Controller";
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Controller")]
        protected bool _needsUpdate = true;
        [SerializeField, Label("Data Text Asset"), BoxGroup("Controller")]
        private TextAsset __dataTextAsset;
        [SerializeField, Label("Highlight"), BoxGroup("Controller")]
        private Highlight __highlight;
        [SerializeField, Label("Highlighted"), BoxGroup("Controller")]
        private bool __highlighted = false;


        public Highlight highlight {
            get {
                if(__highlight) return __highlight;
                Highlight highlight = GetComponent<Highlight>();
                return highlight;

            }
            set {
                if(value == this.highlight) return;
                __highlight = value;

            }

        }

        public bool highlighted {
            get {
                return highlight ? highlight.showing : false;

            }
            set {
                if(!highlight || value == highlighted) return;
                highlight.showing = value;

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

            highlight = __highlight;
            highlighted = __highlighted;

        }

        public virtual void Focus() {
            highlighted = true;

        }

        public virtual void Unfocus() {
            highlighted = false;

        }

        public static void Focus<T>(T[] controllers, T[] excludedControllers) where T : IController {
            foreach(var c in controllers) {
                if(excludedControllers.Contains(c)) continue;
                c?.Focus();

            }

        }

        public static void Unfocus<T>(T[] controllers, T[] excludedControllers) where T : IController {
            foreach(var c in controllers) {
                if(excludedControllers.Contains(c)) continue;
                c?.Unfocus();

            }

        }

        public static void Focus<T>(params T[] excludedControllers) where T : IController {
            var s = SceneManager.GetActiveScene();
            foreach(var r in s.GetRootGameObjects()) {
                var cs = r.GetComponentsInChildren<T>();
                var mcs = (from c in cs where excludedControllers.Contains(c) select c).ToArray();
                Focus<T>(cs, mcs);

            }

        }

        public static void Unfocus<T>(params T[] excludedControllers) where T : IController {
            var s = SceneManager.GetActiveScene();
            foreach(var r in s.GetRootGameObjects()) {
                var cs = r.GetComponentsInChildren<T>();
                var mcs = (from c in cs where excludedControllers.Contains(c) select c).ToArray();
                Unfocus<T>(cs, mcs);

            }

        }
        public static void Focus<T>() where T : IController {
            Focus<T>(new T[] {});

        }

        public static void Unfocus<T>() where T : IController {
            Unfocus<T>(new T[] {});

        }

        public virtual void SetData(IData data) {}

        public virtual void SetData<T>(TextAsset textAsset) where T : IData {
            SetData(Helper.Parse<T>(textAsset));

        }

        public virtual void SetData(TextAsset textAsset) {
            SetData(Helper.Parse<Data>(textAsset));

        }
        
        public virtual T GetData<T>() where T : IData { return default; }

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