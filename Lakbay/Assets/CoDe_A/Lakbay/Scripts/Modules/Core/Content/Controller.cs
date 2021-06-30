/*
 * Date Created: Tuesday, June 29, 2021 7:41 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Content {
    public interface IController : Core.IController {
        new Data data { get; set; }
        List<Data.Entry> entries { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Content.Controller";

        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;
                entries = value.entries;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Entries")]
        private List<Data.Entry> __entries;
        protected List<Data.Entry> _entries;
        public virtual List<Data.Entry> entries {
            get => _entries;
            set {
                if(value == entries) return;
                _entries = value;
                __entries = value;

            }

        }


        public Controller() : base() {
            (this as IController).data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            entries = __entries;

        }

        public override void SetData(TextAsset textAsset) => (this as IController).data = new Data(textAsset);

    }

}