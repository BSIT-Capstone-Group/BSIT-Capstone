/*
 * Date Created: Tuesday, July 20, 2021 6:55 PM
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
using CoDe_A.Lakbay.Modules.Core;

namespace CoDe_A.Lakbay.Modules.Game.Road {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IData {
        List<List2D<string>> rows { get; set; }
        
    }

    [Serializable]
    public struct Data : IData {
        [SerializeField]
        private List<List2D<string>> _rows;
        public List<List2D<string>> rows { get => _rows; set => _rows = value; }

    }

}