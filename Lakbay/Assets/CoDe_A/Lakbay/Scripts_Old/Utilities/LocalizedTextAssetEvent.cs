using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

namespace CoDe_A_Old.Lakbay.Utilities {
    [Serializable]
    public class LocalizedTextAsset : LocalizedAsset<TextAsset> {}

    [Serializable]
    public class TextAssetEvent : UnityEvent<TextAsset> {}

    public class LocalizedTextAssetEvent : LocalizedAssetEvent<TextAsset, LocalizedTextAsset, TextAssetEvent> {
        
    }

}