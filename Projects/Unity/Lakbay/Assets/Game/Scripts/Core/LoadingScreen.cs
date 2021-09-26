/*
 * Date Created: Thursday, September 9, 2021 2:43 PM
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

using TMPro;

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    public class LoadingScreen : Controller {
        public int sceneBuildIndex = 1;
        public TextMeshProUGUI information;
        public TextMeshProUGUI progress;
        public Slider slider;

        protected CanvasGroup _canvasGroup;
        public virtual CanvasGroup canvasGroup => _canvasGroup;

        public override void Awake() {
            base.Awake();
            if(!Game.initialized) DontDestroyOnLoad(gameObject);

            _canvasGroup = gameObject.EnsureComponent<CanvasGroup>();
            Hide();

        }

        public void SetInformation(string text) {
            information?.SetText(text);

        }

        protected void _SetProgress(string text) {
            progress?.SetText(text);

        }

        public void SetSlider(float progress) {
            if(slider) slider.value = progress;

        }

        public void SetProgress(float progress) {
            _SetProgress(progress.ToString("000%"));
            SetSlider(progress);

        }

        public void Show() {
            canvasGroup.alpha = 1.0f;
            canvasGroup.blocksRaycasts = true;

        }

        public void Hide() {
            canvasGroup.alpha = 0.0f;
            canvasGroup.blocksRaycasts = false;

        }

    }

}