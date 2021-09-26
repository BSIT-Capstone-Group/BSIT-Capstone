/*
 * Date Created: Friday, September 24, 2021 7:45 AM
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
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.LinearPlay {
    public enum TimeOfDay {
        Morning, Afternoon, Evening, Night

    }

    [Serializable]
    public struct Environment {
        public TimeOfDay timeOfDay; 

    }

    [Serializable]
    public struct Level {
        public Environment environment;
        public List<Question> questions;
        [YamlIgnore]
        public int score => questions.Where((q) => q.correct).Count();

    }

    public class LinearPlayInitialization : Controller {
        [SerializeField]
        protected List<TextAsset> _levels = new List<TextAsset>();
        public List<Level> levels = new List<Level>();
        
        public override void Awake() {
            base.Awake();
            
            Debug.Log(Utilities.Helper.GetLocales().Length);

            if(!Game.store.Contains("lpInitialized"))
                Game.store["lpInitialized"] = false;

            if(!Game.store.Get<bool>("lpInitialized")) {
                var levels = new List<Level>();
                Level level;
                foreach (var level_ in _levels) {
                    level = level_.DeserializeAsYaml<Level>();
                    levels.Add(level);

                }
                levels.AddRange(this.levels);
                level = levels.First();

                Game.store["lpLevels"] = levels;
                Game.store["lpLevel"] = level;

                Game.store["lpInitialized"] = true;

                var config = new Configuration();
                print(config.accessibility.language);
                print(config.accessibility.languageLocale);

            }

        }

        // [ContextMenu("Localize")]
        public override void Localize() {
            base.Localize();
            // foreach(var level in _levels) {
            //     var @event = gameObject.AddComponent<LocalizedTextAssetEvent>();
            //     @event.AssetReference = level.GetAssetReference();
            //     UnityEventTools.AddPersistentListener(
            //         @event.OnUpdateAsset,
            //         Test
            //     );

            // }

        }

        public virtual void Test(TextAsset ta) {}

    }

}