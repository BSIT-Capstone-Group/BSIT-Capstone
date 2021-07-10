/*
 * Date Created: Wednesday, July 7, 2021 8:59 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Input {
    using Event = Utilities.Event;

    public interface IEvent : Core.IEvent {
        Event.OnBoolChange<IEvent> onActiveChange { get; }

        void OnActiveChange(bool old, bool @new);

    }

    public interface IProperty : Core.IProperty {
        bool active { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected bool _active;
        public bool active { get => _active; set => _active = value; }

    }

}