/*
 * Date Created: Sunday, July 4, 2021 1:47 PM
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

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Utilities {
    public static class Event {
        [Serializable]
        public class OnValueChange<T> : UnityEvent<T, T> {}
        [Serializable]
        public class OnValueChange<T0, T1> : UnityEvent<T0, T1, T1> {}
        [Serializable]
        public class OnStringChange<T> : OnValueChange<T, string> {}
        [Serializable]
        public class OnIntChange<T> : OnValueChange<T, int> {}
        [Serializable]
        public class OnFloatChange<T> : OnValueChange<T, float> {}
        [Serializable]
        public class OnBoolChange<T> : OnValueChange<T, bool> {}

    }

}