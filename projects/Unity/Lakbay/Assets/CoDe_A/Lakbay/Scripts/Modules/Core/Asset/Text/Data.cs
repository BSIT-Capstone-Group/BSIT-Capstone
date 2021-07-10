/*
 * Date Created: Wednesday, July 7, 2021 8:05 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Asset.Text {
    using Event = Utilities.Event;
    using Load = TextAsset;
    using Component = TextMeshProUGUI;

    public interface IEvent : Asset.IEvent<Load, Component> {
        Event.OnStringChange<IEvent> onValueChange { get; }

        void OnValueChange(string old, string @new);

    }

    public interface IProperty : Asset.IProperty<Load, Component> {
        string value { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Asset.IData<Load, Component>, IProperty {

    }

    [Serializable]
    public class Data : Asset.Data<Load, Component>, IData {
        [SerializeField] protected string _value;
        public virtual string value { get => _value; set => _value = value; }

    }

}