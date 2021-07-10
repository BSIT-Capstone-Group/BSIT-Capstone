/*
 * Date Created: Saturday, July 3, 2021 6:28 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Asset.Text {
    public interface IData : Asset.IData<TextAsset> {
        string value { get; set; }

    }

    [Serializable]
    public class Data : Asset.Data<TextAsset>, IData {
        protected string _value;
        public virtual string value { get => _value; set => _value = value; }

        [YamlIgnore]
        public override TextAsset asset {
            get {
                try { return new TextAsset(value); }
                catch { return default; }

            } set {}

        }

        public Data() : this("") {}

        public Data(
            string value="",
            Asset.IData<TextAsset> data=null
        ) : base(data ?? new Asset.Data<TextAsset>()) {
            this.value = value;

        }

        public Data(IData data) : this(
            data.value,
            data
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

    }

}