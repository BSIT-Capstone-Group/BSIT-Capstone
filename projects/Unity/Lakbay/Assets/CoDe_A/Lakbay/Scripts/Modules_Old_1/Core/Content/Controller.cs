/*
 * Date Created: Sunday, July 4, 2021 9:04 AM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Content {
    using EntryList = List<Entry.Data>;
    using OnEntriesChange = OnEntriesChange<IController>;

    [Serializable]
    public class OnEntryControllerChange<T> : OnValueChange<T, Entry.Controller> { }
    [Serializable]
    public class OnEntriesChange<T> : OnValueChange<T, EntryList> { }

    public interface IController : Core.IController<Data>, IData {
        int entriesCount { get; }

        Entry.Controller textEntryController { get; set; }
        Entry.Controller imageEntryController { get; set; }

        OnEntriesChange onEntriesChange { get; }
        void OnEntriesChange(EntryList old, EntryList @new);

        OnEntryControllerChange<IController> onTextEntryControllerChange { get; }
        void OnTextEntryControllerChange(Entry.Controller old, Entry.Controller @new);

        OnEntryControllerChange<IController> onImageEntryControllerChange { get; }
        void OnImageEntryControllerChange(Entry.Controller old, Entry.Controller @new);

        void Populate();

    }

    public class Controller : Core.Controller<Data>, IController {
        public new const string BoxGroupName = "Content.Controller";

        public override Data data {
            get => new Data(this);
            set {
                base.data = value;
                if(value != null) {
                    entries = value.entries;
                    if(entries != null) {
                        foreach(var e in entries) {
                            print(e.text);
                            print(e.images);

                        }


                    }

                }

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Entry.Controller m_textEntryController;
        protected Entry.Controller _textEntryController;
        public virtual Entry.Controller textEntryController {
            get => _textEntryController;
            set {
                if(value == textEntryController) return;
                var o = textEntryController; var n = value;
                _textEntryController = m_textEntryController = value;
                onTextEntryControllerChange.Invoke(this, o, n);
                OnTextEntryControllerChange(o, n);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Entry.Controller m_imageEntryController;
        protected Entry.Controller _imageEntryController;
        public virtual Entry.Controller imageEntryController {
            get => _imageEntryController;
            set {
                if(value == imageEntryController) return;
                var o = imageEntryController; var n = value;
                _imageEntryController = m_imageEntryController = value;
                onImageEntryControllerChange.Invoke(this, o, n);
                OnImageEntryControllerChange(o, n);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected EntryList m_entries;
        protected EntryList _entries;
        public virtual EntryList entries {
            get => _entries;
            set {
                if(value == entries) return;
                var o = entries; var n = value;
                _entries = m_entries = value;
                onEntriesChange.Invoke(this, o, n);
                OnEntriesChange(o, n);

            }
            
        }
        
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnEntryControllerChange<IController> _onTextEntryControllerChange = new OnEntryControllerChange<IController>();
        public virtual OnEntryControllerChange<IController> onTextEntryControllerChange => _onTextEntryControllerChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnEntryControllerChange<IController> _onImageEntryControllerChange = new OnEntryControllerChange<IController>();
        public virtual OnEntryControllerChange<IController> onImageEntryControllerChange => _onImageEntryControllerChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnEntriesChange _onEntriesChange = new OnEntriesChange();
        public virtual OnEntriesChange onEntriesChange => _onEntriesChange;

        protected int _entriesCount;
        public virtual int entriesCount => _entriesCount;


        public Controller() : base() {
            data = new Data();

        }
        
        [ContextMenu("Set Data")]
        public override void SetData() => base.SetData();

        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            textEntryController = m_textEntryController;
            imageEntryController = m_imageEntryController;
            entries = m_entries;

        }

        public virtual void OnEntriesChange(EntryList old, EntryList @new) {
            Populate();

        }

        public virtual void OnTextEntryControllerChange(Entry.Controller old, Entry.Controller @new) {
            Populate();

        }

        public virtual void OnImageEntryControllerChange(Entry.Controller old, Entry.Controller @new) {
            Populate();

        }

        public void Populate() {
            try { if(!Application.isPlaying) return; } catch { return; }
            gameObject.DestroyChildren();

            foreach(var e in entries) {
                Entry.Controller ec = null;
                if(e.type == Entry.Type.Text) ec = textEntryController;
                else if(e.type == Entry.Type.Image) ec = imageEntryController;

                ec = ec.Instantiate<Entry.Controller>(gameObject);
                ec.data = e;
                // print(e.text);
                // print(e.images.Count);

            }

        }

        public virtual void MonitorEntriesCount() {
            if(entries == null) return;
            var c = entries.Count;
            if(c != entriesCount) {
                _entriesCount = entries.Count;
                Populate();

            }

        }

        public override void Update() {
            base.Update();
            MonitorEntriesCount();

        }

        public override void OnDataTextAssetChange(TextAsset old, TextAsset @new) {
            base.OnDataTextAssetChange(old, @new);
            // if(@new) print.AsYaml<Data>())

        }

    }

}