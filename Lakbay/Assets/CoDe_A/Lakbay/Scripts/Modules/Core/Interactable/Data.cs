/*
 * Date Created: Wednesday, June 30, 2021 8:21 AM
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

namespace Code_A.Lakbay.Modules.Core.Interactable {
    public interface IData : Core.IData {


    }

    [Serializable]
    public class Data : Core.Data, IData {
        public bool canDetectInput;
        public bool highlighted;
        public Content.Data tutorialContent;

        public Data() : this(true) {}

        public Data(
            bool canDetectInput=true,
            bool highlighted=false,
            Content.Data tutorialContent=null,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.canDetectInput = canDetectInput;
            this.highlighted = highlighted;
            this.tutorialContent = tutorialContent;

        }

        public Data(Data data) : this(
            data.canDetectInput,
            data.highlighted,
            data.tutorialContent,
            data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

    }

}