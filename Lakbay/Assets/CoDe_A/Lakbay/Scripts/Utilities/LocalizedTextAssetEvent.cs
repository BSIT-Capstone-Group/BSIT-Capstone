/*
 * Date Created: Tuesday, June 29, 2021 8:27 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;

using UnityEngine.Localization;
using UnityEngine.Events;
using UnityEngine.Localization.Components;

namespace CoDe_A.Lakbay.Utilities {
    [Serializable]
    public class LocalizedTextAsset : LocalizedAsset<TextAsset> {}

    [Serializable]
    public class TextAssetEvent : UnityEvent<TextAsset> {}

    public class LocalizedTextAssetEvent : LocalizedAssetEvent<TextAsset, LocalizedTextAsset, TextAssetEvent> {}

}