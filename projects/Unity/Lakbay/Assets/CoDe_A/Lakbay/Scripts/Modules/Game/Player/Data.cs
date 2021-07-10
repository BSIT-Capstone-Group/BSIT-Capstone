/*
 * Date Created: Wednesday, July 7, 2021 10:26 AM
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

namespace CoDe_A.Lakbay.Modules.Game.Player {
    using Event = Utilities.Event;

    public interface IEvent : Core.Interactable.IEvent {

    }

    public interface IProperty : Core.Interactable.IProperty {
        Move.Data slide { get; set; }
        Move.Data travel { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.Interactable.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Interactable.Data, IData {
        [SerializeField] protected Move.Data _slide;
        public virtual Move.Data slide { get => _slide; set => _slide = value; }
        [SerializeField] protected Move.Data _travel;
        public virtual Move.Data travel { get => _travel; set => _travel = value; }

    }

}