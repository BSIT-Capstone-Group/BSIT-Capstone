/*
 * Date Created: Tuesday, July 6, 2021 10:58 AM
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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Content {
    using Event = Utilities.Event;
    using BaseIData = Core.IData<Controller>;
    using BaseData = Core.Data<Controller>;
    using EntryList = List<Entry.Data>;

    public interface IData : Core.IData {
        Entry.Controller textEntryController { get; set; }
        Entry.Controller imageEntryController { get; set; }
        EntryList entries { get; set; }

    }

    public interface IData<T> : Core.IData<T>, IData
        where T : IController {
        Event.OnValueChange<T, Entry.Controller> onTextEntryControllerChange { get; }
        Event.OnValueChange<T, Entry.Controller> onImageEntryControllerChange { get; }
        Event.OnValueChange<T, EntryList> onEntriesChange { get; }

    }

    [Serializable]
    public class Data : Core.Data<Controller>, IData<Controller> {
        public new const string HeaderName = "Content.Data";

        protected Entry.Controller m_textEntryController;
        [Header(HeaderName)]
        [SerializeField]
        protected Entry.Controller _textEntryController;
        [YamlIgnore]
        public virtual Entry.Controller textEntryController {
            get => _textEntryController;
            set {
                if(value == textEntryController) return;
                var o = textEntryController; var n = value;
                // _textEntryController = m_textEntryController = value;
                _textEntryController = value;
                onTextEntryControllerChange.Invoke(controller, o, n);
                controller?.OnTextEntryControllerChange(o, n);

            }

        }
        protected Entry.Controller m_imageEntryController;
        [SerializeField]
        protected Entry.Controller _imageEntryController;
        [YamlIgnore]
        public virtual Entry.Controller imageEntryController {
            get => _imageEntryController;
            set {
                if(value == imageEntryController) return;
                var o = imageEntryController; var n = value;
                // _imageEntryController = m_imageEntryController = value;
                _imageEntryController = value;
                onImageEntryControllerChange.Invoke(controller, o, n);
                controller?.OnImageEntryControllerChange(o, n);

            }

        }
        protected EntryList m_entries;
        [SerializeField]
        protected EntryList _entries;
        public virtual EntryList entries {
            get => _entries;
            set {
                if(value == entries) return;
                var o = entries; var n = value;
                // _entries = m_entries = value;
                _entries = value;
                onEntriesChange.Invoke(controller, o, n);
                controller?.OnEntriesChange(o, n);

            }

        }

        [SerializeField]
        protected Event.OnValueChange<Controller, Entry.Controller> _onTextEntryControllerChange = new Event.OnValueChange<Controller, Entry.Controller>();
        public virtual Event.OnValueChange<Controller, Entry.Controller> onTextEntryControllerChange => _onTextEntryControllerChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, Entry.Controller> _onImageEntryControllerChange = new Event.OnValueChange<Controller, Entry.Controller>();
        public virtual Event.OnValueChange<Controller, Entry.Controller> onImageEntryControllerChange => _onImageEntryControllerChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, EntryList> _onEntriesChange = new Event.OnValueChange<Controller, EntryList>();
        public virtual Event.OnValueChange<Controller, EntryList> onEntriesChange => _onEntriesChange;


        public Data() { Create(instance: this); }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            // textEntryController = m_textEntryController;
            // imageEntryController = m_imageEntryController;
            // entries = m_entries;

            if(m_textEntryController != textEntryController) {
                var o = m_textEntryController; var n = textEntryController;
                onTextEntryControllerChange.Invoke(controller, o, n);
                controller?.OnTextEntryControllerChange(o, n);
                m_textEntryController = textEntryController;

            }
            if(m_imageEntryController != imageEntryController) {
                var o = m_imageEntryController; var n = imageEntryController;
                onImageEntryControllerChange.Invoke(controller, o, n);
                controller?.OnImageEntryControllerChange(o, n);
                m_imageEntryController = imageEntryController;

            }
            if(m_entries != entries) {
                var o = m_entries; var n = entries;
                onEntriesChange.Invoke(controller, o, n);
                controller?.OnEntriesChange(o, n);
                m_entries = entries;

            }

            if(entries != null ) {
                foreach(var e in entries) e.OnInspectorHasUpdate();

            }

        }

        public override void Load(TextAsset textAsset) => Create(textAsset, this);

        public static Data Create(
            EntryList entries=null,
            BaseIData data=null,
            Data instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.entries = entries ?? new EntryList();

            return instance;

        }

        public static Data Create(
            IData<Controller> data,
            Data instance=null
        ) {
            data ??= new Data();
            return Create(
                data.entries,
                data as BaseIData,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, Data instance=null) {
           return Create(textAsset.Parse<Data>(), instance);

        }

    }

}