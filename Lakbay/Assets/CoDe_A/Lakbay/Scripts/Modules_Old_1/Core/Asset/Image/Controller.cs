/*
 * Date Created: Saturday, July 3, 2021 8:55 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Asset.Image {
    using Image_ = UnityEngine.UI.Image;
    using OnImageComponentChange = OnImageComponentChange<IController>;

    [Serializable]
    public class OnImageComponentChange<T> : Core.OnValueChange<T, Image_> {}

    public interface IController : Asset.IController<Data, Sprite>, IData {
        Image_ imageComponent { get; set; }

        OnImageComponentChange onImageComponentChange { get; }
        void OnImageComponentChange(Image_ old, Image_ @new);

    }

    public class Controller : Asset.Controller<Data, Sprite>, IController {
        public new const string BoxGroupName = "Image.Controller";

        public override Data data {
            get => new Data(this);
            set {
                base.data = value;
                if(value != null) {

                }

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Image_ m_imageComponent;
        protected Image_ _imageComponent;
        public Image_ imageComponent {
            get => _imageComponent;
            set {
                if(value == imageComponent) return;
                var o = imageComponent; var n = value;
                _imageComponent = m_imageComponent = value;
                onImageComponentChange.Invoke(this, o, n);
                OnImageComponentChange(o, n);

            }
            
        }

        [Foldout(EventFoldoutName)]
        [SerializeField]
        protected OnImageComponentChange _onImageComponentChange = new OnImageComponentChange();
        public OnImageComponentChange onImageComponentChange => _onImageComponentChange;


        public Controller() : base() {
            data = new Data();

        }
        
        [ContextMenu("Set Data")]
        public override void SetData() => base.SetData();

        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            imageComponent = m_imageComponent;

        }

        public virtual void OnImageComponentChange(Image_ old, Image_ @new) {
            if(@new) @new.sprite = asset;

        }

        public override void OnAssetChange(Sprite old, Sprite @new) {
            base.OnAssetChange(old, @new);
            if(imageComponent) imageComponent.sprite = @new;

        }

    }

}