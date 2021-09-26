/*
 * Date Created: Wednesday, August 25, 2021 12:29 PM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.SceneManagement;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Utilities {
    public static class Helper {
        public static readonly ISerializer YamlSerializer = new SerializerBuilder()
            .EmitDefaults()
            .Build();

        public static readonly IDeserializer YamlDeserializer = new DeserializerBuilder()
            .Build();

        public static IEnumerator DoFor(
            float duration,
            Func<float, float, float, float> onRun,
            Action onStart=null,
            Action onStop=null,
            bool fixedUpdate=false,
            Func<float> deltaTime=null,
            Func<float> fixedDeltaTime=null
        ) {
            float elapsedTime = 0;
            onStart?.Invoke();

            float timeDelta = 0.0f;
            duration = Mathf.Max(0.0f, duration);
            while (elapsedTime < duration) {
                var dt = deltaTime != null ? deltaTime() : Time.deltaTime; 
                var fdt = fixedDeltaTime != null ? fixedDeltaTime() : Time.fixedDeltaTime; 
                timeDelta = !fixedUpdate ? dt : fdt;
                timeDelta = onRun != null ? onRun(timeDelta, elapsedTime, duration) : timeDelta;

                elapsedTime = Mathf.Clamp(elapsedTime + timeDelta, 0, duration);

                if(!fixedUpdate) yield return new WaitForEndOfFrame();
                else yield return new WaitForFixedUpdate();

            }

            onStop?.Invoke();

        }

        public static IEnumerator DoAfter(
            float duration,
            Action onRun,
            Action onStart=null,
            bool fixedUpdate=false,
            Func<float> deltaTime=null,
            Func<float> fixedDeltaTime=null
        ) {
            return DoFor(
                duration,
                null,
                onStart,
                () => onRun(),
                fixedUpdate,
                deltaTime,
                fixedDeltaTime
            );

        }

        public static WaitForSecondsRealtime FixedUpdate() {
            return new WaitForSecondsRealtime(Time.fixedDeltaTime);

        }

        public static Scene[] GetAllScenes() {
            int count = SceneManager.sceneCountInBuildSettings;
            List<Scene> scenes = new List<Scene>();
            for(int i = 0; i < count; i++) {
                scenes.Add(SceneManager.GetSceneByBuildIndex(i));

            }

            return scenes.ToArray();

        }

        public static T ToDelegate<T>(object obj, string propertyName, bool getter) where T : Delegate {
            var pi = obj.GetType().GetProperty(propertyName);
            var mi = getter ? pi.GetMethod : pi.SetMethod;
            return (T) Delegate.CreateDelegate(
                typeof(T), obj, mi
            );

        }

        public static T ToDelegate<T>(object obj, string propertyName) where T : Delegate {
            return ToDelegate<T>(obj, propertyName, false);

        }

        public static Locale[] GetLocales() {
            Locale[] locales;
            locales = LocalizationSettings.AvailableLocales.Locales.ToArray();

            return locales;

        }

        public static string[] GetLocaleCodes() {
            return GetLocaleCodes(GetLocales());

        }

        public static string[] GetLocaleCodes(params Locale[] locales) {
            return locales.Select((l) => l.Identifier.Code).ToArray();

        }

        public static LocalizedAsset<T> GetAssetReference<T>(T asset, Locale[] locales, char delimiter='_')
            where T : UnityEngine.Object {
            LocalizedAsset<T> la = default;
            if(TrimLocaleCode(asset, locales, out string name, out string code, delimiter)) {
                la = new LocalizedAsset<T>();
                la.SetReference(asset.GetType().Name, name);

            }

            return la;

        }

        public static bool TrimLocaleCode<T>(T asset, Locale[] locales, out string name, out string code, char delimiter='_')
            where T : UnityEngine.Object {
            var names = asset.name.Split(delimiter).ToList();
            name = asset.name;
            code = default;
            var localeCodes = GetLocaleCodes(locales);
            if(localeCodes.Contains(names.Last())) {
                code = names.Pop(names.Count - 1);
                name = names.Join(delimiter.ToString());
                return true;

            }

            return false;

        }

    }

}