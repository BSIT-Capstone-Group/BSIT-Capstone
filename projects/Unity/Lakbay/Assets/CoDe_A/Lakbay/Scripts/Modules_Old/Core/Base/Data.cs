/*
 * Date Created: Friday, July 2, 2021 2:19 PM
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

namespace CoDe_A.Lakbay.ModulesOld.Core.Base {
    public interface ISerializable : Raw.IInterface {

    }

    public interface IData : Raw.IData, ISerializable {

    }

    [Serializable]
    public class Data : Raw.Data, IData {
        public Data() : this(default(ISerializable)) {}

        public Data(ISerializable serializable) : base() {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

    }

}