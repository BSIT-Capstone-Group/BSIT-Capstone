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

using Code_A_Old_2.Lakbay.Utilities;

namespace Code_A_Old_2.Lakbay.Modules.Core.Interactable.UI {
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

        // public override bool showInteractableData => false;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        private List<Text_> m_texts;
        protected List<Text_> _texts;
        public virtual List<Text_> texts {
            get => _texts;
            set {
                if(value == texts) return;
                _texts = m_texts = value;

            }

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        private List<Image_> m_images;
        protected List<Image_> _images;
        public virtual List<Image_> images {
            get => _images;
            set {
                if(value == images) return;
                _images = m_images = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        private TextMap m_textMap;
        protected TextMap _textMap;
        public virtual TextMap textMap {
            get => _textMap;
            set {
                if(value == textMap) return;
                _textMap = m_textMap = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Image Map")]
        private ImageMap m_imageMap;
        protected ImageMap _imageMap;
        public virtual ImageMap imageMap {
            get => _imageMap;
            set {
                if(value == imageMap) return;
                _imageMap = m_imageMap = value;

            }

        }

        
        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            texts = m_texts;
            images = m_images;
            textMap = m_textMap;
            imageMap = m_imageMap;
            
        }

        public override void OnHighlightedChange(bool value) {
            base.OnHighlightedChange(value);
            if(value) {


            } else {
                

            }

        }

    }

}