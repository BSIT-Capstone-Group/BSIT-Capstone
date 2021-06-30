/*
 * Date Created: Wednesday, June 30, 2021 9:52 AM
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

namespace Code_A.Lakbay.Modules.Core.Interactable.GameObject {
    public interface IData : Interactable.IData {


    }

    [Serializable]
    public class Data : Interactable.Data, IData {
        public Data() : this(default(Interactable.Data)) {}

        public Data(
            Interactable.Data data=null
        ) : base(data ?? new Interactable.Data()) {
            

        }

        public Data(Data data) : this(
            (Interactable.Data) data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

    }

}