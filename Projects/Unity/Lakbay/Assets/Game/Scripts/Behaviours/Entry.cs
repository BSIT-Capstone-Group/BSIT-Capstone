/*
 * Date Created: Thursday, September 9, 2021 7:33 AM
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
    [Serializable]
    public struct Entry {
        public enum Type {
            Text = 1,
            Asset = 2,
            Image = Asset | 4,
            Audio = Asset | 8,
            Video = Asset | 16,
            Document = Asset | 32

        }

        [SerializeField]
        private Type _type;
        public Type type { get => _type; set => _type = value; }
        [SerializeField]
        private List<string> _items;
        public List<string> items { get => _items; set => _items = value; }

        public Entry(Type type=default, List<string> items=null) {
            _type = type;
            _items = items ?? new List<string>();

        }

    }

}