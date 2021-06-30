/*
 * Date Created: Tuesday, June 29, 2021 10:15 PM
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

namespace Code_A.Lakbay.Modules.Core.Content.Entry {
    public enum Type { Text, Image }

    public interface IData : Core.IData {
        Type type { get; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        public string text;
        public List<Image.Data> images;

        public Type type => (text != null && text.Length != 0) ? Type.Text : Type.Image;


        public Data() : this(" ") {}

        public Data(
            string text=" ",
            List<Image.Data> images=null,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.text = text;
            this.images = images ?? new List<Image.Data>();

        }

        public Data(Data data) : this(
            data.text,
            data.images,
            data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

    }

}