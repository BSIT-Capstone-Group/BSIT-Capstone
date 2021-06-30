/*
 * Date Created: Wednesday, June 30, 2021 5:51 AM
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
using Image_ = UnityEngine.UI.Image;
using Text_ = TMPro.TMP_Text;

using NaughtyAttributes;
using RotaryHeart.Lib.SerializableDictionary;

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Interactable.UI {
    [Serializable] public class TextMap : SerializableDictionaryBase<string, Text_> {}
    [Serializable] public class ImageMap : SerializableDictionaryBase<string, Image_> {}

    public interface IController : Interactable.IController {
        List<Text_> texts { get; set; }
        List<Image_> images { get; set; }
        TextMap textMap { get; set; }
        ImageMap imageMap { get; set; }

    }

    public class Controller : Interactable.Controller, IController {
        public new const string BoxGroupName = "UI.Controller";

        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Texts")]
        private List<Text_> __texts;
        protected List<Text_> _texts;
        public virtual List<Text_> texts {
            get => _texts;
            set {
                if(value == texts) return;
                _texts = __texts = value;

            }

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Imagess")]
        private List<Image_> __images;
        protected List<Image_> _images;
        public virtual List<Image_> images {
            get => _images;
            set {
                if(value == images) return;
                _images = __images = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Text Map")]
        private TextMap __textMap;
        protected TextMap _textMap;
        public virtual TextMap textMap {
            get => _textMap;
            set {
                if(value == textMap) return;
                _textMap = __textMap = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Image Map")]
        private ImageMap __imageMap;
        protected ImageMap _imageMap;
        public virtual ImageMap imageMap {
            get => _imageMap;
            set {
                if(value == imageMap) return;
                _imageMap = __imageMap = value;

            }

        }

        
        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            texts = __texts;
            images = __images;
            textMap = __textMap;
            imageMap = __imageMap;
            
        }

        public override void OnHighlightedChange(bool value) {
            base.OnHighlightedChange(value);
            if(value) {


            } else {
                

            }

        }

    }

}