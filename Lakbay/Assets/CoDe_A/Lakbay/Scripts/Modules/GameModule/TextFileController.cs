using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CoDe_A.Lakbay.Modules.GameModule {
    public class TextFileController : DatabaseController {
        public static readonly new string ADDRESSABLE_LABEL = "Text Files";
        public static Dictionary<string, TextAsset> textFiles = new Dictionary<string, TextAsset>();

        public static IEnumerator loadAssets(
            Action<int, int, float, TextAsset> onProgress,
            Action<float, Dictionary<string, TextAsset>> onComplete
        ) {
            Action<float, Dictionary<string, TextAsset>> oc = (tt, a) => {
                textFiles = a;
            };
            oc += onComplete;
            yield return loadAssets<TextAsset>(TextFileController.ADDRESSABLE_LABEL, onProgress, oc);

        }

    }

}
