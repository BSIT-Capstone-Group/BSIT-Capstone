/*
 * Date Created: Monday, July 5, 2021 8:25 PM
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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Entry {
    using Event = Utilities.Event;
    using BaseIData = Core.IData<Controller>;
    using BaseData = Core.Data<Controller>;
    using ImageList = List<Asset.Image.Data>;
    using TextController = Asset.Text.Controller;
    using ImageController = Asset.Image.Controller;

    public enum Type { Text, Image }

    public interface IData : Core.IData {
        TextController textController { get; set; }
        ImageController imageController { get; set; }
        string text { get; set; }
        ImageList images { get; set; }

        Type type { get; }

    }

    public interface IData<T> : Core.IData<T>, IData
        where T : IController {
        Event.OnValueChange<T, TextController> onTextControllerChange { get; }
        Event.OnValueChange<T, ImageController> onImageControllerChange { get; }
        Event.OnStringChange<T> onTextChange { get; }
        Event.OnValueChange<T, ImageList> onImagesChange { get; }

    }

    [Serializable]
    public class Data : Core.Data<Controller>, IData<Controller> {
        public new const string HeaderName = "Entry.Data";

        protected TextController m_textController;
        [Header(HeaderName)]
        [SerializeField]
        protected TextController _textController;
        public virtual TextController textController {
            get => _textController;
            set {
                if(value == textController) return;
                var o = textController; var n = value;
                // _textController = m_textController = value;
                _textController = value;
                onTextControllerChange.Invoke(controller, o, n);
                controller?.OnTextControllerChange(o, n);

            }

        }
        protected ImageController m_imageController;
        [SerializeField]
        protected ImageController _imageController;
        public virtual ImageController imageController {
            get => _imageController;
            set {
                if(value == imageController) return;
                var o = imageController; var n = value;
                // _imageController = m_imageController = value;
                _imageController = value;
                onImageControllerChange.Invoke(controller, o, n);
                controller?.OnImageControllerChange(o, n);

            }

        }
        protected string m_text;
        [SerializeField, TextArea]
        protected string _text;
        public virtual string text {
            get => _text;
            set {
                if(value == text) return;
                var o = text; var n = value;
                // _text = m_text = value;
                _text = value;
                onTextChange.Invoke(controller, o, n);
                controller?.OnTextChange(o, n);

            }

        }
        protected ImageList m_images;
        [SerializeField]
        protected ImageList _images;
        public virtual ImageList images {
            get => _images;
            set {
                if(value == images) return;
                var o = images; var n = value;
                // _images = m_images = value;
                _images = value;
                onImagesChange.Invoke(controller, o, n);
                controller?.OnImagesChange(o, n);

            }

        }

        [YamlIgnore]
        public virtual Type type => text != null && text.Length != 0 ? Type.Text : Type.Image;

        [SerializeField]
        protected Event.OnValueChange<Controller, TextController> _onTextControllerChange = new Event.OnValueChange<Controller, TextController>();
        public virtual Event.OnValueChange<Controller, TextController> onTextControllerChange => _onTextControllerChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, ImageController> _onImageControllerChange = new Event.OnValueChange<Controller, ImageController>();
        public virtual Event.OnValueChange<Controller, ImageController> onImageControllerChange => _onImageControllerChange;
        [SerializeField]
        protected Event.OnStringChange<Controller> _onTextChange = new Event.OnStringChange<Controller>();
        public virtual Event.OnStringChange<Controller> onTextChange => _onTextChange;
        [SerializeField]
        protected Event.OnValueChange<Controller, ImageList> _onImagesChange = new Event.OnValueChange<Controller, ImageList>();
        public virtual Event.OnValueChange<Controller, ImageList> onImagesChange => _onImagesChange;

        public Data() { Create(instance: this); }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            textController = m_textController;
            imageController = m_imageController;
            text = m_text;
            images = m_images;

            if(images != null ) {
                foreach(var i in images) i.OnInspectorHasUpdate();

            }

        }

        public override void Load(TextAsset textAsset) => Create(textAsset, this);

        public static Data Create(
            string text="",
            ImageList images=null,
            BaseIData data=null,
            Data instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);
            instance.text = text;
            instance.images = images;

            return instance;

        }

        public static Data Create(IData<Controller> data, Data instance=null) {
            data ??= new Data();
            return Create(
                data.text,
                data.images,
                data,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, Data instance=null) {
            return Create(textAsset.Parse<Data>(), instance);
            
        }

    }

}