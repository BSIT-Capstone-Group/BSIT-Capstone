/*
 * Date Created: Tuesday, July 20, 2021 8:26 PM
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
    public struct Entry {
        [Serializable]
        public enum Type { Text, Image }

        [SerializeField]
        private Text _text;
        public Text text { get => _text; set => _text = value; }
        [SerializeField]
        private List<Image> _images;
        public List<Image> images { get => _images; set => _images = value; }

        public Type type => images != null && images.GetCount() > 0 ? Type.Image : Type.Text;

    }

}