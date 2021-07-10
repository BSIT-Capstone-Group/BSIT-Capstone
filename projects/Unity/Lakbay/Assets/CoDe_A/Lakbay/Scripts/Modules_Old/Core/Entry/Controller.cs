/*
 * Date Created: Friday, July 2, 2021 6:09 PM
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

using NaughtyAttributes;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld.Core.Entry {
    [Serializable]
    public abstract class Event : Base.Event<Controller, Data> {}

    public interface IController : Core.Base.IController, ISerializable {
        new Data data { get; set; }
        Text.Controller textController { get; set; }
        Image.Controller imageController { get; set; }
        
        new Event.OnDataChange onDataChange { get; }

    }

    public class Controller : Core.Base.Controller, IController {
        public new const string BoxGroupName = "Entry.Controller";

        Data IController.data {
            get => new Data(this);
            set {
                var self = this as IController;
                value ??= new Data();
                var o = self.data; var n = value;
                (this as Core.Base.IController).data = value;
                text = value.text;
                images = value.images;
                self.onDataChange.Invoke(this, o, n);
                print("this");

            }

        }
        [BoxGroup(BoxGroupName)]
        // []
        public Event.OnDataChange _onTextDataChange = new Event.OnDataChange();
        Event.OnDataChange IController.onDataChange => _onTextDataChange;
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Text.Controller m_textController;
        protected Text.Controller _textController;
        public Text.Controller textController {
            get => _textController;
            set {
                if(value == textController) return;
                _textController = m_textController = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Image.Controller m_imageController;
        protected Image.Controller _imageController;
        public Image.Controller imageController {
            get => _imageController;
            set {
                if(value == imageController) return;
                _imageController = m_imageController = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected string m_text;
        protected string _text;
        public virtual string text {
            get => _text;
            set {
                if(value == text) return;
                _text = m_text = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected List<Image.Data> m_images;
        protected List<Image.Data> _images;
        public virtual List<Image.Data> images {
            get => _images;
            set {
                if(value == images) return;
                _images = m_images = value;

            }

        }


        public Controller() : base() {
            var self = this as IController;
            self.data = null;

        }

        public override void OnInspectorHasUpdate() {
            var self = this as IController;
            base.OnInspectorHasUpdate();
            textController = m_textController;
            imageController = m_imageController;
            text = m_text;
            images = m_images;

        }

        public override void SetData(TextAsset textAsset) {
            var self = this as IController;
            self.data = new Data(textAsset);
            
        }

    }

}