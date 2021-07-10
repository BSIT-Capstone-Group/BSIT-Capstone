/*
 * Date Created: Wednesday, July 7, 2021 8:36 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Highlight {
    using Event = Utilities.Event;

    public interface IEvent : Core.IEvent {
        Event.OnBoolChange<IEvent> onActiveChange { get; }
        Event.OnFloatChange<IEvent> onWidthChange { get; }
        Event.OnStringChange<IEvent> onColorChange { get; }
        Event.OnValueChange<IEvent, Outline.Mode> onModeChange { get; }

        void OnActiveChange(bool old, bool @new);
        void OnWidthChange(float old, float @new);
        void OnColorChange(string old, string @new);
        void OnModeChange(Outline.Mode old, Outline.Mode @new);

    }

    public interface IProperty : Core.IProperty {
        bool active { get; set; }
        float width { get; set; }
        string color { get; set; }
        Outline.Mode mode { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected bool _active;
        public virtual bool active { get => _active; set => _active = value; }
        [SerializeField] protected float _width;
        public virtual float width { get => _width; set => _width = value; }
        [SerializeField] protected string _color;
        public virtual string color { get => _color; set => _color = value; }
        [SerializeField] protected Outline.Mode _mode;
        public virtual Outline.Mode mode { get => _mode; set => _mode = value; }
    }

}