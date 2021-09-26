/*
 * Date Created: Sunday, September 26, 2021 6:46 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Localization;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    public static class Helper {
        public const string GameAssetsPath = "Assets/Game/Assets";
        public const string LocalizationAssetsPath = GameAssetsPath + "/Localization";
        public const string BuildPath = "Build";

        public static List<string> GetAssetPaths<T>(params string[] excludedExtensions) {
            var assetType = typeof(T);
            var gassets = AssetDatabase.FindAssets(
                $"t:{assetType.Name}", new string[] {GameAssetsPath});
            var assetPaths = gassets.Select((g) => AssetDatabase.GUIDToAssetPath(g))
                .Where((p) => !p.EndsWith(excludedExtensions)).ToList();

            return assetPaths;

        }

        [MenuItem("Game/Addressables/Normalize Addressable Addresses")]
        public static void NormalizeAddressableAddresses() {
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            foreach(var group in settings.groups) {
                foreach(var entry in group.entries) {
                    entry.SetAddress(entry.AssetPath);

                }

            }

        }

        [MenuItem("Game/Addressables/Mark Assets as Addressables")]
        public static void MarkAssetsAsAddressables() {
            _MarkAssetsAsAddressables<TextAsset>(".cs");
            _MarkAssetsAsAddressables<Sprite>();
            _MarkAssetsAsAddressables<Texture>();
            _MarkAssetsAsAddressables<AudioClip>();

        }

        private static void _MarkAssetsAsAddressables<T>(params string[] excludedExtensions)
            where T : UnityEngine.Object {
            var assetType = typeof(T);
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            if(!settings) return;
            var assetPaths = GetAssetPaths<T>(excludedExtensions);

            var group = settings.FindGroup(assetType.Name);

            var entries = new List<AddressableAssetEntry>();
            foreach(var path in assetPaths) {
                var asset = AssetDatabase.LoadAssetAtPath(path, assetType) as T;
                if(!group)
                    group = settings.CreateGroup(
                        assetType.Name, false, false, true, null,
                        new Type[] {typeof(ContentUpdateGroupSchema)}
                    );

                EditorUtility.DisplayProgressBar(
                    "Mark Assets as Addressables",
                    $"Marking Assets as Addressables ({assetType.Name})...",
                    (assetPaths.IndexOf(path) + 1) / assetPaths.Count
                );
                var entry = settings.CreateOrMoveEntry(
                    AssetDatabase.AssetPathToGUID(path), group);
                entries.Add(entry);

                if(!settings.GetLabels().Contains(assetType.Name))
                    settings.AddLabel(assetType.Name);

                entry.labels.Add(assetType.Name);

            }

            NormalizeAddressableAddresses();
            EditorUtility.ClearProgressBar();

        }

        [MenuItem("Game/Localization/Localize Assets")]
        public static void LocalizeAssets() {
            _LocalizeAssets<TextAsset>(".cs");
            _LocalizeAssets<Sprite>();
            _LocalizeAssets<Texture>();
            _LocalizeAssets<AudioClip>();

        }

        [MenuItem("Game/Build/Release")]
        private static void BuildRelease() {
            _Build();

        }

        [MenuItem("Game/Build/Development")]
        private static void BuildDevelopment() {
            _Build(true);

        }

        private static void _Build(bool development=false) {
            AddressableAssetSettings.BuildPlayerContent();

            var now = DateTime.Now;
            PlayerSettings.bundleVersion = $"{now.Year:0000}.{now.Month:00}.{now.Day:00}";
            string name = PlayerSettings.productName, version = PlayerSettings.bundleVersion;
            string folder = BuildPath + "/" + (development ? "Development" : "Release");

            var buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select((s) => s.path).ToArray();
            foreach(var scene in buildPlayerOptions.scenes) Debug.Log(scene);
            buildPlayerOptions.locationPathName = $"{folder}/{name.ToLower()}-v{version}.apk";
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options |= BuildOptions.AutoRunPlayer;
            if(development) buildPlayerOptions.options |= BuildOptions.Development;

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            var summary = report.summary;

            if (summary.result == BuildResult.Succeeded) {
                Debug.Log(@$"Build succeeded! OutputPath: '{summary.outputPath}', OutputSize: {summary.totalSize} bytes.");
            
            } else if (summary.result == BuildResult.Failed) {
                Debug.Log($"Build failed!");

            }

        }

        private static void _LocalizeAssets<T>(params string[] excludedExtensions) where T : UnityEngine.Object {
            var assetType = typeof(T);
            var atc = LocalizationEditorSettings.GetAssetTableCollection(assetType.Name);
            var assetPaths = GetAssetPaths<T>(excludedExtensions);

            foreach(var path in assetPaths) {
                var asset = AssetDatabase.LoadAssetAtPath(path, assetType) as T;
                var paths = path.Split('/').ToList();
                paths.Pop(paths.Count - 1);
                var locales = LocalizationEditorSettings.GetLocales().ToArray();

                if(asset && Utilities.Helper.TrimLocaleCode(asset, locales, out string name, out string code)) {
                    if(!atc)
                        atc = LocalizationEditorSettings.CreateAssetTableCollection(
                            assetType.Name, LocalizationAssetsPath + "/Tables"
                        );

                    EditorUtility.DisplayProgressBar(
                        "Localize Assets",
                        $"Localizing Assets ({assetType.Name})...",
                        (assetPaths.IndexOf(path) + 1) / assetPaths.Count
                    );

                    paths.Add(name);
                    string key = paths.Join("/");
                    atc.AddAssetToTable(
                        new LocaleIdentifier(code),
                        key,
                        asset);

                }


            }

            NormalizeAddressableAddresses();
            EditorUtility.ClearProgressBar();

        }

    }

}