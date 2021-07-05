/*
 * Date Created: Friday, July 2, 2021 5:08 PM
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

namespace CoDe_A.Lakbay.ModulesOld.Core.Asset {
    public interface ISerializable<T> : Core.Base.ISerializable where T : UnityEngine.Object {
        string path { get; set; }

    }

    public interface IData<T> : Core.Base.IData, ISerializable<T> where T : UnityEngine.Object {
        T asAsset { get; }

    }

    [Serializable]
    public class Data<T> : Core.Base.Data, IData<T> where T : UnityEngine.Object {
        protected string _path;
        public virtual string path { get => _path; set => _path = value; }

        [YamlIgnore]
        public virtual T asAsset => default;

        public Data() : this("") {}

        public Data(
            string path="",
            Core.Base.ISerializable serializable=null
        ) : base(serializable ?? new Core.Base.Data()) {
            this.path = path;

        }

        public Data(ISerializable<T> serializable) : this(
            serializable.path,
            serializable
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data<T>>()) {}

    }

}