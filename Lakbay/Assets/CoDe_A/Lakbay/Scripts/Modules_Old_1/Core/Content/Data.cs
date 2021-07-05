/*
 * Date Created: Sunday, July 4, 2021 9:04 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld1.Core.Content {
    using EntryList = List<Entry.Data>;

    public interface IData : Core.IData {
        EntryList entries { get; set; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        protected EntryList _entries;
        public virtual EntryList entries { get => _entries; set => _entries = value; }

        public Data() : this(default(EntryList)) {}

        public Data(
            EntryList entries=null,
            Core.IData data=null
        ) : base(data ?? new Core.Data()) {
            this.entries = entries ?? new EntryList();

        }

        public Data(IData data) : this(
            data.entries,
            data
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<IData>()) {}

    }

}