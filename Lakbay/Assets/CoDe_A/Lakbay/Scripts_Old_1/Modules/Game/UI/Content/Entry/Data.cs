/*
 * Date Created: Tuesday, June 29, 2021 10:49 AM
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

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.UI.Content.Entry {
    public interface IData : UI.IData {
        bool isImage { get; }

    }

    [Serializable]
    public class Data : UI.Data, IData {
        [Serializable]
        public struct Image {
            public string path;
            public string description;

        }

        public string text;
        public List<Image> images;
        public bool isImage => (images != null && images.Count != 0);


        public Data() : this(" ") {}

        public Data(string text=" ", List<Image> images=null, UI.Data data=null) : base((UI.Data) data ?? new UI.Data()) {
            this.text = text;
            this.images = images;

        }

        public Data(Data data) : this(
            data.text,
            data.images,
            (UI.Data) data
        ) {}

        public Data(Controller controller) : this(
            controller.text,
            controller.images,
            (controller as UI.IController).data
        ) {}

    }

}