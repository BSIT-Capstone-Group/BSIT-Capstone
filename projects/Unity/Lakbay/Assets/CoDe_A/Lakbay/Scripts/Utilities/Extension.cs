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

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Utilities {
    public static class Extension {
        public static IEnumerable<T> GetFlags<T>(this T e) where T : Enum {
            return Enum.GetValues(e.GetType()).Cast<T>().Where((ee) => e.HasFlag(ee));

        }

        public static T PickRandomly<T>(this IEnumerable<T> enumerable) => Helper.PickRandomly<T>(enumerable.ToArray());
        public static T Pop<T>(this List<T> list, int index) => Helper.Pop<T>(list, index);
        public static T Pop<T>(this List<T> list, T item) => Helper.Pop<T>(list, list.IndexOf(item));

        public static T ParseJson<T>(this TextAsset textAsset) => Helper.ParseJson<T>(textAsset);

        public static T ParseYaml<T>(this TextAsset textAsset) => Helper.ParseYaml<T>(textAsset);

        public static T Parse<T>(this TextAsset textAsset) => Helper.Parse<T>(textAsset);


        public static GameObject Instantiate(this GameObject gameObject, GameObject parent) {
            if(parent)
                return GameObject.Instantiate(gameObject, parent.transform);

            return GameObject.Instantiate(gameObject);

        }

        public static GameObject Instantiate(this GameObject gameObject) => Instantiate(gameObject, null);

        public static void DestroyChildren(this GameObject gameObject, bool immediate=false) => Helper.DestroyChildren(gameObject.transform, immediate);


        public static GameObject[] GetChildren(this GameObject parent) {
            return Helper.GetChildren(parent);
            
        }

        public static T GetComponent<T>(this GameObject component, bool forceAdd) where T : Component {
            var c = component.GetComponent<T>();
            if(c == null && forceAdd) c = component.gameObject.AddComponent<T>();

            return c;

        }

        public static Vector3 GetSize(this GameObject gameObject) {
            var cs = gameObject.GetComponentsInChildren<Renderer>();

            if(cs != null && cs.Length > 0) {
                var m = Enumerable.Range(0, 3).Select<int, float>((i) => {
                    return cs.Max((r) => r.bounds.size[i]);
                });
                
                return m.AsVector3();

            }

            return Vector3.zero;

        }


        public static void CopyToClipboard(this string str) => Helper.CopyToClipboard(str); 
        
        public static T ParseJson<T>(this string str) => Helper.ParseJson<T>(str);

        public static T ParseYaml<T>(this string str) => Helper.ParseYaml<T>(str);

        public static T Parse<T>(this string str) => Helper.Parse<T>(str);


        public static T[] Shuffle<T>(this T[] array) => Helper.Shuffle<T>(array);

        public static T PickRandomly<T>(this T[] array) => Helper.PickRandomly<T>(array);

        public static T[] Reverse<T>(this T[] array) => Helper.Reverse<T>(array);

        public static Vector3 AsVector3(this float[] array) => Helper.AsVector3(array);
        public static Vector3 AsVector3(this int[] array) => Helper.AsVector3(array.Select((v) => (float) v).ToList());
        public static Vector3 AsVector3(this IEnumerable<float> array) => Helper.AsVector3(array.Select((v) => (float) v).ToList());
        public static Vector3 AsVector3(this IEnumerable<int> array) => Helper.AsVector3(array.Select((v) => (float) v).ToList());


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