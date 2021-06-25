using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using CloneExtensions;

using Force.DeepCloner;

using Newtonsoft.Json;

using UnityEngine;

using YamlDotNet.Serialization;

namespace CoDe_A.Lakbay.Utilities {
    public static class Helper {
        public static readonly float ReadingCharacterPerSecond = 25.0f;

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

        public static void DestroyChildren(Transform parent) {
            for(int i = 0; i < parent.childCount; i++) {
                GameObject.Destroy(parent.GetChild(i).gameObject);

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
            System.Random random = new System.Random();

            return Helper.Shuffle<T>(random, array);

        }

        public static T PickRandomly<T>(System.Random random, T[] array) {
            int index = random.Next(array.Length);
            return array[index];

        }

        public static T PickRandomly<T>(T[] array) {
            return PickRandomly<T>(new System.Random(), array);

        }

        public static T[] Reverse<T>(T[] array) {
            List<T> list = new List<T>(array);
            list.Reverse();

            return list.ToArray();

        }

        public static T ParseJson<T>(string str) {
            return JsonConvert.DeserializeObject<T>(str);

        }

        public static T ParseYaml<T>(string str) {
            return new Deserializer().Deserialize<T>(str);

        }
        
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

            while (elapsedTime < duration) {
                float time = !fixedUpdate ? Time.deltaTime : Time.fixedDeltaTime;
                time = onRun != null ? onRun(time, elapsedTime, duration) : time;

                elapsedTime += time;

                if(!fixedUpdate) yield return new WaitForEndOfFrame();
                else yield return new WaitForFixedUpdate();

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
        
    }

}