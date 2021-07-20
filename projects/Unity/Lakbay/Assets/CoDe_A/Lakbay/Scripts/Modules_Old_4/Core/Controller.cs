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

namespace CoDe_A.Lakbay.ModulesOld4.Core {
    using Event = Utilities.Event;
    using Input = Utilities.Input;

    public interface IController {
        string controllerName { get; }
        TextAsset dataTextAsset { get; set; }

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

        void Log(string message);
        void print(params object[] objs);
        void print(object[] objs, string separator);

        T GetData<T>(IController<T> controller) where T : IData;

        // void Play();
        // void Pause();
        // void TogglePlaying();
        // void HandleInputs();

    }

    public class Controller : MonoBehaviour, IController {
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
                // SetDataTextAsset(this, ref _dataTextAsset, value);
                // if(Helper.SetInvoke(this, ref _dataTextAsset, value).Item1) {
                //     if(!dataTextAsset) return;
                //     try {
                //         print(objs: $"Successfully Parsed!\n");
                //         // T.cdata = dataTextAsset.Parse<T>();
                //         data.Set(dataTextAsset);


                //     } catch(Exception e) {
                //         print(objs: $"Failed to Parse!\n" + e);

                //     }

                // }

            }

        }


        public Controller() : base() {
            _controllerName = Helper.GetName(this, 3);

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

        public virtual void Reset() {}

        public virtual void Log(string message) {
            var l = $"[{controllerName}]".PadRight(25, ' ');
            Debug.Log($"{l}: {message}");

        }

        public virtual void print(params object[] objs) => print(objs, ", ");

        public virtual void print(object[] objs, string separator) {
            var strs = objs.Select<object, string>((o) => o.ToString());
            Log(string.Join(separator, strs));

        }

        public virtual T GetData<T>(IController<T> controller) where T : IData {
            return controller.data;

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

    public interface IController<T> : IController
        where T : IData {
        T data { get; set; }

        // void SetDataTextAsset();

    }

    public class Controller<T> : Controller, IController<T>
        where T : IData, new() {
        public override TextAsset dataTextAsset {
            get => base.dataTextAsset;
            set => SetDataTextAsset(this, ref _dataTextAsset, value);
            
        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected T _data;
        public virtual T data {
            get => _data;
            set {
                SetData(this, ref _data, value);
                // var old = data;
                // if(Helper.SetInvoke(this, ref _data, value).Item1) {
                //     var @new = data;
                //     old?.SetController(this);
                //     @new?.SetController(this);

                // }

            }

        }


        public Controller() : base() {
            data = new T();

        }

        [ContextMenu("Reset")]
        public override void Reset() => data = new T();

        // public virtual void SetDataTextAsset() {
        //     var old = dataTextAsset;
        //     dataTextAsset = null;
        //     dataTextAsset = old;

        // }

        public static void SetDataTextAsset(IController<T> controller, ref TextAsset old, TextAsset @new) {
            var self = controller;
            if(Helper.SetInvoke(self, ref old, @new).Item1) {
                if(!self.dataTextAsset) return;
                try {
                    self.print(objs: $"Successfully Parsed!\n");
                    // T.cdata = dataTextAsset.Parse<T>();
                    self.data.Set(self.dataTextAsset);


                } catch(Exception e) {
                    self.print(objs: $"Failed to Parse!\n" + e);

                }

            }

        }

        public static void SetData(Controller controller, ref T old, T @new) {
            var self = controller;
            if(Helper.SetInvoke(self, ref old, @new).Item1) {
                // var @new = data;
                old?.SetController(self);
                @new?.SetController(self);

            }

        }

    }

}