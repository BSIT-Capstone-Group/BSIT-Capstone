/*
 * Date Created: Friday, August 27, 2021 9:37 AM
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

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Repeater : Controller {
        protected bool _repeated = false;
        protected bool _freed = false;
        protected Repeater _next;
        
        public virtual Repeater next => _next;
        
        public RepeaterHandler handler;

        public override void Awake() {
            base.Awake();

        }

        public override void Update() {
            base.Update();
            if(gameObject.IsBoundsVisible()) {
                Repeat();

            } else {
                Free();

            }

        }

        public override void OnBoundsInvisible() {
            base.OnBoundsInvisible();
            Free();

        }

        public override void OnEnable() {
            base.OnEnable();
            if(!handler) {
                var rh = transform.parent.GetComponent<RepeaterHandler>();
                handler = rh;

            }

        }

        public virtual void Free() {
            if(!handler.infinite) return;
            if(_freed) return;

            handler.freeRepeaters.Enqueue(this);
            transform.SetAsLastSibling();
            _repeated = false;
            _freed = true;

            OnFree();
            
        }

        public virtual void OnFree() {


        }

        public virtual void Repeat() {
            if(!handler.canRepeat) return;
            if(_repeated) return;

            try {
                _next = handler.freeRepeaters.Dequeue();
                _next.transform.position = transform.position;
                var pos = Vector3.zero;
                var size = gameObject.GetSize();

                switch(handler.direction) {
                    case RepeatDirection.Front:
                        pos = Vector3.forward * size.z;
                        break;

                    case RepeatDirection.Back:
                        pos = Vector3.back * size.z;
                        break;

                    case RepeatDirection.Top:
                        pos = Vector3.up * size.y;
                        break;

                    case RepeatDirection.Bottom:
                        pos = Vector3.down * size.y;
                        break;

                    case RepeatDirection.Left:
                        pos = Vector3.left * size.x;
                        break;

                    case RepeatDirection.Right:
                        pos = Vector3.right * size.x;
                        break;

                    default:
                        break;

                }
                
                _next.transform.Translate(pos);
                _repeated = true;
                _freed = false;

                OnRepeat();

            } catch {}

        }

        public virtual void OnRepeat() {

        }

    }

}