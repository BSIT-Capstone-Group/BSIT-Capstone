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

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Player {
    public enum SlideDirection { Left, None, Right };

    public interface IController {
        Data data { get; set; }

        bool canReceiveSlideInput { get; set; }
        bool canReceiveTravelInput { get; set; }

        float travelDuration { get; set; }
        float travelSpeed { get; set; }
        float slideDuration { get; set; }
        float slideSpeed { get; set; }
        bool isTraveling { get; set; }
        int laneIndex { get; set; }

        void SetData(Data data);

    }

    public class Controller : Input.Controller, IController {
        private Coroutine _travelCoroutine;


        [SerializeField, Label("Data"), BoxGroup("Player.Controller")]
        private Data __data;
        [BoxGroup("Player.Controller")]
        public Lane.Group.Controller laneGroupController;


        protected Data _data;
        public virtual Data data {
            get => _data;
            set {
                if(value == data) return;
                _data = value;
                __data = value;

            }

        }
        protected bool _canReceiveSlideInput = true;
        public virtual bool canReceiveSlideInput {
            get => _canReceiveSlideInput;
            set {
                if(value == canReceiveSlideInput) return;
                _canReceiveSlideInput = value;
                __canReceiveSlideInput = value;

            }

        }

        protected bool _canReceiveTravelInput = true;
        public virtual bool canReceiveTravelInput {
            get => _canReceiveTravelInput;
            set {
                if(value == canReceiveTravelInput) return;
                _canReceiveTravelInput = value;
                __canReceiveTravelInput = value;

            }

        }


        [Header("Travel")]
        [BoxGroup("Player.Controller")]
        public Easing.Ease travelEasing = Easing.Ease.Linear;
        [SerializeField, Label("Travel Duration"), BoxGroup("Player.Controller")]
        private float __travelDuration = 60.0f;
        [SerializeField, Label("Travel Speed"), BoxGroup("Player.Controller")]
        private float __travelSpeed = 1.0f;
        [SerializeField, Label("Is Traveling"), BoxGroup("Player.Controller")]
        private bool __isTraveling = false;
        [SerializeField, Label("Can Receive Travel Input"), BoxGroup("Player.Controller")]
        private bool __canReceiveTravelInput = true;


        [Header("Slide")]
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Player.Controller")]
        protected SlideDirection _slideDirection = SlideDirection.None;
        [BoxGroup("Player.Controller")]
        public Easing.Ease slideEasing = Easing.Ease.Spring;
        [SerializeField, Label("Slide Duration"), BoxGroup("Player.Controller")]
        private float __slideDuration = 0.15f;
        [SerializeField, Label("Slide Speed"), BoxGroup("Player.Controller")]
        private float __slideSpeed = 1.0f;
        [SerializeField, Label("Lane Index"), BoxGroup("Player.Controller")]
        private int __laneIndex = 0;
        [SerializeField, Label("Can Receive Slide Input"), BoxGroup("Player.Controller")]
        private bool __canReceiveSlideInput = true;

        
        protected float _travelDuration = 60.0f;
        protected float _travelSpeed = 1.0f;
        protected float _slideDuration = 0.15f;
        protected float _slideSpeed = 1.0f;
        protected bool _isTraveling = true;
        protected int _laneIndex = 1;
        

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
                value = Helper.Limit(value, laneGroupController.laneControllers.Count - 1);
                if(value == laneIndex) return;
                _slideDirection = value < laneIndex ? SlideDirection.Left : (
                    value > laneIndex ? SlideDirection.Right : SlideDirection.None
                );
                _laneIndex = value;
                __laneIndex = value;
                Slide(slideDuration, slideEasing);

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

        public override void Start() {
            base.Start();
            // SetData(dataTextAsset);

        }

        public override void OnValidate() {
            base.OnValidate();

        }

        public override void OnNeedsUpdate() {
            base.OnNeedsUpdate();

            data = __data;

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

        public override void SetData(TextAsset textAsset) {
            SetData(Helper.Parse<Data>(textAsset));

        }

        public virtual void SetData(Data data) {
            SetData((Core.Data) data);

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
            _isTraveling = true;
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

    }

}
