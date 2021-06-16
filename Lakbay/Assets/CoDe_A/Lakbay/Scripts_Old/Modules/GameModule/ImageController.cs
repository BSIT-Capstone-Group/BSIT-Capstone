using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CoDe_A_Old.Lakbay.Modules.GameModule {
    public class ImageController : DatabaseController {
        public static readonly new string ADDRESSABLE_LABEL = "Images";
        public static Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();

        public static IEnumerator loadAssets(
            Action<int, int, float, Sprite> onProgress,
            Action<float, Dictionary<string, Sprite>> onComplete
        ) {
            Action<float, Dictionary<string, Sprite>> oc = (tt, a) => {
                images = a;
            };
            oc += onComplete;
            yield return loadAssets<Sprite>(ImageController.ADDRESSABLE_LABEL, onProgress, oc);

        }

    }

}
