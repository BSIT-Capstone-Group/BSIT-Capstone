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
    using EntryList = List<Entry.Data>;

    public interface IController : Core.IController {
        void OnTextEntryControllerChange(Entry.Controller old, Entry.Controller @new);
        void OnImageEntryControllerChange(Entry.Controller old, Entry.Controller @new);
        void OnEntriesChange(EntryList old, EntryList @new);

        void Populate();

    }
    
    public interface IController<T> : Core.IController<T>, IController
        where T : IData {
            
    }

    // [AddComponentMenu("Core/Content Controller")]
    // [ExecuteAlways]
    public class Controller : Core.Controller<Data>, IController<Data> {
        public new const string BoxGroupName = "Content.Controller";

        public override Data data {
            get => base.data;
            set {
                if(value == data) return;
                base.data = value;
                var o = data; var n = value;
                if(o != null) o.controller = null;
                if(n != null) n.controller = this;

            }

        }


        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            
        }

        public virtual void OnTextEntryControllerChange(Entry.Controller old, Entry.Controller @new) {
            Populate();

        }

        public virtual void OnImageEntryControllerChange(Entry.Controller old, Entry.Controller @new) {
            Populate();

        }

        public virtual void OnEntriesChange(EntryList old, EntryList @new) {
            Populate();

        }

        public virtual void Populate() {
            if(!Helper.IsGamePlaying()) return;

            gameObject.DestroyChildren();
            if(data.entries == null) return;

            foreach(var e in data.entries) {
                Entry.Controller c = null;
                if(e.type == Entry.Type.Text) {
                    // if(!data.textEntryController) continue;
                    c = data.textEntryController.Instantiate<Entry.Controller>(gameObject);

                } else if(e.type == Entry.Type.Image) {
                    // if(!data.imageEntryController) continue;
                    c = data.imageEntryController.Instantiate<Entry.Controller>(gameObject);

                }

                Entry.Data.Create(e, c.data);

            }

        }

        public override void Update() {
            base.Update();

            if(data.entries != null && transform.childCount != data.entries.Count) {
                Populate();

            }

        }

    }

}