/*
 * Date Created: Wednesday, July 7, 2021 5:42 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Information {
    using Event = Utilities.Event;

    public interface IEvent : Core.IEvent {
        Event.OnStringChange<IEvent> onLabelChange { get; }
        Event.OnStringChange<IEvent> onDescriptionChange { get; }

        void OnLabelChange(string old, string @new);
        void OnDescriptionChange(string old, string @new);

    }

    public interface IProperty : Core.IProperty {
        string label { get; set; }
        string description { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected string _label;
        public virtual string label { get => _label; set => _label = value; }
        [SerializeField, TextArea] protected string _description;
        public virtual string description { get => _description; set => _description = value; }

    }

}