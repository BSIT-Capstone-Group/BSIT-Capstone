/*
 * Date Created: Monday, July 5, 2021 4:43 PM
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

namespace CoDe_A.Lakbay.Modules.Core.Asset.Text {
    using Event = Utilities.Event;

    public interface IData : Asset.IData {

    }

    public interface IData<T> : Asset.IData<T>, IData
        where T : IController {

    }

    [Serializable]
    public class Data : Asset.Data<Controller>, IData<Controller> {
        public new const string HeaderName = "Text.Data";

        public Data() : this(default(Asset.IData<Controller>)) {}

        public Data(
            Asset.IData<Controller> data=null
        ) : base(data ?? new Asset.Data<Controller>()) {

        }

        public Data(IData<Controller> data) : this(
            
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<Data>()) {}

        public override void OnInspectorHasUpdate() {
            base.OnInspectorHasUpdate();

        }

    }

}