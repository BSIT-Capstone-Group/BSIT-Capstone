/*
 * Date Created: Wednesday, July 7, 2021 11:54 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Game.Slide {
    using Event = Utilities.Event;

    public interface IEvent : Core.Transition.Vector3.IEvent {
        Event.OnIntChange<IEvent> onIndexChange { get; }

        void OnIndexChange(int old, int @new);
    }

    public interface IProperty : Core.Transition.Vector3.IProperty {
        int index { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.Transition.Vector3.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Transition.Vector3.Data, IData {
        [SerializeField] protected int _index;
        public virtual int index { get => _index; set => _index = value; }

    }

}