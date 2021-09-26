/*
 * Date Created: Friday, September 24, 2021 6:29 AM
 * Author: NI.L.A
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

using TMPro;
using YamlDotNet.Serialization;

using Ph.CoDe_A.Lakbay.Core;
using Ph.CoDe_A.Lakbay.Utilities;

namespace Ph.CoDe_A.Lakbay.Core {
    public class Store : Controller {
        protected readonly OrderedDictionary _store = new OrderedDictionary();

        public virtual object this[string key] {
            get => Get(key);
            set => Set(key, value);

        }

        public virtual void Set(string key, object value) {
            _store[key] = value;

        }

        public virtual object Get(string key) {
            return Contains(key) ? _store[key] : null;

        }

        public virtual T Get<T>(string key) => (T) Get(key);

        public virtual void Unset(string key) {
            if(Contains(key)) _store.Remove(key);

        }

        public virtual bool Contains(string key) => _store.Contains(key);

        public virtual void Clear() {
            foreach(var key in _store.Keys) Unset(key as string);

        }

    }

}