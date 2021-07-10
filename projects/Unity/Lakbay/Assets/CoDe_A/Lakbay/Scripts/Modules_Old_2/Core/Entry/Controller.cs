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
    using ImageList = List<Asset.Image.Data>;

    public interface IController : Core.IController {
        void OnTextControllerChange(Asset.Text.Controller old, Asset.Text.Controller @new);
        void OnImageControllerChange(Asset.Image.Controller old, Asset.Image.Controller @new);
        void OnTextChange(string old, string @new);
        void OnImagesChange(ImageList old, ImageList @new);

        void Populate();

    }
    
    public interface IController<T> : Core.IController<T>, IController
        where T : IData {
            
    }

    [AddComponentMenu("Core/Entry Controller")]
    // [ExecuteAlways]
    public class Controller : Core.Controller<Data>, IController<Data> {
        public new const string BoxGroupName = "Entry.Controller";

        public override Data data {
            get => base.data;
            set {
                if(value == data) return;
                base.data = value;
                var o = data; var n = value;
                if(o != null) o.controller = null;
                if(n != null) n.controller = this;

            }

        }


        [ContextMenu("Localize")]
        public override void Localize() => base.Localize();

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            
        }

        public virtual void OnTextChange(string old, string @new) {
            Populate();

        }

        public virtual void OnImagesChange(ImageList old, ImageList @new) {
            Populate();

        }

        public virtual void OnTextControllerChange(Asset.Text.Controller old, Asset.Text.Controller @new) {
            Populate();

        }

        public virtual void OnImageControllerChange(Asset.Image.Controller old, Asset.Image.Controller @new) {
            Populate();

        }

        public override void Update() {
            base.Update();

            if(data.type == Type.Image && data.images != null && transform.childCount != data.images.Count) {
                Populate();

            }

        }

        public virtual void Populate() {
            if(!Helper.IsGamePlaying()) return;
        
            gameObject.DestroyChildren();

            if(data.type == Type.Text) {
                if(data.textController == null) return;
                data.textController.data.value = data.text ?? "";

            } else if(data.type == Type.Image) {
                if(data.imageController == null) return;
                if(data.images == null) return;
                if(data.textController) data.textController.data.value = "";

                foreach(var i in data.images) {
                    var c = data.imageController.Instantiate<Asset.Image.Controller>(gameObject);
                    Asset.Image.Data.Create(i, c.data);

                }

            }

        }

    }

}