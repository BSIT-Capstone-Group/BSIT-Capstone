/*
 * Date Created: Wednesday, June 30, 2021 9:52 AM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Serialization;

using NaughtyAttributes;

using Code_A_Old_2.Lakbay.Utilities;
using YamlDotNet.Serialization;

namespace Code_A_Old_2.Lakbay.Modules.Core.Interactable.GameObject {
    public interface IData : Interactable.IData {
        Outline.Data outline { get; set; }
        bool canCollide { get; set; }
        bool oneTimeCollision { get; set; }

    }

    [Serializable]
    public class Data : Interactable.Data, IData {
        [NonSerialized, YamlIgnore]
        public new Controller controller;

        [SerializeField]
        protected Outline.Data m_outline;
        protected Outline.Data _outline;
        public Outline.Data outline {
            get => _outline;
            set {
                if(value == outline) return;
                _outline = m_outline = value;

            }

        }
        [SerializeField]
        protected bool m_canCollide;
        protected bool _canCollide;
        public bool canCollide {
            get => _canCollide;
            set {
                if(value == canCollide) return;
                _canCollide = m_canCollide = value;

            }

        }
        [SerializeField]
        protected bool m_oneTimeCollision;
        protected bool _oneTimeCollision;
        public bool oneTimeCollision {
            get => _oneTimeCollision;
            set {
                if(value == oneTimeCollision) return;
                _oneTimeCollision = m_oneTimeCollision = value;

            }

        }
        

        public Data() : this((Outline.Data) null) {}

        public Data(
            Outline.Data outline=null,
            bool canCollide=true,
            bool oneTimeCollision=false,
            Interactable.Data data=null
        ) : base(data ?? new Interactable.Data()) {
            this.outline = outline ?? new Outline.Data();
            this.canCollide = canCollide;
            this.oneTimeCollision = oneTimeCollision;

        }

        public Data(Data data) : this(
            data.outline,
            data.canCollide,
            data.oneTimeCollision,
            data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            outline = m_outline;
            canCollide = m_canCollide;
            oneTimeCollision = m_oneTimeCollision;
            outline?.OnInspectorHasUpdate();

        }

    }

}