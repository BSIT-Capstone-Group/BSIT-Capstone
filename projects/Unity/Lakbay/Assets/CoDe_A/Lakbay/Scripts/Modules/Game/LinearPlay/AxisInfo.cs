/*
 * Date Created: Sunday, July 25, 2021 8:41 AM
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

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    [Serializable]
    public struct AxisInfo {
        [SerializeField]
        int _max;
        public int max { get => _max; set => _max = value; }
        [SerializeField]
        int _interval;
        public int interval { get => _interval; set => _interval = value; }
        [SerializeField]
        List<int> _range;
        public List<int> range { get => _range; set => _range = value; }

        
        public AxisInfo(int max=-1, int interval=-1, List<int> range=null) {
            this._max = max;
            this._interval = interval;
            this._range = range;

        }

    }

}