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

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class LoadingScreen : Controller {
        public TextMeshProUGUI asset;
        public TextMeshProUGUI progress;
        public Slider slider;

        public virtual CanvasGroup canvasGroup => GetComponent<CanvasGroup>();

        public override void Awake() {
            base.Awake();
            if(!Game.initialized) DontDestroyOnLoad(gameObject);

            if(!GetComponent<CanvasGroup>()) gameObject.AddComponent<CanvasGroup>();
            Hide();

        }

        public void SetAsset(string text) {
            asset?.SetText(text);

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