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

namespace CoDe_A.Lakbay.ModulesOld3.Core {
    using Event = Utilities.Event;
    using Collider_ = UnityEngine.Collider;

    public interface IController : IInterface, IPropertyEvent {
        void Log(string message);
        
        void OnCollide(Collider_ collider);
        void OnInspectorHasUpdate();

        void Play();
        void Pause();
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
            set => Helper.SetInvoke(this, ref _dataTextAsset, value, onDataTextAssetChange, OnDataTextAssetChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, ReadOnly] protected bool _playing;
        public virtual bool playing {
            get => _playing;
            set => Helper.SetInvoke(this, ref _playing, value, onPlayingChange, OnPlayingChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, TextAsset> _onDataTextAssetChange = new Event.OnValueChange<IEvent, TextAsset>();
        public virtual Event.OnValueChange<IEvent, TextAsset> onDataTextAssetChange => _onDataTextAssetChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnBoolChange<IEvent> _onPlayingChange = new Event.OnBoolChange<IEvent>();
        public virtual Event.OnBoolChange<IEvent> onPlayingChange => _onPlayingChange;

        
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

        [ContextMenu("Play")]
        public virtual void Play() => playing = true;

        [ContextMenu("Pause")]
        public virtual void Pause() {
            playing = false;
            
        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = gameObject.GetComponent<LocalizedTextAssetEvent>(true);
            Helper.AddPersistentListener(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

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

        public virtual void OnPlayingChange(bool old, bool @new) {
            

        }

        public virtual void OnDataTextAssetChange(TextAsset old, TextAsset @new) {


        }

    }

}