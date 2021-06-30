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

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Content.Entry {
    public interface IController : Core.IController {
        new Data data { get; set; }
        Interactable.UI.Controller textUI { get; set; }
        Interactable.UI.Controller imageUI { get; set; }
        string text { get; set; }
        List<Image.Data> images { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Entry.Controller";

        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;
                text = value.text;
                images = value.images;

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
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Text")]
        private string __text;
        protected string _text;
        public virtual string text {
            get => _text;
            set {
                if(value == text) return;
                _text = __text = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Images")]
        private List<Image.Data> __images;
        protected List<Image.Data> _images;
        public virtual List<Image.Data> images {
            get => _images;
            set {
                if(value == images) return;
                _images = __images = value;

            }

        }


        public Controller() : base() {
            (this as IController).data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            textUI = __textUI;
            imageUI = __imageUI;
            text = __text;
            images = __images;

        }

        public override void SetData(TextAsset textAsset) => (this as IController).data = new Data(textAsset);

    }

}