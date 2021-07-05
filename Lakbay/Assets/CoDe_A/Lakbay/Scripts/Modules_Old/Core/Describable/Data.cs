/*
 * Date Created: Friday, July 2, 2021 6:04 PM
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

namespace CoDe_A.Lakbay.ModulesOld.Core.Describable {
    public interface ISerializable : Core.Base.ISerializable {
        string label { get; set; }
        string description { get; set; }

    }

    public interface IData : Core.Base.IData, ISerializable {

    }

    [Serializable]
    public class Data : Core.Base.Data, IData {
        protected string _label;
        public virtual string label { get => _label; set => _label = value; }
        protected string _description;
        public virtual string description { get => _description; set => _description = value; }

        public Data() : this("") {}

        public Data(
            string label="",
            string description="",
            Core.Base.ISerializable serializable=null
        ) : base(serializable ?? new Core.Base.Data()) {

        }

        public Data(ISerializable serializable) : this(
            serializable.label,
            serializable.description,
            serializable
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

    }

}