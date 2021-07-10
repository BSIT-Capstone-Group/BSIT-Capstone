/*
 * Date Created: Wednesday, July 7, 2021 7:30 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Asset.Image {
    using Event = Utilities.Event;
    using Load = Sprite;
    using Component = UnityEngine.UI.Image;

    public interface IEvent : Asset.IEvent<Load, Component> {
        Event.OnValueChange<IEvent, Information.Data> onInformationChange { get; }

        void OnInformationChange(Information.Data old, Information.Data @new);

    }

    public interface IProperty : Asset.IProperty<Load, Component> {
        Information.Data information { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Asset.IData<Load, Component>, IProperty {

    }

    [Serializable]
    public class Data : Asset.Data<Load, Component>, IData {
        [SerializeField] protected Information.Data _information;
        public virtual Information.Data information { get => _information; set => _information = value; }

    }

}