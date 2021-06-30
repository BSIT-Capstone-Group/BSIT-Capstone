/*
 * Date Created: Tuesday, June 29, 2021 12:46 PM
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

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.UI.Content {
    public interface IData : UI.IData {
        bool isEmpty { get; }

    }

    [Serializable]
    public class Data : UI.Data, IData {
        public List<Entry.Data> entries;
        public bool isEmpty => entries != null && entries.Count != 0;

        public Data() : this(default(List<Entry.Data>)) {}

        public Data(
            List<Entry.Data> entries=null,
            UI.Data data=null
        ) : base((UI.Data) (data ?? new UI.Data())) {
            this.entries = entries ?? new List<Entry.Data>();

        }

        public Data(Data data) : this(
            data.entries,
            (UI.Data) data
        ) {}

        public Data(Controller controller) : this(
            controller.entries,
            (controller as UI.IController).data
        ) {}

    }

}