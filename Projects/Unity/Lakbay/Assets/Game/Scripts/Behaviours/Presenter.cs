/*
 * Date Created: Thursday, September 16, 2021 4:18 AM
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

using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Behaviours {
    public class Presenter : Controller {
        public GameObject display;

        public override void Awake() {
            base.Awake();
            if(!display) display = gameObject;

        }

        public virtual void Display(
            IEnumerable<GameObject> gameObjects,
            Action<GameObject> onDisplay=null
        ) {
            foreach(var go in gameObjects) {
                Display(go, onDisplay);

            }

        }

        public virtual void Display(
            GameObject gameObject, 
            Action<GameObject> onDisplay=null
        ) {
            gameObject.transform.SetParent(display.transform);
            onDisplay?.Invoke(gameObject);

        }

        public virtual void Display<T>(
            T component, 
            Action<T> onDisplay=null
        ) where T : Component {
            Display(component.gameObject, (g) => onDisplay?.Invoke(component));

        }

        public virtual void Display<T>(
            IEnumerable<T> components,
            Action<T> onDisplay=null
        ) where T : Component {
            foreach(var component in components) Display(component, onDisplay);

        }

        public virtual void Clear() {
            display.DestroyChildren();

        }

    }

}