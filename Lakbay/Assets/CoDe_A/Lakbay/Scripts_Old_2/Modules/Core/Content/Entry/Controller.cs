/*
 * Date Created: Tuesday, June 29, 2021 10:15 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Text = TMPro.TMP_Text;
using Dropdown = TMPro.TMP_Dropdown;

using NaughtyAttributes;

using Code_A_Old_2.Lakbay.Utilities;
using IC = Code_A_Old_2.Lakbay.Modules.Core.Content.Entry.IController;

namespace Code_A_Old_2.Lakbay.Modules.Core.Content.Entry {
    public interface IController : Core.IController {
        bool showEntryData { get; }
        new Data data { get; set; }
        Interactable.UI.Controller textUI { get; set; }
        Interactable.UI.Controller imageUI { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Entry.Controller";

        public override bool showCoreData => false;
        public virtual bool showEntryData => true;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showEntryData")]
        protected Data m_entryData;
        protected Data _entryData;
        Data IController.data {
            get => _entryData;
            set {
                value ??= new Data();
                if(value == As<IC>().data) return;
                As<Core.IController>().data = value;
                if(As<IC>().data != null) As<IC>().data.controller = null;
                value.controller = this;
                _entryData = m_entryData = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Text UI")]
        protected Interactable.UI.Controller __textUI;
        protected Interactable.UI.Controller _textUI;
        public virtual Interactable.UI.Controller textUI {
            get => _textUI;
            set {
                if(value == textUI) return;
                _textUI = __textUI = value;

            }
            
        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Image UI")]
        protected Interactable.UI.Controller __imageUI;
        protected Interactable.UI.Controller _imageUI;
        public virtual Interactable.UI.Controller imageUI {
            get => _imageUI;
            set {
                if(value == imageUI) return;
                _imageUI = __imageUI = value;

            }
            
        }


        public Controller() : base() {
            As<IC>().data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            As<IC>().data = m_entryData;
            textUI = __textUI;
            imageUI = __imageUI;
            As<IC>().data?.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => As<IC>().data = new Data(textAsset);

    }

}