/*
 * Date Created: Friday, July 23, 2021 8:50 AM
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


    [Serializable]
    public struct Move {
        [SerializeField]
        private Vector3 _from;
        [YamlIgnore]
        public Vector3 from { get => _from; set => _from = value; }
        [SerializeField]
        private Vector3 _to;
        [YamlIgnore]
        public Vector3 to { get => _to; set => _to = value; }
        [SerializeField]
        private Easing.Ease _easing;
        public Easing.Ease easing { get => _easing; set => _easing = value; }
        [SerializeField]
        private float _speed;
        public float speed { get => _speed; set => _speed = value; }
        [SerializeField]
        private float _duration;
        public float duration { get => _duration; set => _duration = value; }
        [SerializeField]
        private float _progress;
        public float progress { get => _progress; set => _progress = value; }

        public Vector3 current {
            get {
                return Easing.Use(from, to, progress, easing);

            }

        }


        public Move(
            Vector3 from=default,
            Vector3 to=default,
            Easing.Ease easing=default,
            float speed=default,
            float duration=default,
            float progress=default
        ) {
            this._from = from;
            this._to = to;
            this._easing = easing;
            this._speed = speed;
            this._duration = duration;
            this._progress = progress;

        }

    }

}