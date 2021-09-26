/*
 * Date Created: Wednesday, September 22, 2021 8:43 AM
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
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Utilities {
    // Source: http://wiki.unity3d.com/index.php?title=FramesPerSecond
    public class FPSDisplay : MonoBehaviour {
        protected float _deltaTime = 0.0f;
    
        protected virtual void Update() {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

        }
    
        protected virtual void OnGUI() {
            if(!Debug.isDebugBuild) return;

            int w = Screen.width, h = Screen.height;
    
            GUIStyle style = new GUIStyle();
    
            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 3 / 100;
            style.fontStyle = FontStyle.Bold;
            style.normal.textColor = Color.white;
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);

        }
    }

}