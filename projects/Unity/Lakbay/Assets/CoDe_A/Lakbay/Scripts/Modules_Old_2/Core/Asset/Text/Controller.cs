/*
 * Date Created: Monday, July 5, 2021 4:42 PM
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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Asset.Text {
    using Event = Utilities.Event;
    using Text_ = TextMeshProUGUI;

    public interface IController : Asset.IController<TextAsset, Text_> {
        void OnValueChange(string old, string @new);

    }
    
    public interface IController<T> : Asset.IController<T, TextAsset, Text_>, IController
        where T : IData {
            
    }

    // [AddComponentMenu("Core/Asset/Text Controller")]
    public class Controller : Asset.Controller<Data, TextAsset, Text_>, IController<Data> {
        
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

        public override void OnComponentChange(Text_ old, Text_ @new) {
            base.OnComponentChange(old, @new);
            @new?.SetText(data.asset ? data.asset.text : "");

        }

        public override void OnAssetChange(TextAsset old, TextAsset @new) {
            base.OnAssetChange(old, @new);
            data.component?.SetText(data.asset ? data.asset.text : "");

        }

        public override void OnPathChange(string old, string @new) {
            base.OnPathChange(old, @new);
            data.component?.SetText(data.asset ? data.asset.text : "");

        }

        public virtual void OnValueChange(string old, string @new) {
            data.component?.SetText(data.asset ? data.asset.text : "");

        }

        public override void Update() {
            base.Update();

        }

    }

}