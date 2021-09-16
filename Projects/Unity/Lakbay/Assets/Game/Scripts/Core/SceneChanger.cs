/*
 * Date Created: Friday, September 10, 2021 8:49 AM
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
using UnityEngine.SceneManagement;

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    public class SceneChanger : Controller {
        public TMP_Dropdown dropdown;
        protected List<Scene> scenes;

        public override void Awake() {
            base.Awake();
            if(!Game.initialized) DontDestroyOnLoad(gameObject);

            scenes = Helper.GetAllScenes().ToList();

            var sceneNames = scenes.Select((s) => s.path).ToList();
            dropdown.AddOptions(sceneNames);
            dropdown.onValueChanged.AddListener((i) => {
                SceneManager.LoadScene(i);
                print("controllers...", Controller._instances.Count);

            });

        }

        public override void Update() {
            base.Update();

        }

    }

}