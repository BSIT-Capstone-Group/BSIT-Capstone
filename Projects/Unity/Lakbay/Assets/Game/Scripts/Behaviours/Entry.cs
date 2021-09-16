/*
 * Date Created: Thursday, September 16, 2021 8:19 PM
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
    public class Entry : Controller {
        public enum Type {
            Text = 0,
            Asset = 1,
            Image = Asset | 2,
            Audio = Asset | 4,
            Video = Asset | 8,
            Document = Asset | 16

        }

        public static KeyValuePair<Type, string> Parse(string entry) {
            var parts = entry.Split(':').ToList();
            Type type = default;

            if(parts.Count > 1) Enum.TryParse(parts.Pop(0).Trim(), out type);
            return new KeyValuePair<Type, string>(type, parts.Join(":"));

        }

        public static void Parse(string entry, out Type type, out string value) {
            var kvp = Parse(entry);
            type = kvp.Key;
            value = kvp.Value;

        }

    }

    public class Entry<T> : Entry {
        public T component;

    }

}