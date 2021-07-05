/*
 * Date Created: Sunday, July 4, 2021 2:41 AM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Entry {
    using ImageList = List<Asset.Image.Data>;

    public enum Type { Text, Image }

    public interface IData : Core.IData {
        string text { get; set; }
        ImageList images { get; set; }

        Type type { get; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        protected string _text;
        public virtual string text { get => _text; set => _text = value; }
        protected ImageList _images;
        public virtual ImageList images { get => _images; set => _images = value; }

        [YamlIgnore]
        public virtual Type type {
            get {
                return _text != null && _text.Length != 0 ? Type.Text : Type.Image;

            }

        }


        public Data() : this(" ") {}

        public Data(
            string text=" ",
            ImageList images =null,
            Core.IData data=null
        ) : base(data ?? new Core.Data()) {
            this.text = text;
            this.images = images ?? new ImageList();

        }

        public Data(IData data) : this(
            data.text,
            data.images,
            data
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<IData>()) {}

    }

}