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
            Text = 0,
            Asset = 1,
            Image = Asset | 2,
            Audio = Asset | 4,
            Video = Asset | 8,
            Document = Asset | 16

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

        public override string ToString() {
            return items.Join("\n");

        }

    }

}