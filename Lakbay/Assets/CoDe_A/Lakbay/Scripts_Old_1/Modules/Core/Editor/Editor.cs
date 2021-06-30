/*
 * Date Created: Saturday, June 26, 2021 6:28 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System.ComponentModel.Design;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Core {
    public static class Editor {
        // Source: https://answers.unity.com/questions/1654306/how-to-get-path-from-the-current-opened-folder-in.html
        public static string GetActiveFolderPath() {
            Type projectWindowUtilType = typeof(ProjectWindowUtil);
            MethodInfo getActiveFolderPath = projectWindowUtilType.GetMethod(
                "GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic
            );
            object obj = getActiveFolderPath.Invoke(null, new object[0]);
            string pathToCurrentFolder = obj.ToString();

            return pathToCurrentFolder;
        
        }

        // Source: https://stackoverflow.com/a/66168086/14733693
        public static string CreateAsset(UnityEngine.Object asset, string name) {
            string path = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(
                GetActiveFolderPath() + $"/{name}"
            );
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();

            Selection.activeObject = asset;
            Utilities.Helper.Log(typeof(Editor).Name, $"Successfully created asset at '{path}'.");

            return path;

        }

        public static string CreateInstance<T>(string name) where T : ScriptableObject {
            var asset = ScriptableObject.CreateInstance<T>();
            return CreateAsset(asset, name);

        }

        [MenuItem("Assets/Create/CoDe_A/Lakbay/Scriptable Objects/Linear Play Player")]
        public static void CreateInstanceOfLinearPlayPlayer() {
            // CreateInstance<Game.LinearPlay.Player.Data>("Linear Play Player.asset");

        }
        
        [MenuItem("Assets/Create/CoDe_A/Lakbay/Scriptable Objects/Free-Roam Play Player")]
        public static void CreateInstanceOfFreeRoamPlayPlayer() {
            // CreateInstance<Game.LinearPlay.Player.Data>("Free-Roam Play Player.asset");

        }
        
        [MenuItem("Assets/Create/CoDe_A/Lakbay/Scriptable Objects/Linear Play Lane")]
        public static void CreateInstanceOfLinearPlayLane() {
            // CreateInstance<Game.LinearPlay.Lane.Data>("Linear Play Lane.asset");

        }

    }

}
