/*
 * Date Created: Wednesday, June 30, 2021 8:30 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Color_ = UnityEngine.Color;

using NaughtyAttributes;
using YamlDotNet.Serialization;

using Code_A_Old_2.Lakbay.Utilities;

namespace Code_A_Old_2.Lakbay.Modules.Core.Color {
    public interface IData : Core.IData {
        Color_ color { get; set; }
        string @string { get; set; }
        List<int> rgba { get; set; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField]
        protected Color_ m_color;
        protected Color_ _color;
        [YamlIgnore]
        public Color_ color {
            get => _color;
            set {
                if(value == color) return;
                _color = m_color = value;

            }

        }
        [SerializeField]
        protected string m_string;
        protected string _string;
        public string @string {
            get => _string;
            set {
                if(value == @string) return;
                _string = m_string = value;

            }

        }
        [SerializeField]
        protected List<int> m_rgba;
        protected List<int> _rgba;
        public List<int> rgba {
            get => _rgba;
            set {
                if(value == rgba) return;
                _rgba = value;

            }

        }
        [YamlIgnore]
        public Color_ asColor {
            get {
                if(ColorUtility.TryParseHtmlString(@string, out Color_ color)) {
                    return color;

                } else if(rgba.Count == 4) {
                    var c = rgba.Select<int, byte>((c) => (byte) c).ToArray();
                    return new Color32(c[0], c[1], c[2], c[3]);

                } else return color;

            }

        }

        public Data() : this("#fff") {}

        public Data(
            string @string="#fff",
            List<int> rgba=null,
            Color_ color=default,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.@string = @string;

            rgba ??= new List<int>();
            while(rgba.Count != 4) rgba.Add(255);

            this.rgba = rgba;
            this.color = default;

        }

        public Data(Data data) : this(
            data.@string,
            data.rgba,
            data.color,
            data
        ) {}

        // public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            @string = m_string;
            rgba = m_rgba;
            color = m_color;

        }

    }

}