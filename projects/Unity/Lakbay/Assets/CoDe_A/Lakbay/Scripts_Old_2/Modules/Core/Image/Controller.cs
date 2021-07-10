/*
 * Date Created: Tuesday, June 29, 2021 9:40 PM
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

using Code_A_Old_2.Lakbay.Utilities;
using IC = Code_A_Old_2.Lakbay.Modules.Core.Image.IController; 

namespace Code_A_Old_2.Lakbay.Modules.Core.Image {
    public interface IController : Core.IController {
        bool showImageData { get; }
        new Data data { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Image.Controller";

        public override bool showCoreData => false;
        public virtual bool showImageData => true;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showImageData")]
        protected Data m_imageData;
        protected Data _imageData;
        Data IController.data {
            get => _imageData;
            set {
                value ??= new Data();
                if(value == As<IC>().data) return;
                As<Core.IController>().data = value;
                if(As<IC>().data != null) As<IC>().data.controller = null;
                value.controller = this;
                _imageData = m_imageData = value;

            }

        }


        public Controller() : base() {
            As<IC>().data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            As<IC>().data = m_imageData;
            As<IC>().data?.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => As<IC>().data = new Data(textAsset);

    }

}