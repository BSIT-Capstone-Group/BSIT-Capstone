/*
 * Date Created: Tuesday, June 29, 2021 10:15 PM
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
using YamlDotNet.Serialization;

using Code_A_Old_2.Lakbay.Utilities;

namespace Code_A_Old_2.Lakbay.Modules.Core.Content.Entry {
    public enum Type { Text, Image }

    public interface IData : Core.IData {
        Type type { get; }
        string text { get; set; }
        List<Image.Data> images { get; set; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [NonSerialized, YamlIgnore]
        public new Controller controller;

        [YamlIgnore]
        public Type type => (text != null && text.Length != 0) ? Type.Text : Type.Image;

        [SerializeField, TextArea]
        private string m_text;
        protected string _text;
        public virtual string text {
            get => _text;
            set {
                if(value == text) return;
                _text = m_text = value;

            }

        }
        [SerializeField]
        private List<Image.Data> m_images;
        protected List<Image.Data> _images;
        public virtual List<Image.Data> images {
            get => _images;
            set {
                if(value == images) return;
                _images = m_images = value;

            }

        }


        public Data() : this(" ") {}

        public Data(
            string text=" ",
            List<Image.Data> images=null,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.text = text;
            this.images = images ?? new List<Image.Data>();

        }

        public Data(Data data) : this(
            data.text,
            data.images,
            data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            text = m_text;
            images = m_images;
            foreach(var i in images) i.OnInspectorHasUpdate();

        }

    }

}