/*
 * Date Created: Tuesday, July 20, 2021 7:26 PM
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


    public interface IController {
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
        
    }

    public class Controller : MonoBehaviour, IController {
        public Controller() : base() {}
        
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

        public virtual void Reset() { base.SendMessage("Reset"); }

        public virtual void Log(string message) {
            var l = $"[{Helper.GetName(this, 3)}]".PadRight(25, ' ');
            Debug.Log($"{l}: {message}");

        }

        public virtual void print(params object[] objs) => print(objs, ", ");

        public virtual void print(object[] objs, string separator) {
            var strs = objs.Select<object, string>((o) => o.ToString());
            Log(string.Join(separator, strs));

        }

    }

}