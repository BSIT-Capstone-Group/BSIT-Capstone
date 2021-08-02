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
        int _maxCount;
        public int maxCount { get => _maxCount; set => _maxCount = value; }
        [SerializeField]
        int _indexInterval;
        public int indexInterval { get => _indexInterval; set => _indexInterval = value; }
        [SerializeField]
        int _startIndex;
        public int startIndex { get => _startIndex; set => _startIndex = value; }
        [SerializeField]
        int _endIndex;
        public int endIndex { get => _endIndex; set => _endIndex = value; }
        [SerializeField]
        List<int> _indexRange;
        public List<int> indexRange { get => _indexRange; set => _indexRange = value; }

        
        public AxisInfo(
            int maxCount=-1,
            int indexInterval=-1,
            int startIndex=-1,
            int endIndex=-1,
            List<int> indexRange=null
        ) {
            this._maxCount = maxCount;
            this._indexInterval = indexInterval;
            this._startIndex = startIndex;
            this._endIndex = endIndex;
            this._indexRange = indexRange;

        }

    }

}