/*
 * Date Created: Tuesday, July 13, 2021 10:26 AM
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
using UnityEngine.InputSystem;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;
    using Input = Utilities.Input;

    public interface IController {}

    public interface IController<T> : IController
        where T : IData {
        string controllerName { get; }

        TextAsset dataTextAsset { get; set; }
        T data { get; set; }

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
        void OnGUI();

        void Reset();
        void SetDataTextAsset();

        void Log(string message);
        void print(params object[] objs);
        void print(object[] objs, string separator);

        // void Play();
        // void Pause();
        // void TogglePlaying();
        // void HandleInputs();

    }

    public class Controller<T> : MonoBehaviour, IController<T>
        where T : IData, new() {
        public const string BoxGroupName = "Core.Controller";

        [BoxGroup(BoxGroupName)]
        [SerializeField, ReadOnly]
        protected string _controllerName;
        public virtual string controllerName => _controllerName;

        [BoxGroup(BoxGroupName)]
        [ContextMenuItem("Set Data", "SetDataTextAsset")]
        [SerializeField]
        protected TextAsset _dataTextAsset;
        public virtual TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(Helper.SetInvoke(this, ref _dataTextAsset, value).Item1) {
                    if(!dataTextAsset) return;
                    try {
                        print(objs: $"Successfully Parsed!\n");
                        // T.cdata = dataTextAsset.Parse<T>();
                        data.Set(dataTextAsset);


                    } catch(Exception e) {
                        print(objs: $"Failed to Parse!\n" + e);

                    }

                }

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T _data;
        public virtual T data {
            get => _data;
            set {
                var old = data;
                if(Helper.SetInvoke(this, ref _data, value).Item1) {
                    var @new = data;
                    old?.SetController(this);
                    @new?.SetController(this);

                }

            }

        }


        public Controller() : base() {
            _controllerName = Helper.GetName(this, 3);
            data = new T();

        }

        public virtual void Awake() {


        }

        public virtual void FixedUpdate() {


        }

        public virtual void LateUpdate() {


        }

        public virtual void OnCollisionEnter(Collision collision) {


        }

        public virtual void OnDisable() {


        }

        public virtual void OnEnable() {


        }

        public virtual void OnGUI() {


        }

        public virtual void OnTriggerEnter(Collider collider) {


        }

        public virtual void OnValidate() {


        }

        public virtual void Start() {


        }

        public virtual void Update() {
            // if(handlingInputs) HandleInputs();

        }

        public virtual void SetDataTextAsset() {
            var old = dataTextAsset;
            dataTextAsset = null;
            dataTextAsset = old;

        }

        [ContextMenu("Reset")]
        public virtual void Reset() => data = new T();

        public virtual void Log(string message) {
            var l = $"[{controllerName}]".PadRight(25, ' ');
            Debug.Log($"{l}: {message}");

        }

        public virtual void print(params object[] objs) => print(objs, ", ");

        public virtual void print(object[] objs, string separator) {
            var strs = objs.Select<object, string>((o) => o.ToString());
            Log(string.Join(separator, strs));

        }
        
        // public virtual void Play() {
        //     // playing = true;

        // }
        
        // public virtual void Pause() {
        //     // playing = false;

        // }
        
        // public virtual void TogglePlaying() {
        //     // if(playing) Pause();
        //     // else Play();

        // }

        // public virtual void HandleInputs() {
        //     // if(Input.keyboard.spaceKey.wasPressedThisFrame) playing = !playing;

        // }

    }

}