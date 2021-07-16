/*
 * Date Created: Monday, July 12, 2021 5:52 PM
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

namespace CoDe_A.Lakbay.Utilities {
    public static class Input {
        public static Keyboard keyboard => InputSystem.GetDevice<Keyboard>();
        public static Mouse mouse => InputSystem.GetDevice<Mouse>();
        public static Touchscreen touchscreen => InputSystem.GetDevice<Touchscreen>();

    }

}