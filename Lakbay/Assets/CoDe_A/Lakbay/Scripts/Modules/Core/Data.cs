/*
 * Date Created: Tuesday, June 29, 2021 6:49 PM
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

namespace Code_A.Lakbay.Modules.Core {
    public interface IData {


    }

    [Serializable]
    public class Data : IData {
        public string label;
        public string description;


        public Data() : this("") {}

        public Data(
            string label="",
            string description=""
        ) : base() {
            this.label = label;
            this.description = description;

        }

        public Data(Data data) : this(
            data.label,
            data.description
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

    }

}