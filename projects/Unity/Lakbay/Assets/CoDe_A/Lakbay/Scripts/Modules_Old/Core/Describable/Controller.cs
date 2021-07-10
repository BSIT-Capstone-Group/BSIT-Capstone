/*
 * Date Created: Friday, July 2, 2021 6:04 PM
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

using NaughtyAttributes;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.ModulesOld.Core.Describable {
    public interface IController : Core.Base.IController, ISerializable {
        new Data data { get; set; }

    }

    public class Controller : Core.Base.Controller, IController {
        public new const string BoxGroupName = "Describable.Controller";

        Data IController.data {
            get => new Data(this);
            set {
                value ??= new Data();
                (this as Core.Base.IController).data = value;

            }

        }
        [BoxGroup(BoxGroupName)]
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
        [BoxGroup(BoxGroupName)]
        [SerializeField]
        protected string m_description;
        protected string _description;
        public virtual string description {
            get => _description;
            set {
                if(value == description) return;
                _description = m_description = value;

            }

        }

        public Controller() : base() {
            var self = this as IController;
            self.data = null;

        }

        public override void OnInspectorHasUpdate() {
            var self = this as IController;
            base.OnInspectorHasUpdate();
            label = m_label;
            description = m_description;

        }

        public override void SetData(TextAsset textAsset) {
            var self = this as IController;
            self.data = new Data(textAsset);
            
        }

    }

}