/*
 * Date Created: Wednesday, June 30, 2021 9:04 PM
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
using Outline_ = Code_A_Old_2.Lakbay.Utilities.Outline;

namespace Code_A_Old_2.Lakbay.Modules.Core.Interactable.GameObject.Outline {
    public interface IData : Code_A_Old_2.Lakbay.Modules.Core.IData {
        Color.Data color { get; set; }
        Outline_.Mode mode { get; set; }
        float width { get; set; }


    }

    [Serializable]
    public class Data : Code_A_Old_2.Lakbay.Modules.Core.Data, IData {
        [SerializeField]
        protected Color.Data m_color;
        protected Color.Data _color;
        public Color.Data color {
            get => _color;
            set {
                if(value == color) return;
                _color = m_color = value;

            }

        }
        [SerializeField]
        protected Outline_.Mode m_mode;
        protected Outline_.Mode _mode;
        public Outline_.Mode mode {
            get => _mode;
            set {
                if(value == mode) return;
                _mode = m_mode = value;

            }

        }
        [SerializeField]
        protected float m_width;
        protected float _width;
        public float width {
            get => _width;
            set {
                if(value == width) return;
                _width = m_width = value;

            }

        }


        public Data() : this((Color.Data) null) {}

        public Data(
            Color.Data color=null,
            Outline_.Mode mode=Outline_.Mode.OutlineAll,
            float width=15.0f,
            Code_A_Old_2.Lakbay.Modules.Core.Data data=null
        ) : base(data ?? new Code_A_Old_2.Lakbay.Modules.Core.Data()) {
            this.color = color ?? new Color.Data();
            this.mode = mode;
            this.width = width;

        }

        public Data(Data data) : this(
            data.color,
            data.mode,
            data.width,
            data
        ) {}

        // public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            color = m_color;
            mode = m_mode;
            width = m_width;
            color?.OnInspectorHasUpdate();

        }

    }

}