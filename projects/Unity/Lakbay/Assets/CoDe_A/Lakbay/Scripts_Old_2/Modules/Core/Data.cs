/*
 * Date Created: Tuesday, June 29, 2021 6:49 PM
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

namespace Code_A_Old_2.Lakbay.Modules.Core {
    public interface IData {
        string label { get; set; }
        string description { get; set; }

        void OnInspectorHasUpdate();

    }

    [Serializable]
    public class Data : IData {
        [NonSerialized, YamlIgnore]
        public Controller controller;

        [SerializeField]
        protected string m_label;
        protected string _label;
        public virtual string label {
            get => _label;
            set {
                if(value == label) return;
                _label = m_label = value;

            }

        }
        [SerializeField, TextArea]
        protected string m_description;
        protected string _description;
        public virtual string description {
            get => _description;
            set {
                if(value == description) return;
                _description = m_description = value;

            }

        }


        public Data() : this("") {}

        public Data(
            string label="",
            string description=""
        ) : base() {
            this.label = label;
            this.description = description;

        }

        public Data(Data data) : this(
            data.label,
            data.description
        ) {}

        public Data(IController controller) : this(controller.data) {}

        public Data(TextAsset textAsset) : this(Helper.Parse<Data>(textAsset)) {}

        public virtual void OnInspectorHasUpdate() {
            label = m_label;
            description = m_description;

        }

    }

}