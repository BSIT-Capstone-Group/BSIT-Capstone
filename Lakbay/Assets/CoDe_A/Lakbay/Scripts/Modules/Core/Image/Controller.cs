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

using Code_A.Lakbay.Utilities;

namespace Code_A.Lakbay.Modules.Core.Image {
    public interface IController : Core.IController {
        new Data data { get; set; }
        Sprite sprite { get; set; }
        string path { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Image.Controller";

        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as Core.IController).data = value;
                sprite = value.sprite;
                path = value.path;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Sprite")]
        private Sprite __sprite;
        protected Sprite _sprite;
        public virtual Sprite sprite {
            get => _sprite;
            set {
                if(value == sprite) return;
                _sprite = value;
                __sprite = value;

            }

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField, Label("Path")]
        private string __path;
        protected string _path;
        public virtual string path {
            get => _path;
            set {
                if(value == path) return;
                _path = value;
                __path = value;

            }

        }


        public Controller() : base() {
            (this as IController).data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            sprite = __sprite;
            path = __path;

        }

        public override void SetData(TextAsset textAsset) => (this as IController).data = new Data(textAsset);

    }

}