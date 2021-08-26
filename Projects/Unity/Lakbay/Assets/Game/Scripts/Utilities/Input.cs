/*
 * Date Created: Wednesday, August 25, 2021 8:15 PM
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
using UnityEngine.InputSystem;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Utilities {
    public abstract class Input {
        public static Keyboard keyboard => InputSystem.GetDevice<Keyboard>();
        public static Mouse mouse => InputSystem.GetDevice<Mouse>();
        public static Touchscreen touchscreen => InputSystem.GetDevice<Touchscreen>();

    }

    public abstract class IInput : Input {}

}