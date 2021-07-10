/*
 * Date Created: Saturday, July 3, 2021 8:55 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Asset.Image {
    public interface IData : Asset.IData<Sprite> {

    }

    [Serializable]
    public class Data : Asset.Data<Sprite>, IData {
        public Data() : this(default(Asset.IData<Sprite>)) {}

        public override Sprite asset {
            get {
                try { }
                catch { }

                return default;

            } set {}

        }

        public Data(
            Asset.IData<Sprite> data=null
        ) : base(data ?? new Asset.Data<Sprite>()) {

        }

        public Data(IData data) : this(

        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

    }

}