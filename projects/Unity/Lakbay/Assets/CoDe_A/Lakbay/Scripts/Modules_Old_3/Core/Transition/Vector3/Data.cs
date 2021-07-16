/*
 * Date Created: Wednesday, July 7, 2021 12:42 PM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Transition.Vector3 {
    using Event = Utilities.Event;
    using Vector3_ = UnityEngine.Vector3;

    public interface IEvent : Core.Transition.IEvent<Vector3_> {

    }

    public interface IProperty : Core.Transition.IProperty<Vector3_> {

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.Transition.IData<Vector3_>, IProperty {

    }

    [Serializable]
    public class Data : Core.Transition.Data<Vector3_>, IData {
        public Data() { Create(instance: this); }

        public static Data Create(
            Core.Transition.IProperty<Vector3_> data=null,
            IProperty instance=null
        ) {
            // instance ??= new Data<T>();
            if(object.Equals(instance, null)) instance = new Data();
            Core.Transition.Data<Vector3_>.Create(
                (object.Equals(data, null) ? new Core.Transition.Data<Vector3_>() as Core.Transition.IProperty<Vector3_>: data),
                instance
            );

            return instance as Data;

        }

        public static Data Create(IProperty data, IProperty instance=null) {
            return Create(
                data,
                instance
            );
        }

        public static Data Create(TextAsset textAsset, IProperty instance=null) {
            return Create(textAsset.Parse<Data>() as IProperty, instance);

        }

    }

}