/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using NaughtyAttributes;

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.LinearPlay.Player {
    public enum SlideDirection { Left, None, Right };

    public interface IController : Core.IController {
        bool canReceiveSlideInput { get; set; }
        bool canReceiveTravelInput { get; set; }

        float travelDuration { get; set; }
        float travelSpeed { get; set; }
        float slideDuration { get; set; }
        float slideSpeed { get; set; }
        bool isTraveling { get; set; }
        int laneIndex { get; set; }

        new Data data { get; set; }

    }

    public class Controller : Core.Controller, IController {
        private Coroutine _travelCoroutine;
        private Coroutine _slideCoroutine;

        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;
                canReceiveSlideInput = value.canReceiveSlideInput;
                canReceiveTravelInput = value.canReceiveTravelInput;
                travelDuration = value.travelDuration;
                travelSpeed = value.travelSpeed;
                slideDuration = value.slideDuration;
                slideSpeed = value.slideSpeed;
                isTraveling = value.isTraveling;
                laneIndex = value.laneIndex;

            }

        }

        [BoxGroup("Player.Controller")]
        public Lane.Group.Controller laneGroupController;

        public virtual bool canReceiveSlideInput {
            get => _canReceiveSlideInput;
            set {
                if(value == canReceiveSlideInput) return;
                _canReceiveSlideInput = value;

            }

        }

        public virtual bool canReceiveTravelInput {
            get => _canReceiveTravelInput;
            set {
                if(value == canReceiveTravelInput) return;
                _canReceiveTravelInput = value;

            }

        }


        [BoxGroup("Player.Controller"), Header("Travel Options")]
        public Easing.Ease travelEasing = Easing.Ease.Linear;
        [BoxGroup("Player.Controller")]
        [SerializeField, Label("Travel Duration")]
        private float __travelDuration;
        [BoxGroup("Player.Controller")]
        [SerializeField, Label("Travel Speed")]
        private float __travelSpeed;
        [BoxGroup("Player.Controller")]
        [SerializeField, Label("Is Traveling")]
        private bool __isTraveling;
        [BoxGroup("Player.Controller")]
        [SerializeField]
        protected bool _canReceiveTravelInput;


        [BoxGroup("Player.Controller"), Header("Slide Options")]
        [SerializeField, NaughtyAttributes.ReadOnly]
        protected SlideDirection _slideDirection = SlideDirection.None;
        [BoxGroup("Player.Controller")]
        public Easing.Ease slideEasing = Easing.Ease.Spring;
        [BoxGroup("Player.Controller")]
        [SerializeField, Label("Slide Duration")]
        private float __slideDuration;
        [BoxGroup("Player.Controller")]
        [SerializeField, Label("Slide Speed")]
        private float __slideSpeed;
        [BoxGroup("Player.Controller")]
        [SerializeField, Label("Lane Index")]
        private int __laneIndex;
        [BoxGroup("Player.Controller")]
        [SerializeField]
        protected bool _canReceiveSlideInput;

        
        protected float _travelDuration;
        protected float _travelSpeed;
        protected float _slideDuration;
        protected float _slideSpeed;
        protected bool _isTraveling;
        protected int _laneIndex;
        

        public virtual float travelDuration {
            get => _travelDuration;
            set {
                if(value == travelDuration) return;
                _travelDuration = value;
                __travelDuration = value;

            }

        }
        public virtual float travelSpeed {
            get => _travelSpeed;
            set {
                if(value == travelSpeed) return;
                _travelSpeed = value;
                __travelSpeed = value;

            }

        }
        public virtual bool isTraveling {
            get => _isTraveling;
            set {
                if(value == isTraveling) return;
                _isTraveling = value;
                __isTraveling = value;

                if(_isTraveling) {
                    _travelCoroutine = Travel(travelDuration, travelEasing);

                } else if(_travelCoroutine != null) StopCoroutine(_travelCoroutine);

            }

        }


        public virtual float slideDuration {
            get => _slideDuration;
            set {
                if(value == slideDuration) return;
                _slideDuration = value;
                __slideDuration = value;

            }

        }
        public virtual float slideSpeed {
            get => _slideSpeed;
            set {
                if(value == slideSpeed) return;
                _slideSpeed = value;
                __slideSpeed = value;

            }

        }
        public virtual int laneIndex {
            get => _laneIndex;
            set {
                if(!laneGroupController || laneGroupController.laneControllers.Count == 0) return;
                value = Helper.Limit(value, laneGroupController.laneControllers.Count - 1);
                if(value == laneIndex) return;
                _slideDirection = value < laneIndex ? SlideDirection.Left : (
                    value > laneIndex ? SlideDirection.Right : SlideDirection.None
                );
                _laneIndex = value;
                __laneIndex = value;

                if(_slideDirection != SlideDirection.None) {
                    _slideCoroutine = Slide(slideDuration, slideEasing);

                } else if(_slideCoroutine != null) StopCoroutine(_slideCoroutine);
                

            }

        }


        public virtual Lane.Controller laneController => laneGroupController.laneControllers[laneIndex];
        public virtual Vector3 laneControllerTargetPosition {
            get => GetLaneControllerTargetPosition(laneController);

        }
        public virtual Vector3 laneControllerTargetEndPosition {
            get => GetLaneControllerTargetEndPosition(laneController);

        }


        public virtual SlideDirection slideDirection => _slideDirection;

        // public Controller() : base() { (this as IController).data = new Data(); }

        public override void Awake() {
            base.Awake();
            (this as IController).data = new Data();

        }

        public override void Focus() {
            base.Focus();
            isTraveling = false;
            canReceiveInput = false;

        }

        public override void Unfocus() {
            base.Unfocus();
            isTraveling = true;
            canReceiveInput = true;

        }

        public override void Start() {
            base.Start();

        }

        public override void OnValidate() {
            base.OnValidate();

        }

        public override void OnHasUpdate() {
            base.OnHasUpdate();

            travelDuration = __travelDuration;
            travelSpeed = __travelSpeed;
            isTraveling = __isTraveling;

            slideDuration = __slideDuration;
            slideSpeed = __slideSpeed;
            laneIndex = __laneIndex;

        }

        public override void ReceiveInput() {
            base.ReceiveInput();
            if(canReceiveTravelInput) ReceiveTravelInput();
            if(canReceiveSlideInput) ReceiveSlideInput();

        }

        public virtual void ReceiveSlideInput() {
            // var l = Input.System.GetKeyDown(KeyCode.LeftArrow);
            // var r = Input.System.GetKeyDown(KeyCode.RightArrow);
            var l = Input.System.keyboard.leftArrowKey.wasPressedThisFrame;
            var r = Input.System.keyboard.rightArrowKey.wasPressedThisFrame;
            if(l || r) {
                if(l) laneIndex--;
                else if(r) laneIndex++;

            }

        }

        public virtual void ReceiveTravelInput() {
            // var i = Input.System.GetKeyDown(KeyCode.Space);
            var i = Input.System.keyboard.spaceKey.wasPressedThisFrame;
            if(i) isTraveling = !isTraveling;

        }

        public virtual Vector3 GetLaneControllerTargetPosition(Lane.Controller laneController) {
            var pos = transform.position;
            var lpos = laneController.startPosition;
            return new Vector3(lpos.x, pos.y, pos.z);

        }

        public virtual Vector3 GetLaneControllerTargetEndPosition(Lane.Controller laneController) {
            var pos = transform.position;
            var lpos = laneController.endPosition;
            return new Vector3(pos.x, pos.y, lpos.z);

        }

        public virtual Coroutine Slide(Lane.Controller laneController, float duration, Easing.Ease easing) {
            var pos = transform.position;
            return Helper.DoOver(
                mono: this,
                duration: duration,
                onStart: null,
                onRun: (t, e, d) => {
                    var lpos = new Vector3(pos.x, pos.y, transform.position.z);
                    transform.position = Easing.Use(
                        lpos,
                        GetLaneControllerTargetPosition(laneController),
                        e / d,
                        easing
                    );
                    return t * slideSpeed;

                },
                onStop: () => {
                    transform.position = GetLaneControllerTargetPosition(laneController);
                    _slideDirection = SlideDirection.None;

                },
                true
            );

        }

        public virtual Coroutine Slide(Lane.Controller laneController, float duration) {
            return Slide(laneController, duration, Easing.Ease.Linear);

        }

        public virtual Coroutine Slide(Lane.Controller laneController) => Slide(laneController, 0.001f);

        public virtual Coroutine Slide(float duration, Easing.Ease easing) => Slide(laneController, duration, easing);

        public virtual Coroutine Slide(float duration) => Slide(laneController, duration);

        public virtual Coroutine Travel(Lane.Controller laneController, float duration, Easing.Ease easing) {
            var pos = transform.position;
            return Helper.DoOver(
                mono: this,
                duration: duration,
                onStart: null,
                onRun: (t, e, d) => {
                    var lpos = new Vector3(transform.position.x, pos.y, pos.z);
                    transform.position = Easing.Use(
                        lpos,
                        GetLaneControllerTargetEndPosition(laneController),
                        e / d,
                        easing
                    );
                    return t * travelSpeed;

                },
                onStop: () => {
                    transform.position = GetLaneControllerTargetEndPosition(laneController);
                    _isTraveling = false;

                },
                true
            );

        }
        
        public virtual Coroutine Travel(Lane.Controller laneController, float duration) {
            return Travel(laneController, duration, Easing.Ease.Linear);

        }

        public virtual Coroutine Travel(Lane.Controller laneController) => Travel(laneController, 0.001f);

        public virtual Coroutine Travel(float duration, Easing.Ease easing) => Travel(laneController, duration, easing);

        public virtual Coroutine Travel(float duration) => Travel(laneController, duration);

        public override void SetData(TextAsset data) => (this as IController).data = Helper.Parse<Data>(data);

    }

}
