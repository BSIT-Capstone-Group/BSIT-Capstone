/*
 * Date Created: Wednesday, August 25, 2021 5:31 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using Newtonsoft.Json;
using TMPro;

namespace Ph.CoDe_A.Lakbay.Utilities {
    public static class Extension {
        public static T Pop<T>(this IList<T> list, int index=0) {
            var item = list[index];
            list.RemoveAt(index);
            return item;

        }

        public static T PopRandomly<T>(this IList<T> list) {
            var index = Enumerable.Range(0, list.Count).PickRandomly();
            return list.Pop(index);

        }

        public static T1 Pop<T0, T1>(
            this IDictionary<T0, T1> dictionary, T0 key
        ) {
            var value = dictionary[key];
            dictionary.Remove(key);
            return value;

        }

        public static T1 PopRandomly<T0, T1>(
            this IDictionary<T0, T1> dictionary
        ) {
            var key = dictionary.Keys.PickRandomly();
            return dictionary.Pop(key);

        }

        public static T1 Pop<T0, T1>(
            this IDictionary<T0, T1> dictionary, T0 key, T1 fallbackValue
        ) {
            if(!dictionary.ContainsKey(key)) return fallbackValue;
            return dictionary.Pop(key);

        }

        public static T1 Get<T0, T1>(
            this IDictionary<T0, T1> dictionary, T0 key
        ) {
            return dictionary[key];

        }

        public static T1 Get<T0, T1>(
            this IDictionary<T0, T1> dictionary, T0 key, T1 fallbackValue
        ) {
            if(!dictionary.ContainsKey(key)) return fallbackValue;
            return dictionary.Get(key);

        }

        public static Dictionary<T0, T1> ToDictionary<T0, T1>(
            this IEnumerable<KeyValuePair<T0, T1>> enumerable
        ) {
            var dictionary = new Dictionary<T0, T1>();
            foreach(var pair in enumerable) dictionary.Add(pair.Key, pair.Value);
            return dictionary;

        }

        public static Dictionary<T0, T1> ToDictionary<T0, T1>(
            this IEnumerable<Tuple<T0, T1>> enumerable
        ) {
            var dictionary = new Dictionary<T0, T1>();
            foreach(var pair in enumerable) dictionary.Add(pair.Item1, pair.Item2);
            return dictionary;

        }

        public static bool IsInstance<T>(this T obj, params Type[] types) {
            foreach(var type in types) {
                if(obj.GetType().IsSubclassOf(type) || obj.GetType() == type) return true;

            }

            return false;

        }

        public static string Join<T>(
            this IEnumerable<T> enumerable, string separator
        ) {
            return string.Join(separator, enumerable);

        }

        public static string Join<T>(
            this string separator, IEnumerable<T> enumerable
        ) {
            return enumerable.Join(separator);

        }

        public static Vector3 ToVector3(this IEnumerable<float> enumerable) {
            var array = enumerable.ToArray();
            return new Vector3(array[0], array[1], array[2]);

        }

        public static Vector3Int ToVector3Int(this IEnumerable<int> enumerable) {
            var array = enumerable.ToArray();
            return new Vector3Int(array[0], array[1], array[2]);

        }

        public static Vector2 ToVector2(this IEnumerable<float> enumerable) {
            var array = enumerable.ToArray();
            return new Vector2(array[0], array[1]);

        }

        public static Vector2Int ToVector2Int(this IEnumerable<int> enumerable) {
            var array = enumerable.ToArray();
            return new Vector2Int(array[0], array[1]);

        }

        public static bool Within(this float value, float min, float max) {
            return value >= min && value <= max;

        }

        public static bool Within(this int value, int min, int max) {
            return ((float) value).Within(min, max);

        }

        public static bool Between(this float value, float min, float max) {
            return value > min && value < max;

        }

        public static bool Between(this int value, int min, int max) {
            return ((float) value).Between(min, max);

        }

        public static bool Either<T>(this T obj, params T[] values) {
            foreach(var value in values) {
                if(obj.Equals(value)) return true;

            }

            return false;

        }

        public static string ToJson<T>(this T obj) {
            return JsonConvert.SerializeObject(obj);

        }

        public static T DeserializeAsJson<T>(this string json) {
            return JsonConvert.DeserializeObject<T>(json);

        }

        public static string ToYaml<T>(this T obj) {
            return Helper.YamlSerializer.Serialize(obj);

        }
        
        public static T DeserializeAsYaml<T>(this string yaml) {
            return Helper.YamlDeserializer.Deserialize<T>(yaml);

        }

        public static void DestroyChild(
            this GameObject gameObject, int index, float time=0.0f
        ) {
            GameObject.Destroy(gameObject.transform.GetChild(index).gameObject, time);

        }

        public static void DestroyChildren(
            this GameObject gameObject, float time=0.0f
        ) {
            var children = gameObject.GetChildren();
            foreach(var child in children.Enumerate()) {
                gameObject.DestroyChild(child.Key, time);

            }

        }

        public static GameObject[] GetChildren(this GameObject gameObject) {
            var transform = gameObject.transform;
            var childCount = transform.childCount;
            var range = Enumerable.Range(0, childCount);
            return (
                from i in range
                select transform.GetChild(i).gameObject
            ).ToArray();

        }

        public static Color ToColor(this string str) {
            var color = Color.white;
            ColorUtility.TryParseHtmlString(str, out color);
            return color;

        }

        public static Color32 ToColor32(this IEnumerable<byte> enumerable) {
            var color = (Color32) Color.white;
            foreach(var i in enumerable.Enumerate()) color[i.Key] = i.Value;
            return color;
            
        }

        public static Color ToColor(this IEnumerable<float> enumerable) {
            var color = Color.white;
            foreach(var i in enumerable.Enumerate()) color[i.Key] = i.Value;
            return color;
            
        }

        public static IEnumerable<KeyValuePair<int, T>> Enumerate<T>(
            this IEnumerable<T> enumerable
        ) {
            int i = 0;
            var list = new List<KeyValuePair<int, T>>();
            foreach(var item in enumerable)
                list.Add(new KeyValuePair<int, T>(i++, item));

            return list;

        }

        public static T PickRandomly<T>(this IEnumerable<T> enumerable) {
            var array = enumerable.ToArray();
            if(array.Length == 0) return default;
            return array[UnityEngine.Random.Range(0, array.Length)];

        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable) {
            var list = enumerable.ToList();
            var newList = new List<T>();

            while(list.Count > 0) {
                var popped = list.PopRandomly();
                newList.Add(popped);

            }

            return newList;

        }

        public static string CopyToClipboard(this string str) {
            GUIUtility.systemCopyBuffer = str;
            return str;

        }

        public static void AddUnique<T>(this IList<T> list, T item) {
            if(!list.Contains(item)) list.Add(item);

        }

        public static void RemoveAll<T>(this IList<T> list, T item) {
            while(list.Contains(item)) list.Remove(item);

        }

        public static Coroutine DoFor(
            this MonoBehaviour mono,
            float duration,
            Func<float, float, float, float> onRun,
            Action onStart=null,
            Action onStop=null,
            bool fixedUpdate=false,
            Func<float> deltaTime=null,
            Func<float> fixedDeltaTime=null
        ) {
            return mono.StartCoroutine(
                Helper.DoFor(
                    duration, onRun, onStart, onStop, fixedUpdate,
                    deltaTime, fixedDeltaTime
                )
            );

        }

        public static Coroutine DoAfter(
            this MonoBehaviour mono,
            float duration,
            Action onRun,
            Action onStart=null,
            bool fixedUpdate=false,
            Func<float> deltaTime=null,
            Func<float> fixedDeltaTime=null
        ) {
            return mono.StartCoroutine(
                Helper.DoAfter(
                    duration, onRun, onStart,
                    fixedUpdate, deltaTime, fixedDeltaTime
                )
            );

        }

        public static GameObject CreateChild(
            this GameObject parent,
            GameObject child,
            params Type[] componentTypes
        ) {
            child.transform.SetParent(parent.transform);
            foreach(var c in componentTypes) child.AddComponent(c);
            return child;

        }
        
        public static GameObject CreateChild(
            this GameObject parent,
            params Type[] components
        ) {
            return parent.CreateChild(new GameObject(), components);

        }

        public static Vector3 GetSize(this GameObject gameObject) {
            var sizes = gameObject.GetComponentsInChildren<Renderer>()
                .Select((r) => r.bounds.size).ToList();
            foreach(var t in gameObject.GetComponentsInChildren<Terrain>()) {
                sizes.Add(t.terrainData.bounds.size);

            }

            if(sizes.Count > 0) {
                var maxes = Enumerable.Range(0, 3).Select(
                    (i) => sizes.Max((s) => s[i])
                );

                return maxes.ToVector3();

            }

            return Vector3.zero;

        }

        public static Bounds GetBounds(this GameObject gameObject) {
            return new Bounds(gameObject.transform.position, gameObject.GetSize());

        }

        // Source: http://wiki.unity3d.com/index.php?title=IsVisibleFrom#UnityScript_-_RendererHelpers.js
        public static bool IsVisibleFrom(
            this Bounds bounds, UnityEngine.Camera camera
        ) {
            var planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, bounds);

        }

        // Source: http://answers.unity.com/answers/1031557/view.html
        public static bool IsVisibleFrom(
            this Vector3 position, UnityEngine.Camera camera
        ) {
            var screenPoint = camera.WorldToViewportPoint(position);
            return screenPoint.z > 0 &&
                screenPoint.x.Between(0, 1) &&
                screenPoint.y.Between(0, 1);

        }

        public static bool IsBoundsVisible(this GameObject gameObject) {
            return gameObject.GetBounds().IsVisibleFrom(UnityEngine.Camera.main);

        }

        public static void FillWith<T>(this IList<T> list, T obj, int count) {
            for(int i = 0; i < count; i++) list.Add(obj);

        }

        
        public static void Move(this Rigidbody rigidbody, Vector3 position) {
            rigidbody.MovePosition(position);

        }

        public static void Move(this Transform transform, Vector3 position) {
            transform.Translate(position - transform.position);

        }

        public static void OffsetPosition(this Rigidbody rigidbody, Vector3 position) {
            rigidbody.Move(rigidbody.position + position);

        }

        public static void OffsetPosition(this Transform transform, Vector3 position) {
            transform.Move(transform.position + position);

        }

        public static Vector3 GetAxes(this Vector3 vector3, bool x=true, bool y=true, bool z=true) {
            var vec = Vector3.zero;
            for(int i = 0; i < 3; i++) vec[i] = x ? vector3.x : vec[i];
            return vec;

        }

        public static void Pose(
            this Transform transform,
            WheelCollider wheelCollider,
            Vector3 offsetPosition,
            Quaternion offsetRotation
        ) {
            wheelCollider.GetWorldPose(out var pos, out var quat);
            transform.position = pos + offsetPosition;
            transform.rotation = quat * offsetRotation;

        }

        public static void Pose(
            this Transform transform,
            WheelCollider wheelCollider,
            Vector3 offsetPosition
        ) {
            transform.Pose(wheelCollider, offsetPosition, Quaternion.identity);

        }

        public static void Pose(
            this Transform transform,
            WheelCollider wheelCollider,
            Quaternion offsetRotation
        ) {
            transform.Pose(wheelCollider, Vector3.zero, offsetRotation);

        }

        public static void Pose(
            this Transform transform,
            WheelCollider wheelCollider
        ) {
            transform.Pose(wheelCollider, Vector3.zero, Quaternion.identity);

        }

        public static void Break(this Rigidbody rigidbody, Action<Rigidbody> action=null) {
            var cols = rigidbody.GetComponentsInChildren<Collider>();
            if(cols.Length > 0) {
                float mass = rigidbody.mass / cols.Length;
                float drag = rigidbody.drag / cols.Length;
                foreach(var c in cols) {
                    var r = c.gameObject.GetComponent<Rigidbody>();
                    if(!r) r = c.gameObject.AddComponent<Rigidbody>();
                    r.mass = mass;
                    r.drag = drag;

                    if(action != null) action(r);

                }

            }

        }

        public static T Find<T>(this T[] array, Predicate<T> predicate) {
            return Array.Find(array, predicate);

        }

        public static T[] FindAll<T>(this T[] array, Predicate<T> predicate) {
            return Array.FindAll(array, predicate);

        }

        public static bool Have<T>(this IEnumerable<T> enumerable, int index) {
            return index.Within(0, enumerable.Count());

        }

        // Source: https://stackoverflow.com/a/2107864/14733693
        public static MethodInfo GetGenericMethod<T>(
            this T obj, string name,
            Type[] parameterTypes
        ) {
            var method = obj.GetType().GetMethod(name);
            return method.MakeGenericMethod(parameterTypes);

        }
        
        public static object InvokeGenericMethod<T>(
            this T obj, string name,
            Type[] parameterTypes,
            params object[] parameters
        ) {
            return obj.GetGenericMethod(name, parameterTypes).Invoke(obj, parameters);
        }

    } 

}