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
using YamlDotNet.Serialization;

using Code_A_Old_2.Lakbay.Utilities;

namespace Code_A_Old_2.Lakbay.Modules.Core.Image {
    public interface IData : Core.IData {
        Sprite sprite { get; set; }
        string path { get; set; }
        Sprite asSprite { get; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [NonSerialized, YamlIgnore]
        public new Controller controller;

        [SerializeField]
        private Sprite m_sprite;
        protected Sprite _sprite;
        [YamlIgnore]
        public virtual Sprite sprite {
            get => _sprite;
            set {
                if(value == sprite) return;
                _sprite = m_sprite = value;

            }

        }
        [SerializeField]
        private string m_path;
        protected string _path;
        public virtual string path {
            get => _path;
            set {
                if(value == path) return;
                _path = m_path = value;

            }

        }
        [YamlIgnore]
        public Sprite asSprite {
            get {
                if(true) {
                    // return path;
                    return default;

                }
                
                return sprite;

            }

        }

        public Data() : this(default(Sprite)) {}

        public Data(
            Sprite sprite=default,
            string path="",
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.sprite = sprite;
            this.path = path;

        }

        public Data(Data data) : this(
            data.sprite,
            data.path,
            data
        ) {}

        public Data(IController controller) : this(
            controller.data
        ) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            sprite = m_sprite;
            path = m_path;

        }

    }

}