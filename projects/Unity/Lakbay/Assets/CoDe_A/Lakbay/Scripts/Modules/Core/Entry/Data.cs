/*
 * Date Created: Wednesday, July 7, 2021 8:10 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Entry {
    using Event = Utilities.Event;
    using ImageList = List<Asset.Image.Data>;

    public enum Type { Text, Image }

    public interface IEvent : Core.IEvent {
        Event.OnStringChange<IEvent> onTextChange { get; }
        Event.OnValueChange<IEvent, ImageList> onImagesChange { get; }

        void OnTextChange(string old, string @new);
        void OnImagesChange(ImageList old, ImageList @new);

    }

    public interface IProperty : Core.IProperty {
        string text { get; set; }
        ImageList images { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected string _text;
        public virtual string text { get => _text; set => _text = value; }
        [SerializeField] protected ImageList _images;
        public virtual ImageList images { get => _images; set => _images = value; }

    }

}