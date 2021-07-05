/*
 * Date Created: Friday, July 2, 2021 7:05 AM
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
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using Code_A_Old_2.Lakbay.Utilities;

namespace Code_A_Old_2.Lakbay.Utilities {
    public static class Extension {
        public static T ParseJson<T>(this TextAsset textAsset) => Helper.ParseJson<T>(textAsset);

        public static T ParseYaml<T>(this TextAsset textAsset) => Helper.ParseYaml<T>(textAsset);

        public static T Parse<T>(this TextAsset textAsset) => Helper.Parse<T>(textAsset);


        public static GameObject[] GetChildren(this GameObject parent) {
            return Helper.GetChildren(parent);
            
        }

        public static T GetComponent<T>(this GameObject component, bool forceAdd) where T : Component {
            var c = component.GetComponent<T>();
            if(c == null && forceAdd) c = component.gameObject.AddComponent<T>();

            return c;

        }


        public static void CopyToClipboard(this string str) => Helper.CopyToClipboard(str); 
        
        public static T ParseJson<T>(this string str) => Helper.ParseJson<T>(str);

        public static T ParseYaml<T>(this string str) => Helper.ParseYaml<T>(str);

        public static T Parse<T>(this string str) => Helper.Parse<T>(str);


        public static T[] Shuffle<T>(this T[] array) => Helper.Shuffle<T>(array);

        public static T PickRandomly<T>(this T[] array) => Helper.PickRandomly<T>(array);

        public static T[] Reverse<T>(this T[] array) => Helper.Reverse<T>(array);


        public static int[] GetRgba(this Color32 color) => new int[] {color.r, color.g, color.b, color.a};

        public static Color AsColor(this Color32 color) => (Color) color;


        public static Color32 AsColor32(this Color color) => (Color32) color;

        public static string GetHex(this Color color) => "#" + ColorUtility.ToHtmlStringRGBA(color);

        public static float[] GetRgba(this Color color) => new float[] {color.r, color.g, color.b, color.a};

        public static string AsYaml<T>(this T obj) {
            try {
                var s = new SerializerBuilder().Build();
                return s.Serialize(obj);

            } catch { return null; }

        }

    }

}