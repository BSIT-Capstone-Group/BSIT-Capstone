/*
 * Date Created: Sunday, July 4, 2021 2:41 AM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Entry {
    using TextController = Asset.Text.Controller;
    using ImageController = Asset.Image.Controller;
    using ImageList = List<Asset.Image.Data>;
    using OnTextControllerChange = OnTextControllerChange<IController>;
    using OnImageControllerChange = OnImageControllerChange<IController>;
    using OnImagesChange = OnImagesChange<IController>;

    [Serializable]
    public class OnTextControllerChange<T> : OnValueChange<T, TextController> { }
    [Serializable]
    public class OnImageControllerChange<T> : OnValueChange<T, ImageController> { }
    [Serializable]
    public class OnTextChange : OnStringValueChange<IController> { }
    [Serializable]
    public class OnImagesChange<T> : OnValueChange<T, ImageList> { }

    public interface IController : IController<Data>, IData {
        int imagesCount { get; }

        TextController textController { get; }
        void OnTextControllerChange(TextController old, TextController @new);

        ImageController imageController { get; }
        void OnImageControllerChange(ImageController old, ImageController @new);

        OnTextChange onTextChange { get; }
        void OnTextChange(string old, string @new);

        OnImagesChange onImagesChange { get; }
        void OnImagesChange(ImageList old, ImageList @new);

        void Populate();
        void MonitorImagesCount();

    }

    public class Controller : Controller<Data>, IController {
        public new const string BoxGroupName = "Entry.Controller";

        public override Data data {
            get => new Data(this);
            set {
                base.data = value;
                if(value != null) {
                    text = value.text;
                    images = value.images;

                }

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected TextController m_textController;
        protected TextController _textController;
        public virtual TextController textController {
            get => _textController;
            set {
                if(value == textController) return;
                var o = textController; var n = value;
                _textController = m_textController = value;
                onTextControllerChange.Invoke(this, o, n);
                OnTextControllerChange(o, n);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected ImageController m_imageController;
        protected ImageController _imageController;
        public virtual ImageController imageController {
            get => _imageController;
            set {
                if(value == imageController) return;
                var o = imageController; var n = value;
                _imageController = m_imageController = value;
                onImageControllerChange.Invoke(this, o, n);
                OnImageControllerChange(o, n);

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
                var o = text; var n = value;
                _text = m_text = value;
                onTextChange.Invoke(this, o, n);
                OnTextChange(o, n);

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected ImageList m_images;
        protected ImageList _images;
        public virtual ImageList images {
            get => _images;
            set {
                if(value == images) return;
                var o = images; var n = value;
                _images = m_images = value;
                onImagesChange.Invoke(this, o, n);
                OnImagesChange(o, n);

            }

        }

        public virtual Type type {
            get {
                return data.type;

            }

        }

        protected int _imagesCount;
        public virtual int imagesCount => _imagesCount;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnTextControllerChange _onTextControllerChange = new OnTextControllerChange();
        public virtual OnTextControllerChange onTextControllerChange => _onTextControllerChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnImageControllerChange _onImageControllerChange = new OnImageControllerChange();
        public virtual OnImageControllerChange onImageControllerChange => _onImageControllerChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnTextChange _onTextChange = new OnTextChange();
        public virtual OnTextChange onTextChange => _onTextChange;
        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnImagesChange _onImagesChange = new OnImagesChange();
        public virtual OnImagesChange onImagesChange => _onImagesChange;


        public Controller() : base() {
            data = new Data();

        }
        
        [ContextMenu("Set Data")]
        public override void SetData() => base.SetData();

        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            textController = m_textController;
            imageController = m_imageController;
            text = m_text;
            images = m_images;

        }

        public override void Update() {
            base.Update();
            MonitorImagesCount();

        }

        public virtual void OnTextChange(string old, string @new) {
            Populate();

        }

        public virtual void OnImagesChange(ImageList old, ImageList @new) {
            Populate();

        }

        public virtual void OnTextControllerChange(TextController old, TextController @new) {
            Populate();
            
        }

        public virtual void OnImageControllerChange(ImageController old, ImageController @new) {
            Populate();

        }

        public virtual void Populate() {
            try { if(!Application.isPlaying) return; } catch { return; }
            gameObject.DestroyChildren();
            
            if(type == Type.Text) {
                if(textController) {
                    var ic = textController.Instantiate<TextController>(gameObject);
                    ic.value = text;

                }

            } else if(type == Type.Image) {
                if(imageController) {
                    foreach(var i in images) {
                        var ic = imageController.Instantiate<ImageController>(gameObject);
                        ic.asset = i.asset;

                    }

                }

            }

        }

        public virtual void MonitorImagesCount() {
            if(images == null) return;
            var c = images.Count;
            if(c != imagesCount) {
                _imagesCount = images.Count;
                Populate();

            }

        }

    }

}