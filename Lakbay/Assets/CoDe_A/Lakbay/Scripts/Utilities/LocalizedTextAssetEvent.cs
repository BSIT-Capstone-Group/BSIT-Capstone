/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace CoDe_A.Lakbay.Utilities {
    [Serializable]
    public class LocalizedTextAsset : LocalizedAsset<TextAsset> {}

    [Serializable]
    public class TextAssetEvent : UnityEvent<TextAsset> {}

    public class LocalizedTextAssetEvent : LocalizedAssetEvent<TextAsset, LocalizedTextAsset, TextAssetEvent> {
    }

}