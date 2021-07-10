/*
 * Date Created: Wednesday, July 7, 2021 8:16 AM
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

namespace CoDe_A.Lakbay.Modules.Core.Content {
    using Event = Utilities.Event;
    using EntryList = List<Entry.Data>;

    public interface IEvent : Core.IEvent {
        Event.OnValueChange<IEvent, Information.Data> onInformationChange { get; }
        Event.OnValueChange<IEvent, Asset.Image.Data> onImageChange { get; }
        Event.OnValueChange<IEvent, EntryList> onEntriesChange { get; }

        void OnInformationChange(Information.Data old, Information.Data @new);
        void OnImageChange(Asset.Image.Data old, Asset.Image.Data @new);
        void OnEntriesChange(EntryList old, EntryList @new);

    }

    public interface IProperty : Core.IProperty {
        Information.Data information { get; set; }
        Asset.Image.Data image { get; set; }
        EntryList entries { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected Information.Data _information;
        public virtual Information.Data information { get => _information; set => _information = value; }
        [SerializeField] protected Asset.Image.Data _image;
        public virtual Asset.Image.Data image { get => _image; set => _image = value; }
        [SerializeField] protected EntryList _entries;
        public virtual EntryList entries { get => _entries; set => _entries = value; }

    }

}