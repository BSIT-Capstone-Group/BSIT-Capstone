/*
 * Date Created: Friday, July 23, 2021 7:41 AM
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
    public struct Focus {
        [SerializeField]
        private Outline.Mode _mode;
        public Outline.Mode mode { get => _mode; set => _mode = value; }
        [SerializeField]
        private float _width;
        public float width { get => _width; set => _width = value; }
        [SerializeField]
        private string _hexColor;
        public string hexColor { get => _hexColor; set => _hexColor = value; }

        public Color color => hexColor != null ? hexColor.AsColor() : Color.white;

    }

}