using System;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;
using YamlDotNet.Serialization;
using Force.DeepCloner;
using CloneExtensions;

namespace CoDe_A.Lakbay.Utilities {
    public static class Helper {
        public static readonly float READING_CHARACTER_PER_SECOND = 25.0f;

        public static int getIndexOfChild(Transform parent, Transform child) {
            int index = -1;

            for(int i = 0; i < parent.childCount; i++) {
                if(parent.GetChild(i) == child) {
                    index = i;
                    break;

                }

            }

            return index;

        }

        public static Transform[] getChildren(Transform parent) {
            List<Transform> children = new List<Transform>();

            for(int i = 0; i < parent.childCount; i++) {
                children.Add(parent.GetChild(i));

            }

            return children.ToArray();

        }

        public static GameObject[] getChildren(GameObject parent) {
            return getChildren(parent.transform).Select<Transform, GameObject>(
                (t) => t.gameObject
            ).ToArray();
        }

        public static void destroyChildren(Transform parent) {
            for(int i = 0; i < parent.childCount; i++) {
                GameObject.Destroy(parent.GetChild(i).gameObject);

            }

        }

        public static float getSum(params float[] numbers) {
            float sum = 0.0f;
            foreach(float number in numbers) sum += number;

            return sum;

        }

        public static float getProduct(params float[] numbers) {
            float sum = 0.0f;
            foreach(float number in numbers) sum *= number;

            return sum;

        }

        // Source: https://stackoverflow.com/a/110570/14733693
        public static T[] shuffle<T>(System.Random random, T[] array) {
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

        public static T[] shuffle<T>(T[] array) {
            System.Random random = new System.Random();

            return Helper.shuffle<T>(random, array);

        }

        public static T pickRandom<T>(System.Random random, T[] array) {
            int index = random.Next(array.Length);
            return array[index];

        }

        public static T pickRandom<T>(T[] array) {
            return pickRandom<T>(new System.Random(), array);

        }

        public static T[] reverse<T>(T[] array) {
            List<T> list = new List<T>(array);
            list.Reverse();

            return list.ToArray();

        }

        public static T parseJSON<T>(string str) {
            return JsonConvert.DeserializeObject<T>(str);

        }

        public static T parseYAML<T>(string str) {
            return new Deserializer().Deserialize<T>(str);

        }
        
        public static String asPrettyString(OrderedDictionary keyAndValues, String separator) {
            List<String> strs = new List<String>();
            foreach(DictionaryEntry de in keyAndValues) {
                strs.Add($"{de.Key}: {de.Value}");

            }

            return String.Join(separator, strs);

        }

        public static String asPrettyString(OrderedDictionary keyAndValues) {
            return asPrettyString(keyAndValues, ", ");

        }

        public static void log(OrderedDictionary keyAndValues, String separator) {
            Debug.Log(asPrettyString(keyAndValues, separator));

        }

        public static void log(String label, OrderedDictionary keyAndValues) {
            Debug.Log($"[{label}]:\t" + asPrettyString(keyAndValues, ", "));

        }

        public static void log(OrderedDictionary keyAndValues) => log(keyAndValues, ", ");

        public static void copyToClipboard(String str) => GUIUtility.systemCopyBuffer = str;
        
        public static void clearClipboard() => GUIUtility.systemCopyBuffer = "";

        public static T clone<T>(T obj, bool deep) {
            return deep ? obj.DeepClone<T>() : obj.ShallowClone<T>();

        }

        public static T clone<T>(T obj) => clone(obj, false);
        
    }

}