using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
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

}
