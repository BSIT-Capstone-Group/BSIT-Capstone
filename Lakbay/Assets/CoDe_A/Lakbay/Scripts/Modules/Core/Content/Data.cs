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

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Content {
    public interface IData : Core.IData {


    }

    [Serializable]
    public class Data : Core.Data, IData {
        [Serializable]
        public struct Entry {
            public string text;
            public List<Image.Data> images;
            public bool isText => text != null && text.Length > 0;

        }

        
        public List<Entry> entries;


        public Data() : this(default(List<Entry>)) {}

        public Data(
            List<Entry> entries=null,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.entries = entries ?? new List<Entry>();

        }

        public Data(Data data) : this(
            data.entries,
            data
        ) {}

        public Data(IController controller) : this(
            controller.entries,
            controller.data
        ) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

    }

}