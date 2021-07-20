/*
 * Date Created: Tuesday, July 20, 2021 8:29 PM
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
    using Asset = Sprite;


    [Serializable]
    public struct Image : IAsset<Asset> {
        public Asset asset => default;
        [SerializeField]
        private string _path;
        public string path { get => _path; set => _path = value; }

    }

}