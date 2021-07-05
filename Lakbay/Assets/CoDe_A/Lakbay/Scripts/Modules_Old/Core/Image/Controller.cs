/*
 * Date Created: Friday, July 2, 2021 7:23 PM
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

using Image_ = UnityEngine.UI.Image;

namespace CoDe_A.Lakbay.ModulesOld.Core.Image {
    public interface IController : Asset.IController<Sprite>, ISerializable {
        Image_ imageComponent { get; set; }

    }

    public class Controller : Asset.Controller<Sprite>, IController {
        public new const string BoxGroupName = "Image.Controller";

        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected Image_ m_imageComponent;
        protected Image_ _imageComponent;
        public Image_ imageComponent {
            get => _imageComponent;
            set {
                if(value == imageComponent) return;
                _imageComponent = m_imageComponent = value;

            }

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            imageComponent = m_imageComponent;

        }

        public override void OnPathChange(string oldValue, string newValue) {
            var self = this as IController;
            base.OnPathChange(oldValue, newValue);
            if(imageComponent) imageComponent.sprite = self.data.asAsset;

        }

        public override void OnAssetChange(Sprite oldValue, Sprite newValue) {
            base.OnAssetChange(oldValue, newValue);
            if(imageComponent) imageComponent.sprite = newValue;

        }

    }

}