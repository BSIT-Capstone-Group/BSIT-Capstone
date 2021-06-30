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

using Ph.Com.CoDe_A.Lakbay.Utilities;
using UnityEngine.Events;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Core {
    public interface IController {

        Data data { get; set; }
        Highlight highlight { get; set; }
        bool highlighted { get; set; }
        bool canBeCollided { get; set; }
        bool oneTimeCollision { get; set; }
        bool canReceiveInput { get; set; }
        List<Game.UI.Content.Data> tutorialContents { get; set; }

        void SetData(TextAsset data);

        void Localize();
        void OnHasUpdate();
        void OnCollide(Collider other);
        void ReceiveInput();

        void RegenerateHighlight();
        void Focus();
        void Unfocus();

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
        public string controllerName => _controllerName;

        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Controller")]
        protected bool _hasUpdate;
        public virtual bool hasUpdate {
            get => _hasUpdate;
            set {
                _hasUpdate = value;

                if(hasUpdate) {
                    OnHasUpdate();
                    hasUpdate = false;
                    
                }

            }
            
        }

        [SerializeField, Label("Data Text Asset"), BoxGroup("Controller")]
        private TextAsset __dataTextAsset;
        protected TextAsset _dataTextAsset;
        public TextAsset dataTextAsset {
            get => _dataTextAsset;
            set {
                if(value == dataTextAsset) return;
                _dataTextAsset = value;
                __dataTextAsset = value;

            }

        }

        [SerializeField, Label("Tutorial Contents"), BoxGroup("Controller")]
        private List<Game.UI.Content.Data> __tutorialContents;
        protected List<Game.UI.Content.Data> _tutorialContents;
        public List<Game.UI.Content.Data> tutorialContents {
            get => _tutorialContents;
            set {
                if(value == tutorialContents) return;
                _tutorialContents = value;
                __tutorialContents = value;

            }

        }

        public virtual Data data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                highlighted = value.highlighted;

            }

        }

        [SerializeField, Label("Highlight"), BoxGroup("Controller")]
        private Highlight __highlight;
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

        [SerializeField, Label("Highlighted"), BoxGroup("Controller")]
        private bool __highlighted;
        public bool highlighted {
            get {
                return highlight ? highlight.showing : false;

            }
            set {
                if(!highlight || value == highlighted) return;
                highlight.showing = value;

            }

        }
        
        [SerializeField, Label("Can Be Collided"), BoxGroup("Controller")]
        private bool __canBeCollided = true;
        protected bool _canBeCollided = true;
        public bool canBeCollided {
            get => _canBeCollided;
            set {
                if(value == canBeCollided) return;
                _canBeCollided = value;
                __canBeCollided = value;

            }
            
        }

        [SerializeField, Label("One Time Collision"), BoxGroup("Controller")]
        private bool __oneTimeCollision = false;
        protected bool _oneTimeCollision = false;
        public bool oneTimeCollision {
            get => _oneTimeCollision;
            set {
                if(value == oneTimeCollision) return;
                _oneTimeCollision = value;
                __oneTimeCollision = value;

            }
            
        }

        [SerializeField, BoxGroup("Controller")]
        protected bool _canReceiveInput = true;
        public virtual bool canReceiveInput {
            get => _canReceiveInput;
            set => _canReceiveInput = value;
            
        }

        
        public Controller() : base() {
            _controllerName = Helper.GetName(this, 3);
            
        }

        [ContextMenu("Localize")]
        public virtual void Localize() {
            var c = GetComponent<LocalizedTextAssetEvent>();
            if(!c) {
                c = gameObject.AddComponent<LocalizedTextAssetEvent>();
            }
            
            Utilities.Helper.AddPersistentListener<TextAsset>(
                c.OnUpdateAsset, this, "dataTextAsset"
            );

        }

        public virtual void OnHasUpdate() {
            dataTextAsset = __dataTextAsset;
            tutorialContents = __tutorialContents;

            highlight = __highlight;
            highlighted = __highlighted;

            canBeCollided = __canBeCollided;
            oneTimeCollision = __oneTimeCollision;

        }

        public virtual void ReceiveInput() {


        }

        public virtual void RegenerateHighlight() {
            try {
                var oldOutline = highlight.outline;
                var oldOutlineParent = oldOutline.transform.parent;

                Destroy(oldOutline);
                var newOutline = oldOutline.gameObject.AddComponent<Utilities.Outline>();

                newOutline.OutlineColor = oldOutline.OutlineColor;
                newOutline.OutlineMode = oldOutline.OutlineMode;
                newOutline.OutlineWidth = oldOutline.OutlineWidth;
                newOutline.enabled = false;
                highlight.outline = newOutline;

            } catch {}

        }

        public virtual void Focus() {
            highlighted = true;

        }

        public virtual void Unfocus() {
            highlighted = false;

        }
        

        public virtual void SetData(TextAsset data) => this.data = Helper.Parse<Data>(data);


        public virtual void OnEnable() {
            hasUpdate = true;

        }

        public virtual void Awake() {
            hasUpdate = true;
            data = new Data();
            
        }

        public virtual void Start() {
            hasUpdate = true;
            
        }

        public virtual void OnValidate() {
            hasUpdate = true;
            
        }

        public virtual void Update() {
            // if(_hasUpdate) {
            //     OnNeedsUpdate();
            //     _hasUpdate = false;

            // }

            if(canReceiveInput) ReceiveInput();

        }

        public virtual void FixedUpdate() {

            
        }

        public virtual void OnCollisionEnter(Collision other) {
            if(canBeCollided) {
                OnCollide(other.collider);
                if(oneTimeCollision) canBeCollided = false;

            }

        }

        public virtual void OnTriggerEnter(Collider other) {
            if(canBeCollided) {
                OnCollide(other);
                if(oneTimeCollision) canBeCollided = false;

            }
            
        }

        public virtual void OnCollide(Collider other) {
            

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

        public static void FocusAll() {
            Focus<IController>();

        }

        public static void UnfocusAll() {
            Unfocus<IController>();

        }

    }

}