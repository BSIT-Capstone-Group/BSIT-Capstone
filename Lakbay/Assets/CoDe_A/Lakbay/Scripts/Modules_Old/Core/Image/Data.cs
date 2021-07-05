/*
 * Date Created: Friday, July 2, 2021 7:23 PM
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

namespace CoDe_A.Lakbay.ModulesOld.Core.Image {
    public interface ISerializable : Asset.ISerializable<Sprite> {

    }

    public interface IData : Asset.IData<Sprite>, ISerializable {

    }

    [Serializable]
    public class Data : Asset.Data<Sprite>, IData {

    }

}