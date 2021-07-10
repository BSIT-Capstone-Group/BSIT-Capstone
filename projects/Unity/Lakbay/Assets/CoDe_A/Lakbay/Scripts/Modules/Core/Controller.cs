/*
 * Date Created: Wednesday, July 7, 2021 5:24 AM
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

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;
    using Collider_ = UnityEngine.Collider;

    public interface IController : IInterface, IPropertyEvent {
        string controllerName { get; }
        TextAsset dataTextAsset { get; set; }
        bool paused { get; set; }

        void Log(string message);
        
        void OnCollide(Collider_ collider);
        void OnInspectorHasUpdate();
        void Localize();
        void SetData();

        void OnEnable();
        void OnDisable();
        void Awake();
        void Start();
        void OnValidate();
        void Update();
        void FixedUpdate();
        void LateUpdate();
        void OnCollisionEnter(Collision collision);
        void OnTriggerEnter(Collider_ collider);

        Event.OnBoolChange<IController> onPausedChange { get; }
        void OnPausedChange(bool old, bool @new);

    }

    public class Controller : MonoBehaviour, IController {
        public const string BoxGroupName = "Core.Controller";

        [BoxGroup(BoxGroupName)]
        [SerializeField, ReadOnly] protected string _controllerName;
        public virtual string controllerName => _controllerName;
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected TextAsset _dataTextAsset;
        public virtual TextAsset dataTextAsset {
            get => _dataTextAsset;
            set => Helper.SetInvoke(this, ref _dataTextAsset, value);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected bool _paused;
        public virtual bool paused {
            get => _paused;
            set => Helper.SetInvoke(this, ref _paused, value, onPausedChange, OnPausedChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnBoolChange<IController> _onPausedChange = new Event.OnBoolChange<IController>();
        public virtual Event.OnBoolChange<IController> onPausedChange => _onPausedChange;

        
        public Controller() : base() { _controllerName = Helper.GetName(this, 3); }

        public virtual void Awake() {
            

        }

        public virtual void FixedUpdate() {
            

        }

        public virtual void LateUpdate() {
            

        }

        // [ContextMenu("Set Data")]
        public virtual void SetData() {
            if(dataTextAsset) Data.Create(dataTextAsset, this);

        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            

        }

        public virtual void Log(string message) => print(message);

        public virtual void OnCollide(Collider_ collider) {
            

        }

        public virtual void OnCollisionEnter(Collision collision) {
            

        }

        public virtual void OnDisable() {
            

        }

        public virtual void OnEnable() {
            

        }

        public virtual void OnInspectorHasUpdate() {
            

        }

        public virtual void OnTriggerEnter(Collider_ collider) {
            

        }

        public virtual void OnValidate() {
            

        }

        public virtual void Start() {
            

        }

        public virtual void Update() {
            

        }

        public virtual void OnPausedChange(bool old, bool @new) {


        }
        
    }

}