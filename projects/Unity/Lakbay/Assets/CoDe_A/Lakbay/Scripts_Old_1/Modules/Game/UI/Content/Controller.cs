/*
 * Date Created: Tuesday, June 29, 2021 12:46 PM
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

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.UI.Content {
    public interface IController : UI.IController {
        new Data data { get; set; }
        List<Entry.Data> entries { get; set; }
        bool isEmpty { get; }

    }

    public class Controller : UI.Controller, IController {
        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as UI.IController).data = value;
                entries = value.entries;

            }

        }

        [BoxGroup("Content.Controller")]
        [SerializeField, Label("Entries")]
        private List<Entry.Data> __entries;
        protected List<Entry.Data> _entries;
        public List<Entry.Data> entries {
            get => _entries;
            set {
                if(value == entries) return;
                _entries = value;
                __entries = value;

            }

        }

        public bool isEmpty => (this as IController).data.isEmpty;


        public override void Awake() {
            base.Awake();
            (this as IController).data = new Data();

            SetData(dataTextAsset);

        }

        public override void OnHasUpdate() {
            base.OnHasUpdate();

        }

        public override void SetData(TextAsset data) => (this as IController).data = Helper.Parse<Data>(data);

    }

}