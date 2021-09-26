/*
 * Date Created: Friday, September 17, 2021 8:49 AM
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
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Localization;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    /// <summary>A <see cref="Component">Component</see> that can be attached to
    /// a <see cref="Component">GameObject</see>.</summary>
    public class Module : MonoBehaviour {
        [ContextMenu("Localize")]
        protected virtual void _Localize() {
            Localize();
            
        }

        public virtual void Localize() {


        }

    }

}