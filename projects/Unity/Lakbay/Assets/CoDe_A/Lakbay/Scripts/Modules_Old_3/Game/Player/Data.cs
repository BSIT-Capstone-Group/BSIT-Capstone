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

namespace CoDe_A.Lakbay.ModulesOld3.Game.Player {
    using Event = Utilities.Event;

    public interface IEvent : Core.Interactable.IEvent {

    }

    public interface IProperty : Core.Interactable.IProperty {
        Slide.Data slide { get; set; }
        Travel.Data travel { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.Interactable.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Interactable.Data, IData {
        [SerializeField] protected Slide.Data _slide;
        public virtual Slide.Data slide { get => _slide; set => _slide = value; }
        [SerializeField] protected Travel.Data _travel;
        public virtual Travel.Data travel { get => _travel; set => _travel = value; }

    }

}