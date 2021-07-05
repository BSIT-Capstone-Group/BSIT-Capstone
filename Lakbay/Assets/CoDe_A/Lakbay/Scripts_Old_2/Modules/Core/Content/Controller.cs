/*
 * Date Created: Tuesday, June 29, 2021 7:41 PM
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
using IC = Code_A_Old_2.Lakbay.Modules.Core.Content.IController;

namespace Code_A_Old_2.Lakbay.Modules.Core.Content {
    public interface IController : Core.IController {
        bool showContentData { get; }
        new Data data { get; set; }

    }

    public class Controller : Core.Controller, IController {
        public new const string BoxGroupName = "Content.Controller";

        public override bool showCoreData => false;
        public virtual bool showContentData => false;
        [BoxGroup(BoxGroupName)]
        [SerializeField, ShowIf("showContentData")]
        protected Data m_contentData;
        protected Data _contentData;
        Data IController.data {
            get => _contentData;
            set {
                value ??= new Data();
                if(value == As<IC>().data) return;
                As<Core.IController>().data = value;
                if(As<IC>().data != null) As<IC>().data.controller = null;
                value.controller = this;
                _contentData = m_contentData = value;

            }

        }


        public Controller() : base() {
            As<IC>().data = null;

        }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();
            As<IC>().data = m_contentData;
            As<IC>().data?.OnInspectorHasUpdate();

        }

        public override void SetData(TextAsset textAsset) => As<IC>().data = new Data(textAsset);

    }

}