/*
 * Date Created: Sunday, August 29, 2021 2:42 AM
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
    public enum RepeatDirection {
        Front, Back,
        Top, Bottom,
        Left, Right

    }

    public class RepeaterHandler : Controller {
        protected bool _initialized = false;
        public RepeatDirection direction;
        public bool infinite = true;
        public int limit = 15;
        public readonly Queue<Repeater> freeRepeaters = new Queue<Repeater>();
        public virtual bool canRepeat {
            get => infinite ? true : freeRepeaters.Count != 0;

        }

        public override void Awake() {
            base.Awake();
            Initialize();

        }

        public virtual void Initialize() {
            if(!_initialized) {
                var osec = gameObject.GetComponentInChildren<Repeater>();
                if(!osec) return;
                _initialized = true;

                for(int i = 0; i < limit - 1; i++) {
                    var sec = Instantiate(osec, transform);
                    sec.name = $"{osec.name} ({i + 1})";
                    freeRepeaters.Enqueue(sec);

                }

                osec.name = $"{osec.name} (0)";

            }

        }

    }

}