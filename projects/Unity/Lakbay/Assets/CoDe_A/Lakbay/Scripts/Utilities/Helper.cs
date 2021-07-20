/*
 * Date Created: Tuesday, June 29, 2021 6:50 PM
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

using System.Collections.Specialized;
using Force.DeepCloner;
using UnityEngine.Events;
using UnityEditor.Events;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CoDe_A.Lakbay.Utilities {
    public static class Helper {
        public const float ReadingCharacterPerSecond = 25.0f;
        public static readonly System.Random Random;

        public static readonly Serializer YamlSerializer = new SerializerBuilder()
            .EmitDefaults()
            .EnsureRoundtrip()
            .Build();
        public static readonly Deserializer YamlDeserializer = new Deserializer();

        static Helper() {
            Random = new System.Random();
            
        }

        public static int GetIndexOfChild(Transform parent, Transform child) {
            int index = -1;

            for(int i = 0; i < parent.childCount; i++) {
                if(parent.GetChild(i) == child) {
                    index = i;
                    break;

                }

            }

            return index;

        }

        public static Transform[] GetChildren(Transform parent) {
            List<Transform> children = new List<Transform>();

            for(int i = 0; i < parent.childCount; i++) {
                children.Add(parent.GetChild(i));

            }

            return children.ToArray();

        }

        public static GameObject[] GetChildren(GameObject parent) {
            return GetChildren(parent.transform).Select<Transform, GameObject>(
                (t) => t.gameObject
            ).ToArray();
            
        }

        public static GameObject[] GetChildrenWithTag(GameObject gameObject, string tag) {
            List<GameObject> gos = new List<GameObject>();
            foreach(var c in GetChildren(gameObject)) {
                if(c.CompareTag(tag)) gos.Add(c);

            }

            return gos.ToArray();

        }

        public static Transform[] GetChildrenWithTag(Transform transform, string tag) {
            return GetChildrenWithTag(transform.gameObject, tag).Select(
                (g) => g.transform
            ).ToArray();

        }

        public static GameObject GetChildWithTag(GameObject gameObject, string tag) {
            return GetChildrenWithTag(gameObject, tag)[0];

        }

        public static Transform GetChildWithTag(Transform transform, string tag) {
            return GetChildrenWithTag(transform, tag)[0];

        }

        public static void DestroyChildren(Transform parent, bool immediate=false) {
            for(int i = 0; i < parent.childCount; i++) {
                if(!immediate) GameObject.Destroy(parent.GetChild(i).gameObject);
                else GameObject.DestroyImmediate(parent.GetChild(i).gameObject);

            }

        }

        public static float GetSum(params float[] numbers) {
            float sum = 0.0f;
            foreach(float number in numbers) sum += number;

            return sum;

        }

        public static float GetProduct(params float[] numbers) {
            float sum = 0.0f;
            foreach(float number in numbers) sum *= number;

            return sum;

        }

        // Source: https://stackoverflow.com/a/110570/14733693
        public static T[] Shuffle<T>(System.Random random, T[] array) {
            T[] narray = (T[]) array.Clone();

            int n = narray.Length;
            while (n > 1) {
                int k = random.Next(n--);
                T temp = narray[n];
                narray[n] = narray[k];
                narray[k] = temp;

            }

            return narray;

        }

        public static T[] Shuffle<T>(T[] array) {
            return Helper.Shuffle<T>(Random, array);

        }

        public static T PickRandomly<T>(System.Random random, T[] array) {
            if(array.Length == 0) return default;
            int index = random.Next(array.Length);
            return array[index];

        }

        public static T PickRandomly<T>(T[] array) {
            return PickRandomly<T>(Random, array);

        }

        public static T[] Reverse<T>(T[] array) {
            List<T> list = new List<T>(array);
            list.Reverse();

            return list.ToArray();

        }

        public static object ParseJson(string str, Type type) {
            return JsonConvert.DeserializeObject(str, type);

        }

        public static T ParseJson<T>(string str) {
            return JsonConvert.DeserializeObject<T>(str);

        }

        public static object ParseJson(TextAsset textAsset, Type type) {
            return ParseJson(textAsset.text, type);

        }

        public static T ParseJson<T>(TextAsset textAsset) {
            return ParseJson<T>(textAsset.text);

        }

        public static object ParseYaml(string str, Type type) {
            return YamlDeserializer.Deserialize(str, type);

        }

        public static T ParseYaml<T>(string str) {
            return YamlDeserializer.Deserialize<T>(str);

        }
        
        public static object ParseYaml(TextAsset textAsset, Type type) {
            return ParseYaml(textAsset.text, type);

        }
        
        public static T ParseYaml<T>(TextAsset textAsset) {
            return ParseYaml<T>(textAsset.text);

        }

        public static object Parse(string str, Type type) {
            try {
                return ParseYaml(str, type);

            } catch { 
                return ParseJson(str, type);

            }

        }

        public static T Parse<T>(string str) {
            try {
                return ParseYaml<T>(str);

            } catch (Exception e) {
                Debug.Log(e.ToString()); 
                return ParseJson<T>(str);

            }

        }

        public static object Parse(TextAsset textAsset, Type type) => Parse(textAsset.text, type);

        public static T Parse<T>(TextAsset textAsset) => Parse<T>(textAsset.text);
        
        public static string AsPrettyString(OrderedDictionary keyAndValues, string separator) {
            List<string> strs = new List<string>();
            foreach(DictionaryEntry de in keyAndValues) {
                strs.Add($"{de.Key}: {de.Value}");

            }

            return String.Join(separator, strs);

        }

        public static string AsPrettyString(OrderedDictionary keyAndValues) {
            return AsPrettyString(keyAndValues, ", ");

        }

        public static void Log(OrderedDictionary keyAndValues, string separator) {
            Debug.Log(AsPrettyString(keyAndValues, separator));

        }

        public static void Log(string label, OrderedDictionary keyAndValues) {
            Debug.Log($"[{label}]:\t" + AsPrettyString(keyAndValues, ", "));

        }

        public static void Log(string label, object obj) {
            Debug.Log($"[{label}]:\t" + obj.ToString());

        }

        public static void Log(OrderedDictionary keyAndValues) => Log(keyAndValues, ", ");

        public static void Log(string separator, params object[] objs) {
            Debug.Log(String.Join(separator, objs));

        }

        public static void Log(params System.Object[] objs) {
            Log(", ", objs);

        }

        public static void CopyToClipboard(string str) => GUIUtility.systemCopyBuffer = str;
        
        public static void ClearClipboard() => GUIUtility.systemCopyBuffer = "";

        public static T Clone<T>(T obj, bool deep) {
            return deep ? obj.DeepClone<T>() : obj.ShallowClone<T>();

        }

        public static T Clone<T>(T obj) => Clone(obj, false);

        public static float Limit(float value, float minValue, float maxValue) {
            return Mathf.Max(Mathf.Min(value, maxValue), minValue);

        }

        public static float Limit(float value, float maxValue) {
            return Limit(value, 0, maxValue);

        }

        public static int Limit(int value, int minValue, int maxValue) {
            return Mathf.Max(Mathf.Min(value, maxValue), minValue);

        }

        public static int Limit(int value, int maxValue) {
            return Limit(value, 0, maxValue);

        }

        public static IEnumerator DoOver(
            float duration,
            Action onStart,
            Func<float, float, float, float> onRun,
            Action onStop,
            bool fixedUpdate
        ) {
            float elapsedTime = 0;
            onStart?.Invoke();

            float timeDelta = 0.0f;
            while (elapsedTime <= duration) {
                timeDelta = !fixedUpdate ? Time.deltaTime : Time.fixedDeltaTime;
                timeDelta = onRun != null ? onRun(timeDelta, elapsedTime, duration) : timeDelta;

                elapsedTime += timeDelta;

                // float starTime = Time.time;
                if(!fixedUpdate) yield return new WaitForEndOfFrame();
                else yield return new WaitForFixedUpdate();
                // timeDelta = Time.time - starTime;

            }

            onStop?.Invoke();

        }

        public static IEnumerator DoOver(
            float duration,
            Func<float, float, float, float> onRun,
            bool fixedUpdate
        ) => DoOver(duration, null, onRun, null, fixedUpdate);

        public static IEnumerator DoOver(
            float duration,
            Action onStop,
            bool fixedUpdate
        ) => DoOver(duration, null, null, onStop, fixedUpdate);

        public static IEnumerator DoOver(
            float duration,
            Func<float, float, float, float> onRun
        ) => DoOver(duration, onRun, false);

        public static IEnumerator DoOver(float duration, Action onStop) {
            return DoOver(duration, onStop, false);

        }

        public static Coroutine DoOver(
            MonoBehaviour mono,
            float duration,
            Action onStart,
            Func<float, float, float, float> onRun,
            Action onStop,
            bool fixedUpdate
        ) {
            Coroutine c = mono.StartCoroutine(
                DoOver(duration, onStart, onRun, onStop, fixedUpdate)
            );
            return c;

        }

        public static Coroutine DoOver(
            MonoBehaviour mono,
            float duration, Func<float, float, float, float> onRun,
            bool fixedUpdate
        ) {
            Coroutine c = DoOver(mono, duration, null, onRun, null, fixedUpdate);
            return c;

        }

        public static Coroutine DoOver(
            MonoBehaviour mono, float duration, Action onStop, bool fixedUpdate
        ) {
            Coroutine c = DoOver(mono, duration, null, null, onStop, fixedUpdate);
            return c;

        }

        public static Coroutine DoOver(
            MonoBehaviour mono,
            float duration,
            Func<float, float, float, float> onRun
        ) {
            Coroutine c = DoOver(mono, duration, onRun, false);
            return c;

        }

        public static Coroutine DoOver(
            MonoBehaviour mono, float duration, Action onStop
        ) {
            Coroutine c = DoOver(mono, duration, onStop, false);
            return c;

        }

        public static List<float> AsList(Vector3 vector3) {
            List<float> l = new List<float>() { vector3.x, vector3.y, vector3.z };
            return l;

        }

        public static Vector3 AsVector3(List<float> list) {
            return new Vector3(list[0], list[1], list[2]);

        }

        public static Vector3 AsVector3(params float[] list) {
            return new Vector3(list[0], list[1], list[2]);

        }

        public static bool All<T>(Func<T, bool> predicate, params T[] objs) {
            var bools = objs.Select<T, bool>(predicate);
            foreach(var b in bools) if(!b) return false;
            return true;

        }

        public static bool All(params bool[] bools) => All<bool>((b) => b, bools);

        public static bool Any<T>(Func<T, bool> predicate, params T[] objs) {
            var bools = objs.Select<T, bool>(predicate);
            foreach(var b in bools) if(b) return true;
            return false;

        }

        public static bool Any(params bool[] bools) => Any<bool>((b) => b, bools);

        public static string GetName(Type type, int length) {
            string name = type.FullName;
            List<string> names = name.Split('.').ToList();
            if(length > 0 && names.Count > length) {
                name = String.Join(
                    ".",
                    from n in name.Split('.')
                    where names.IndexOf(n) >= names.Count - length
                    select n
                );

            }

            return name;

        }

        public static string GetName(Type type) => GetName(type, -1);

        public static string GetName(object obj, int length) => GetName(obj.GetType(), length);

        public static string GetName(object obj) => GetName(obj, -1);

        public static T Pop<T>(List<T> list, int index) {
            T t = list[index];
            list.Remove(t);
            return t;

        }

        public static T Replace<T>(List<T> list, int index, T element) {
            T t = list[index];
            list[index] = (T) element;
            return t;

        }

        public static T Replace<T>(List<T> list, int index) => Replace<T>(list, index, default);

        public static T AsDelegate<T>(object obj, string propertyName, bool getter) where T : Delegate {
            var pi = obj.GetType().GetProperty(propertyName);
            var mi = getter ? pi.GetMethod : pi.SetMethod;
            return (T) Delegate.CreateDelegate(
                typeof(T), obj, mi
            );

        }

        public static T AsDelegate<T>(object obj, string propertyName) where T : Delegate {
            return AsDelegate<T>(obj, propertyName, false);

        }
        
        public static void AddPersistentListener(UnityEvent unityEvent, UnityAction call) {
            UnityEventTools.AddPersistentListener(unityEvent, call);

        }
        
        public static void AddPersistentListener(UnityEvent unityEvent, object obj, string setterName) {
            var call = AsDelegate<UnityAction>(obj, setterName);
            AddPersistentListener(unityEvent, call);

        }
        
        public static void AddPersistentListener<T0>(UnityEvent<T0> unityEvent, UnityAction<T0> call) {
            UnityEventTools.AddPersistentListener(unityEvent, call);

        }
        
        public static void AddPersistentListener<T0>(UnityEvent<T0> unityEvent, object obj, string setterName) {
            var call = AsDelegate<UnityAction<T0>>(obj, setterName);
            AddPersistentListener(unityEvent, call);

        }

        public static void RemovePersistentListener(UnityEvent unityEvent, UnityAction call) {
            UnityEventTools.RemovePersistentListener(unityEvent, call);

        }

        public static void RemovePersistentListener(UnityEvent unityEvent, int index) {
            UnityEventTools.RemovePersistentListener(unityEvent, index);

        }
 
        public static Color AsColor(string str) {
            ColorUtility.TryParseHtmlString(str, out Color color);
            return color;
            
        }

        public static Color32 AsColor32(string str) {
            ColorUtility.TryParseHtmlString(str, out Color color);
            return (Color32) color;
            
        }
 
        public static Color AsColor(params float[] colors) {
            var c = colors;
            return new Color(c[0], c[1], c[2], c.Length != 4 ? 1.0f : c[3]);
            
        }
 
        public static Color32 AsColor(params int[] colors) {
            var c = colors.Select<int, byte>((c) => (byte) c).ToArray();
            return new Color32(c[0], c[1], c[2], c.Length != 4 ? (byte) 255 : c[3]);
            
        }

        public static void RestrictedSetter<T0, T1>(
            T0 instance,
            T1 value, out T1 field, out T1 secondField,
            Action<T0, T1, T1> @event=null,
            Action<T1, T1> eventMethod=null
        ) {
            field = default;
            secondField = default;

            if(object.Equals(value, field)) return;
            var o = field; var n = value;
            field = secondField = value;
            @event?.Invoke(instance, o, n);
            eventMethod?.Invoke(o, n);

        }

        public static void RestrictedSetter<T0, T1>(
            T0 instance,
            T1 value, out T1 field, 
            Action<T0, T1, T1> @event=null,
            Action<T1, T1> eventMethod=null
        ) {
            RestrictedSetter<T0, T1>(
                instance, value, out field, out T1 i, @event, eventMethod
            );

        }

        public static bool IsGamePlaying() {
            try { return Application.isPlaying; }
            catch {}

            return false;

        }

        
        public static T[] Set<T>(ref T old, T @new) {
            T[] values = null;
            if(EqualityComparer<T>.Default.Equals(old, @new)) return values;
            values = new T[] { old, @new };
            old = @new;

            return values;

        }

        public static void Invoke<T0, T1>(T0 self, T1 old, T1 @new, Action<T0, T1, T1> @event=null, Action<T1, T1> eventMethod=null) {
            @event?.Invoke(self, old, @new);
            eventMethod?.Invoke(old, @new);

        }

        public static Tuple<bool, T1[]> SetInvoke<T0, T1>(
            T0 self, ref T1 old, T1 @new,
            Action<T0, T1, T1> @event=null, Action<T1, T1> eventMethod=null
        ) {
            T1[] values = Set(ref old, @new);
            bool res = false;
            if(values != null) {
                Invoke<T0, T1>(self, old, @new, @event, eventMethod);
                res = true;

            }

            return new Tuple<bool, T1[]>(res, new T1[] {old, @new});

        }

        public static Tuple<bool, T1[]> SetInvoke<T0, T1>(
            T0 self, ref T1 old, T1 @new,
            UnityEvent<T0, T1, T1> @event=null, Action<T1, T1> eventMethod=null
        ) {
            return SetInvoke(self, ref old, @new, @event.Invoke, eventMethod);

        }

        public static Tuple<bool, T1[]> SetInvoke<T0, T1>(
            T0 self, ref T1 old, T1 @new
        ) => SetInvoke(self, ref old, @new, default(Action<T0, T1, T1>), null);

    }

}