/*
 * Date Created: Wednesday, August 25, 2021 12:14 PM
 * Author: NI.L.A
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

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay {
    public class Controller : MonoBehaviour {
        protected static readonly List<Controller> _instances = new List<Controller>();

        protected Dictionary<string, List<Coroutine>> _coroutines = new Dictionary<string, List<Coroutine>>();
        [SerializeField]
        protected float _deltaTime = 1.0f;
        public virtual float deltaTime {
            get => Time.deltaTime * _deltaTime;
            set => _deltaTime = value;

        }
        [SerializeField]
        protected float _fixedDeltaTime = 1.0f;
        public virtual float fixedDeltaTime {
            get => Time.fixedDeltaTime * _fixedDeltaTime;
            set => _fixedDeltaTime = value;

        }
        public virtual new Rigidbody rigidbody => GetComponent<Rigidbody>();

        public Controller() : base() {

        }

        public virtual void Awake() {
            _instances.AddUnique(this);

        }

        public virtual void Start() {

        }

        public virtual void Update() {

        }

        public virtual void FixedUpdate() {

        }

        public virtual void OnEnable() {
            
        }

        public virtual void OnDisable() {
            
        }

        public virtual void OnDestroy() {
            _instances.RemoveAll(this);
            
        }

        public virtual void OnTriggerEnter(Collider collider) {
            

        }

        public virtual void OnTriggerExit(Collider collider) {


        }

        public virtual void OnTriggerStay(Collider collider) {


        }

        public virtual void OnCollisionEnter(Collision collision) {
            

        }

        public virtual void OnCollisionExit(Collision collision) {


        }

        public virtual void OnCollisionStay(Collision collision) {


        }

        public virtual Coroutine DoFor(
            float duration,
            Func<float, float, float, float> onRun,
            Action onStart=null,
            Action onStop=null,
            bool fixedUpdate=false,
            string id=null
        ) {
            var cor = this.DoFor(
                duration, onRun, onStart, onStop, fixedUpdate,
                () => deltaTime, () => fixedDeltaTime
            );

            if(id != null) {
                if(!_coroutines.ContainsKey(id)) _coroutines.Add(id, new List<Coroutine>());
                _coroutines[id].Add(cor);

            }

            return cor;

        }

        public virtual Coroutine DoAfter(
            float duration,
            Action onRun,
            Action onStart=null,
            bool fixedUpdate=false,
            string id=null
        ) {
            var cor = this.DoAfter(
                duration, onRun, onStart, fixedUpdate,
                () => deltaTime, () => fixedDeltaTime
            );

            if(id != null) {
                if(!_coroutines.ContainsKey(id)) _coroutines.Add(id, new List<Coroutine>());
                _coroutines[id].Add(cor);

            }

            return cor;

        }

        public virtual Coroutine StartCoroutine(IEnumerator coroutine, string id) {
            var cor = StartCoroutine(coroutine);
            if(!_coroutines.ContainsKey(id)) _coroutines.Add(id, new List<Coroutine>());
            _coroutines[id].Add(cor);
            return cor;

        }

        public virtual void StopCoroutineWithId(string id) {
            if(_coroutines.Keys.Contains(id)) {
                foreach(var cor in _coroutines[id]) StopCoroutine(cor);
                _coroutines[id].Clear();

            }

        }

        public static IEnumerable<object> GetInstances(Type type) {
            return _instances.Where((i) => i.GetType().IsSubclassOf(type));

        }

        public static object GetInstance(Type type) {
            return GetInstances(type).First();

        }

        public static IEnumerable<T> GetInstances<T>() where T : Controller {
            return _instances.Where((i) => i.GetType().IsSubclassOf(typeof(T)))
                .Cast<T>();

        }

        public static T GetInstance<T>() where T : Controller {
            return GetInstances<T>().First();

        }

        public static void DoWithInstances<T>(Action<T> callback) where T : Controller {
            var instances = GetInstances<T>();
            foreach(var instance in instances) callback(instance as T);

        }

        public static void SetDeltaTime<T>(float time) where T : Controller {
            DoWithInstances<T>((i) => i.deltaTime = time);

        }

        public static void SetFixedDeltaTime<T>(float time) where T : Controller {
            DoWithInstances<T>((i) => i.fixedDeltaTime = time);

        }

        public static void print(params object[] args) {
            MonoBehaviour.print(args.Join(", "));

        }

    }

}