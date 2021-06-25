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

    public class Controller : Input.Controller {
        [BoxGroup("Player.Controller")]
        public Data data;
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Player.Controller")]
        protected SlideDirection _slideDirection = SlideDirection.None;
        public SlideDirection slideDirection => _slideDirection;
        [SerializeField, NaughtyAttributes.ReadOnly, BoxGroup("Player.Controller")]
        protected bool _needsToSlide = false;
        public bool needsToSlide => _needsToSlide;
        [BoxGroup("Player.Controller")]
        public Easing.Ease slideEasing = Easing.Ease.EaseInBounce;
        [SerializeField, BoxGroup("Player.Controller")]
        private int _laneIndex = 1;
        public int laneIndex {
            get => _laneIndex;
            set {
                value = Helper.Limit(value, laneGroupController.laneControllers.Count - 1);
                if(value == laneIndex) return;
                _needsToSlide = true;
                _slideDirection = value < laneIndex ? SlideDirection.Left : (
                    value > laneIndex ? SlideDirection.Right : SlideDirection.None
                );
                _laneIndex = value;
                Slide(data.slideDuration, slideEasing);

            }

        }
        [BoxGroup("Player.Controller")]
        public Lane.Group.Controller laneGroupController;
        public Lane.Controller laneController => laneGroupController.laneControllers[laneIndex];
        public Vector3 laneControllerTargetPosition {
            get => GetLaneControllerTargetPosition(laneController);

        }

        public override void OnValidate() {
            base.OnValidate();
            _needsToSlide = true;

        }

        public override void OnNeedsUpdate() {
            base.OnNeedsUpdate();
            if(needsToSlide) {
                laneIndex = laneIndex;
                _needsToSlide = false;

            }

        }

        public override void ReceiveInput() {
            base.ReceiveInput();
            ReceiveSlideInput();

        }

        public void ReceiveSlideInput() {
            var l = GetKeyDown(KeyCode.LeftArrow);
            var r = GetKeyDown(KeyCode.RightArrow);
            if(l || r) {
                if(l) laneIndex--;
                else if(r) laneIndex++;

            }

        }

        public Vector3 GetLaneControllerTargetPosition(Lane.Controller laneController) {
            var pos = transform.position;
            var lpos = laneController.transform.position;
            return new Vector3(lpos.x, pos.y, pos.z);

        }

        public Coroutine Slide(Lane.Controller laneController, float duration, Easing.Ease easing) {
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
                    return t * data.slideSpeed;

                },
                onStop: () => {
                    transform.position = GetLaneControllerTargetPosition(laneController);
                    _slideDirection = SlideDirection.None;

                },
                true
            );

        }

        public Coroutine Slide(Lane.Controller laneController, float duration) {
            return Slide(laneController, duration, Easing.Ease.Linear);

        }

        public Coroutine Slide(Lane.Controller laneController) => Slide(laneController, 0.001f);

        public Coroutine Slide(float duration, Easing.Ease easing) => Slide(laneController, duration, easing);

        public Coroutine Slide(float duration) => Slide(laneController, duration);

    }

}
