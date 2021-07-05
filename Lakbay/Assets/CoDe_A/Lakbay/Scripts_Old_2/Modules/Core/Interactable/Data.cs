/*
 * Date Created: Wednesday, June 30, 2021 8:21 AM
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
using YamlDotNet.Serialization;
using UnityEngine.Events;

namespace Code_A_Old_2.Lakbay.Modules.Core.Interactable {
    public interface IData : Core.IData {
        Image.Data image { get; set; }
        bool canDetectInput { get; set; }
        bool highlighted { get; set; }
        Content.Data tutorialContent { get; set; }

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [NonSerialized, YamlIgnore]
        public new Controller controller;

        [SerializeField]
        protected Image.Data m_image;
        protected Image.Data _image;
        public virtual Image.Data image { get; set; }
        [SerializeField]
        protected bool m_canDetectInput;
        protected bool _canDetectInput;
        public virtual bool canDetectInput {
            get => _canDetectInput;
            set {
                if(value == canDetectInput) return;
                _canDetectInput = m_canDetectInput = value;

            }

        }
        [SerializeField]
        protected bool m_highlighted;
        protected bool _highlighted;
        public virtual bool highlighted {
            get => _highlighted;
            set {
                if(value == highlighted) return;
                _highlighted = m_highlighted = value;
                onHighlightedChange.Invoke(controller, value);

            }

        }
        [YamlIgnore]
        public UnityEvent<Controller, bool> onHighlightedChange = new UnityEvent<Controller, bool>();
        [SerializeField]
        protected Content.Data m_tutorialContent;
        protected Content.Data _tutorialContent;
        public virtual Content.Data tutorialContent {
            get => _tutorialContent;
            set {
                if(value == tutorialContent) return;
                _tutorialContent = m_tutorialContent = value;

            }

        }

        public Data() : this(default(Image.Data)) {}

        public Data(
            Image.Data image,
            bool canDetectInput=true,
            bool highlighted=false,
            Content.Data tutorialContent=null,
            Core.Data data=null
        ) : base(data ?? new Core.Data()) {
            this.image = image;
            this.canDetectInput = canDetectInput;
            this.highlighted = highlighted;
            this.tutorialContent = tutorialContent;

        }

        public Data(Data data) : this(
            data.image,
            data.canDetectInput,
            data.highlighted,
            data.tutorialContent,
            data
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            image = m_image;
            canDetectInput = m_highlighted;
            highlighted = m_highlighted;
            tutorialContent = m_tutorialContent;
            image?.OnInspectorHasUpdate();
            tutorialContent?.OnInspectorHasUpdate();

        }

    }

}