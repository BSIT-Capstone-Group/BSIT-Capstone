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
using UnityEngine.InputSystem;
using UnityEngine.UI;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay {
    public class Controller : MonoBehaviour {
        protected static readonly List<Controller> _instances = new List<Controller>();

        protected Dictionary<string, List<Coroutine>> _coroutines = new Dictionary<string, List<Coroutine>>();
        [SerializeField]
        protected float _timeScale = 1.0f;
        public virtual float timeScale {
            get => Time.timeScale * _timeScale;
            set => _timeScale = value;

        }
        public virtual new Rigidbody rigidbody => GetComponent<Rigidbody>();
        protected bool _isBoundsVisible = false;
        protected List<Action> _updateCallbacks = new List<Action>();
        protected List<Action> _fixedUpdateCallbacks = new List<Action>();

        public Controller() : base() {

        }

        public virtual void Awake() {
            _instances.Add(this);
            _isBoundsVisible = false;

        }

        public virtual void Start() {

        }

        public virtual void Update() {
            OnInput(IInput.keyboard);
            OnInput(IInput.mouse);
            OnInput(IInput.touchscreen);

            var cam = UnityEngine.Camera.main;
            bool visible = gameObject.IsBoundsVisible();
            if(visible) {
                if(!_isBoundsVisible) {
                    _isBoundsVisible = true;
                    OnBoundsVisible();

                }

            } else {
                if(_isBoundsVisible) {
                    _isBoundsVisible = false;
                    OnBoundsInvisible();
                    
                }

            }

            foreach(var c in _updateCallbacks) c();

        }

        public virtual void FixedUpdate() {
            foreach(var c in _fixedUpdateCallbacks) c();

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

        public virtual void OnBecameVisible() {
            
        }

        public virtual void OnBecameInvisible() {

        }

        public virtual void OnBoundsVisible() {

        }

        public virtual void OnBoundsInvisible() {

        }

        public virtual void OnInput(Keyboard keyboard) {
            
        }

        public virtual void OnInput(Mouse mouse) {

        }

        public virtual void OnInput(Touchscreen touchscreen) {

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
                () => Time.deltaTime, () => Time.fixedDeltaTime
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
                () => Time.deltaTime, () => Time.fixedDeltaTime
            );

            if(id != null) {
                if(!_coroutines.ContainsKey(id)) _coroutines.Add(id, new List<Coroutine>());
                _coroutines[id].Add(cor);

            }

            return cor;

        }

        public virtual Coroutine StartCoroutine(IEnumerator routine, string id, bool unique=false) {
            if(unique && HasCoroutine(id)) return null;
            var cor = StartCoroutine(routine);
            if(!HasCoroutine(id)) _coroutines.Add(id, new List<Coroutine>());
            _coroutines[id].Add(cor);
            return cor;

        }

        public virtual Coroutine ToggleCoroutine(IEnumerator routine, string id) {
            Coroutine cor = null;
            if(!HasCoroutine(id)) cor = StartCoroutine(routine, id);
            else StopCoroutineWithId(id);
            return cor;

        }
        public virtual IEnumerator MakeCoroutine(string id, IEnumerator routine) {
            var e = routine;

            while(e.MoveNext()) yield return e.Current;

            yield return new WaitForEndOfFrame();

            StopCoroutineWithId(id);

        }

        public virtual void StopCoroutineWithId(string id) {
            if(HasCoroutine(id)) {
                foreach(var cor in _coroutines.Pop(id)) StopCoroutine(cor);

            }

        }

        public virtual bool HasCoroutine(string id) => _coroutines.ContainsKey(id);


        public static IEnumerable<object> GetInstances(Type type) {
            return _instances.Where((i) => i.GetType().IsSubclassOf(type));

        }

        public static object GetInstance(Type type) {
            return GetInstances(type).First();

        }

        public static IEnumerable<T> GetInstances<T>() where T : Controller {
            return _instances.Where((i) => i.IsInstance(typeof(T)))
                .Cast<T>();

        }

        public static T GetInstance<T>() where T : Controller {
            return GetInstances<T>().First();

        }

        public static void DoWithInstances<T>(Action<T> callback) where T : Controller {
            var instances = GetInstances<T>();
            foreach(var instance in instances) callback(instance);

        }

        public static void SetTimeScale<T>(float time) where T : Controller {
            DoWithInstances<T>((i) => i.timeScale = time);

        }

        public static void print(params object[] args) {
            MonoBehaviour.print(args.Join(", "));

        }

    }

}