using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using GInput = UnityEngine.Input;
using GInput = SimpleInput;
using NaughtyAttributes;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.Input {
    public interface IController : Core.IController {
        bool canReceiveInput { get; set; }
        void ReceiveInput();

    }

    public class Controller : GInput, IController {
        [SerializeField, NaughtyAttributes.ReadOnly, Label("Name"), BoxGroup("Controller")]
        protected string _controllerName = "Controller";
        public string controllerName => _controllerName;
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Controller")]
        protected bool _needsUpdate = true;
        public bool needsUpdate => _needsUpdate;
        [SerializeField, BoxGroup("Input.Controller")]
        private bool _canReceiveInput = true;
        public bool canReceiveInput {
            get => _canReceiveInput;
            set => _canReceiveInput = value;
            
        }

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
            if(needsUpdate) {
                OnNeedsUpdate();
                _needsUpdate = false;

            }

            if(canReceiveInput) ReceiveInput();

        }

        public virtual void FixedUpdate() {
            
        }

        public virtual void OnCollisionEnter(Collision other) {
            
        }

        public virtual void OnTriggerEnter(Collider other) {
            
        }

    }

}