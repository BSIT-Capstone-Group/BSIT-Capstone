/*
 * Date Created: Monday, July 5, 2021 7:22 PM
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

namespace CoDe_A.Lakbay.ModulesOld2.Core.Asset.Image {
    using Event = Utilities.Event;
    using Image_ = UnityEngine.UI.Image;

    public interface IController : Asset.IController<Sprite, Image_> {

    }
    
    public interface IController<T> : Asset.IController<T, Sprite, Image_>, IController
        where T : IData {
            
    }

    // [AddComponentMenu("Core/Asset/Image Controller")]
    public class Controller : Asset.Controller<Data, Sprite, Image_>, IController<Data> {
        
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

        public override void OnComponentChange(Image_ old, Image_ @new) {
            base.OnComponentChange(old, @new);
            if(@new) @new.sprite = data?.asset;

        }

        public override void OnAssetChange(Sprite old, Sprite @new) {
            base.OnAssetChange(old, @new);
            if(data != null && data.component) data.component.sprite = data.asset;

        }

        public override void OnPathChange(string old, string @new) {
            base.OnPathChange(old, @new);
            if(data != null && data.component) data.component.sprite = data.asset;

        }

    }

}