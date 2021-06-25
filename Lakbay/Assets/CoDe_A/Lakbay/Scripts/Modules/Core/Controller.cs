using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using CoDe_A.Lakbay.Utilities;
using System.Linq;
using System;

namespace CoDe_A.Lakbay.Modules.Core {
    public interface IController {
        string controllerName { get; }
        bool needsUpdate { get; }

        void Localize();
        void OnNeedsUpdate();
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
        public string controllerName => _controllerName;
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Controller")]
        protected bool _needsUpdate = true;
        public bool needsUpdate => _needsUpdate;

        public Controller() : base() => _controllerName = Helper.GetName(this, 3);

        [ContextMenu("Localize")]
        public virtual void Localize() {}

        public virtual void OnNeedsUpdate() {}

        public virtual void ReceiveInput() {}

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
