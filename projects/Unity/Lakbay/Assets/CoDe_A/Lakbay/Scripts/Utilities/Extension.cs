/*
 * Date Created: Friday, July 2, 2021 7:05 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Utilities {
    public static class Extension {
        public static Vector3 GetProgress(this float value, Vector3 from, Vector3 to) {
            return (to - from) * value + from;

        }

        public static Vector3 GetProgress(this int value, Vector3 from, Vector3 to) {
            return ((float) value).GetProgress(from, to);

        }

        public static float GetProgress(this float value, float from, float to) {
            return (to - from) * value + from;

        }

        public static int GetProgress(this int value, int from, int to) {
            return (int) ((float) value).GetProgress(from, to);

        }

        public static bool TryDestroyComponent<T>(this GameObject gameObject) {
            return gameObject.TryDestroyComponent(typeof(T));

        }

        public static bool TryDestroyComponent(this GameObject gameObject, Type type) {
            if(gameObject.HasComponent(type)) {
                gameObject.DestroyComponent(type);
                return true;

            }

            return false;

        }

        public static bool HasComponent(this GameObject gameObject, Type type) {
            return gameObject.GetComponent(type) != null;

        }

        public static bool HasComponent<T>(this GameObject gameObject) => gameObject.HasComponent(typeof(T));

        public static void DestroyComponent(this GameObject gameObject, Type type) {
            var co = gameObject.GetComponent(type);
            GameObject.Destroy(co);

        }

        public static void DestroyComponent<T>(this GameObject gameObject) {
            gameObject.DestroyComponent(typeof(T));

        }

        public static bool IsTwoDimensional(this GameObject gameObject) {
            return gameObject.HasComponent<RectTransform>();

        }

        public static Color AsColor(this string str) {
            ColorUtility.TryParseHtmlString(str, out var c);
            return c;
        
        }

        public static string ToString<T>(this IEnumerable<T> enumerable, bool pretty) {
            if(!pretty) return enumerable.ToString();
            return "[" + string.Join(", ", enumerable) + "]";

        }

        public static bool Contains<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {
            return enumerable.Contains((t, i) => predicate(t));

        }

        public static bool Contains<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate) {
            int ii = 0;
            foreach(var i in enumerable) {
                if(predicate(i, ii++)) return true;

            }

            return false;

        }

        public static T Find<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate) {
            return enumerable.Find((t, i) => predicate(t));

        }

        public static T Find<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate) {
            int ii = 0;
            foreach(var i in enumerable) {
                if(predicate(i, ii++)) return i;

            }

            return default;

        }

        public static bool TryFind<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate, out T value) {
            return enumerable.TryFind((t, i) => predicate(t), out value);

        }

        public static bool TryFind<T>(this IEnumerable<T> enumerable, Func<T, int, bool> predicate, out T value) {
            value = default;

            if(enumerable.Contains(predicate)) {
                value = enumerable.Find(predicate);
                return true;

            }

            return false;

        }

        public static bool Either<T>(this T value, params T[] otherValues) {
            foreach(var v in otherValues) {
                if(v.Equals(value)) return true;

            }

            return false;

        }

        public static bool Within(this float value, float lower, float upper) {
            return value >= lower && value <= upper;

        }

        public static bool Within(this int value, int lower, int upper) {
            return ((float) value).Within(lower, upper);

        }

        public static bool All(this IEnumerable<bool> enumerable) {
            return enumerable.All((i) => i);

        }

        public static IEnumerable<KeyValuePair<int, T>> Enumerate<T>(this IEnumerable<T> enumerable) {
            int i = 0;
            foreach(var e in enumerable) {
                yield return new KeyValuePair<int, T>(i++, e);

            }

        }

        public static int GetCount(this IEnumerable enumerable) {
            int s = 0;
            foreach(var e in enumerable) s++;
            return s;
            
        }

        public static bool IsEmpty(this IEnumerable enumerable) {
            foreach(var e in enumerable) return false;
            return true;

        }

        public static List2D<T> ToList2D<T>(this IEnumerable<T> l) {
            // return l as List2D<T>;
            var ll = new List2D<T>();
            foreach(var i in l) ll.Add(i);
            return ll;

        }

        public static IEnumerable<T> GetFlags<T>(this T e, bool includeSelf=false) where T : Enum {
            var flags = Enum.GetValues(e.GetType()).Cast<T>().Where((ee) => e.HasFlag(ee));
            if(includeSelf) flags = flags.Append(e);

            return flags;

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
            var rcs = gameObject.GetComponentsInChildren<Renderer>();
            var tcs = gameObject.GetComponentsInChildren<Terrain>();
            var sizes = new List<Vector3>();

            if(rcs != null) {
                foreach(var r in rcs) sizes.Add(r.bounds.size);

            }

            if(tcs != null) {
                foreach(var t in tcs) sizes.Add(t.terrainData.bounds.size);

            }

            if(sizes.Count > 0) {
                var m = Enumerable.Range(0, 3).Select<int, float>((i) => {
                    return sizes.Max((s) => s[i]);
                });
                
                return m.AsVector3();

            }

            return Vector3.zero;

        }


        public static void CopyToClipboard(this string str) => Helper.CopyToClipboard(str); 
        
        public static T ParseJson<T>(this string str) => Helper.ParseJson<T>(str);

        public static T ParseYaml<T>(this string str) => Helper.ParseYaml<T>(str);

        public static T Parse<T>(this string str) => Helper.Parse<T>(str);


        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable) => Helper.Shuffle<T>(enumerable.ToArray());

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
                var s = Helper.YamlSerializer;
                return s.Serialize(obj, typeof(T));

            } catch { return null; }

        }

        public static string Serialize<T>(this Serializer serializer, T obj) {
            return serializer.Serialize(obj, typeof(T));

        }

        public static string Serialize(this Serializer serializer, object obj, Type type) {
            var sw = new StringWriter();
            serializer.Serialize(sw, obj, type);
            return sw.ToString();

        }

    }

}