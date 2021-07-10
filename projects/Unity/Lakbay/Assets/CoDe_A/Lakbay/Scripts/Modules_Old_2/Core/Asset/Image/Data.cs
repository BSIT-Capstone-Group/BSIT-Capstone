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
    using BaseIData = IData<Controller, Sprite, UnityEngine.UI.Image>;
    using BaseData = Data<Controller, Sprite, UnityEngine.UI.Image>;
    using Image_ = UnityEngine.UI.Image;

    public interface IData : IData<Sprite, Image_>
    {

    }

    public interface IData<T> : IData<T, Sprite, Image_>, IData
        where T : IController {

    }

    [Serializable]
    public class Data : BaseData, IData<Controller> {
        public new const string HeaderName = "Image.Data";

        [YamlIgnore]
        public override Sprite asset {
            get {
                try {
                    if(path != null && path.Length != 0) {
                        return default;

                    } else {
                        return _asset;

                    }

                } catch {}

                return default;

            }
            set {
                if(value == _asset) return;
                var o = _asset; var n = value;
                _asset = m_asset = value;
                onAssetChange.Invoke(controller, o, n);
                controller?.OnAssetChange(o, n);

            }

        }

        public Data() { Create(instance: this); }

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();

        }

        public override void Load(TextAsset textAsset) => Create(textAsset, this);

        public static Data Create(
            BaseIData data=null,
            Data instance=null
        ) {
            instance ??= new Data();
            BaseData.Create(data, instance);

            return instance;

        }

        public static Data Create(IData<Controller> data, Data instance=null) {
            data ??= new Data();
            return Create(
                data as BaseIData,
                instance
            );

        }

        public static Data Create(TextAsset textAsset, Data instance=null) {
            return Create(textAsset.Parse<Data>(), instance);

        }

    }

}