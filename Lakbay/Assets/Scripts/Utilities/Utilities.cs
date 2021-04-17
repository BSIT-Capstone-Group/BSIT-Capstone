using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utilities {
    public static class Helper {
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
        public static T[] shuffle<T> (System.Random random, T[] array) {
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

    }

    public class ExtendedMonoBehaviour : MonoBehaviour {

        [HideInInspector]
        public bool paused = false;
        [HideInInspector]
        public float timeScale = 1.0f;

    }

}
