/*
 * Date Created: Friday, July 2, 2021 10:19 PM
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

namespace CoDe_A.Lakbay.ModulesOld.Core.Text {
    public interface ISerializable : Asset.ISerializable<TextAsset> {
        string value { get; set; }

    }

    public interface IData : Asset.IData<TextAsset>, ISerializable {

    }

    [Serializable]
    public class Data : Asset.Data<TextAsset>, IData {
        public override TextAsset asAsset {
            get {
                // var a = LoadAsset(path);
                if(value == null) return null;
                else return new TextAsset(value);

            }
            
        }

        protected string _value;
        public virtual string value { get => _value; set => _value = value; }


        public Data() : this("") {}

        public Data(
            string value="",
            Asset.ISerializable<TextAsset> serializable=null
        ) : base(serializable ?? new Asset.Data<TextAsset>()) {
            this.value = value;

        }

        public Data(ISerializable serializable) : this(
            serializable.value,
            serializable
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

    }

}