/*
 * Date Created: Tuesday, June 29, 2021 7:36 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;

using Code_A_Old_2.Lakbay.Utilities;
using YamlDotNet.Serialization;

namespace Code_A_Old_2.Lakbay.Modules.Core.Content {
    public interface IData : Core.IData {
        List<Entry.Data> entries { get; set; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [NonSerialized, YamlIgnore]
        public new Controller controller;

        [SerializeField]
        private List<Entry.Data> m_entries;
        protected List<Entry.Data> _entries;
        public virtual List<Entry.Data> entries {
            get => _entries;
            set {
                if(value == entries) return;
                _entries = m_entries = value;

            }

        }


        public Data() : this(default(List<Entry.Data>)) {}

        public Data(
            List<Entry.Data> entries=null,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.entries = entries ?? new List<Entry.Data>();

        }

        public Data(Data data) : this(
            data.entries,
            data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            entries = m_entries;
            foreach(var e in entries) e.OnInspectorHasUpdate();

        }

    }

}