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

namespace Ph.CoDe_A.Lakbay.Core {
    public class Container : Controller {
        public GameObject root;

        public override void Awake() {
            base.Awake();
            if(!root) root = gameObject;

        }

        public virtual void Contain(
            IEnumerable<GameObject> gameObjects,
            Action<GameObject> onDisplay=null
        ) {
            foreach(var go in gameObjects) {
                Contain(go, onDisplay);

            }

        }

        public virtual void Contain(
            GameObject gameObject, 
            Action<GameObject> onDisplay=null
        ) {
            gameObject.transform.SetParent(root.transform);
            onDisplay?.Invoke(gameObject);

        }

        public virtual void Contain<T>(
            T component, 
            Action<T> onDisplay=null
        ) where T : Component {
            Contain(component.gameObject, (g) => onDisplay?.Invoke(component));

        }

        public virtual void Contain<T>(
            IEnumerable<T> components,
            Action<T> onDisplay=null
        ) where T : Component {
            foreach(var component in components) Contain(component, onDisplay);

        }

        public virtual void Clear() {
            root.DestroyChildren();

        }

    }

}