/*
 * Date Created: Friday, September 24, 2021 7:29 AM
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
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Utilities {
    // [Serializable]
    // public class LocalizedTextAsset : LocalizedAsset<TextAsset> {
    //     public LocalizedTextAsset() : base() {}
    //     public LocalizedTextAsset(TableReference table, TableEntryReference entry) : this() {
    //         SetReference(table, entry);

    //     }

    // }

    // [Serializable]
    // public class TextAssetEvent : UnityEvent<TextAsset> {}

    public class LocalizedTextAssetEvent : LocalizedAssetEvent<TextAsset, LocalizedAsset<TextAsset>, UnityEvent<TextAsset>> {}

}