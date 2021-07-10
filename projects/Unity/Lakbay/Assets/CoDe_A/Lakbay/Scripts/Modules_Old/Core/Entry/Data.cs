/*
 * Date Created: Friday, July 2, 2021 6:09 PM
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

using NaughtyAttributes;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld.Core.Entry {
    public enum Type { Text, Image }

    public interface ISerializable : Core.Base.ISerializable {
        string text { get; set; }
        List<Image.Data> images { get; set; }

    }

    public interface IData : Core.Base.IData, ISerializable {
        Type type { get; }

    }

    [Serializable]
    public class Data : Core.Base.Data, IData {
        protected string _text;
        public virtual string text { get => _text; set => _text = value; }
        protected List<Image.Data> _images;
        public virtual List<Image.Data> images { get => _images; set => _images = value; }

        [YamlIgnore]
        public virtual Type type => _text != null && _text.Length != 0 ? Type.Text : Type.Image;

        public Data() : this(" ") {}

        public Data(
            string text=" ",
            List<Image.Data> images=null,
            Core.Base.ISerializable serializable=null
        ) : base(serializable ?? new Core.Base.Data()) {
            this.text = text;
            this.images = images ?? new List<Image.Data>();

        }

        public Data(ISerializable serializable) : this(
            serializable.text,
            serializable.images,
            serializable
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

    }

}