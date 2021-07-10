/*
 * Date Created: Wednesday, July 7, 2021 5:24 AM
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

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;
    // using BaseIData = Core.IData<Controller>;
    // using BaseData = Core.Data<Controller>;

    public interface IInterface {


    }

    public interface IEvent : IInterface {}

    public interface IProperty : IInterface {}

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : IProperty {}

    [Serializable]
    public class Data : IData {
        public Data() : base() { Create(instance: this); }

        public static Data Create(IProperty data=null, IProperty instance=null) {
            instance ??= new Data();
            return instance as Data;

        }

        public static Data Create(TextAsset textAsset, IProperty instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

    }

}