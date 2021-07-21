/*
 * Date Created: Wednesday, July 21, 2021 7:06 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Controllers {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IAssetController<T> {
        T component { get; set; }
        
    }

    public class AssetController<T> : Controller, IAssetController<T> {
        [SerializeField]
        protected T _component;
        public virtual T component { get => _component; set => _component = value; }

    }

}