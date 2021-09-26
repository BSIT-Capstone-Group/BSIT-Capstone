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
using UnityEngine.SceneManagement;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.LinearPlay {
    using Core;

    public class Repeater : Controller {
        public int limit = 15;
        protected bool _initialized = false;
        protected int _index = 0;
        public virtual int index => _index;
        [SerializeField]
        protected List<Repeatable> _repeatables = new List<Repeatable>();
        protected readonly List<Repeatable> _originalRepeatables = new List<Repeatable>();
        public Queue<Repeatable> repeatables = new Queue<Repeatable>();

        public override void Awake() {
            base.Awake();
            Initialize();

        }

        public virtual void Initialize() {
            if(_initialized) return;

            gameObject.DestroyChildren();
            Repeatable previous = null;

            for(int i = 0; i < limit; i++) {
                var _repeatable = _repeatables.PickRandomly();
                var repeatable = Instantiate(_repeatable, transform);
                _originalRepeatables.Add(repeatable);
                repeatable.name = _repeatable.name + $" ({i})";
                Repeat(repeatable, previous);
                previous = repeatable;

            }

            _initialized = true;

        }

        public override void Update() {
            base.Update();
            if(_initialized) {
                if(repeatables.Count > 0) {
                    var repeatable = repeatables.Peek();
                    if(!repeatable.occupied) {
                        repeatable = repeatables.Dequeue();
                        Repeat(repeatable, repeatables.Last());

                    }

                }

            }

        }

        protected virtual void Repeat(Repeatable repeatable, Repeatable previousRepeatable=null) {
            var pos = previousRepeatable ? previousRepeatable.transform.position : Vector3.zero;
            var size = previousRepeatable ? previousRepeatable.gameObject.GetSize() : Vector3.zero;
            repeatable.transform.position = pos + (Vector3.forward * size.z);
            repeatables.Enqueue(repeatable);
            repeatable.OnRepeat(this, index);
            _index++;

        }

    }

}