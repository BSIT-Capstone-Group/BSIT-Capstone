/*
 * Date Created: Tuesday, June 29, 2021 10:49 AM
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

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.UI.Content.Entry {
    public interface IController : UI.IController {
        new Data data { get; set; }
        string text { get; set; }
        List<Data.Image> images { get; set; }
        bool isImage { get; }

    }

    public class Controller : UI.Controller, IController {
        Data IController.data {
            get => new Data(this); set {
                if(value == null) value = new Data();
                (this as UI.IController).data = value;
                text = value.text;
                images = value.images;

            }

        }

        [BoxGroup("Entry.Controller")]
        [SerializeField, Label("Text"), TextArea]
        private string __text;
        protected string _text;
        public string text {
            get => _text;
            set {
                if(value == text) return;
                _text = value;
                __text = value;

            }

        }

        [BoxGroup("Entry.Controller")]
        [SerializeField, Label("Images")]
        private List<Data.Image> __images;
        protected List<Data.Image> _images;
        public List<Data.Image> images {
            get => _images;
            set {
                if(value == images) return;
                _images = value;
                __images = value;

            }

        }
        
        public bool isImage => (this as IController).data.isImage;


        public override void Awake() {
            base.Awake();
            (this as IController).data = new Data();
            
        }

        public override void OnHasUpdate() {
            base.OnHasUpdate();

            text = __text;
            images = __images;

        }

        public override void SetData(TextAsset data) => (this as IController).data = Helper.Parse<Data>(data);


    }

}