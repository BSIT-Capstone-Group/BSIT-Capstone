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