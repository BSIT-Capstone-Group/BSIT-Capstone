/*
 * Date Created: Tuesday, June 29, 2021 9:40 PM
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

namespace Code_A.Lakbay.Modules.Core.Image {
    public interface IData : Core.IData {
        Sprite asSprite { get; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        public Sprite sprite;
        public string path;

        public Sprite asSprite => sprite ?? null;

        public Data() : this(default(Sprite)) {}

        public Data(
            Sprite sprite=default,
            string path="",
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.sprite = sprite;
            this.path = path;

        }

        public Data(Data data) : this(
            data.sprite,
            data.path,
            data
        ) {}

        public Data(IController controller) : this(
            controller.data
        ) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

    }

}