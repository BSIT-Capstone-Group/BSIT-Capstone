/*
 * Date Created: Wednesday, July 7, 2021 7:32 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Asset {
    using Event = Utilities.Event;

    public interface IEvent<T0, T1> : Core.IEvent 
        where T0 : UnityEngine.Object
        where T1 : MonoBehaviour {
        Event.OnValueChange<IEvent<T0, T1>, T0> onAssetChange { get; }
        Event.OnValueChange<IEvent<T0, T1>, T1> onComponentChange { get; }
        Event.OnStringChange<IEvent<T0, T1>> onPathChange { get; }

        void OnPathChange(string old, string @new);

    }

    public interface IProperty<T0, T1> : Core.IProperty
        where T0 : UnityEngine.Object
        where T1 : MonoBehaviour {
        T0 asset { get; set; }
        T1 component { get; set; }
        string path { get; set; }

    }

    public interface IPropertyEvent<T0, T1> : IProperty<T0, T1>, IEvent<T0, T1>
        where T0 : UnityEngine.Object
        where T1 : MonoBehaviour {}

    public interface IData<T0, T1> : Core.IData, IProperty<T0, T1>
        where T0 : UnityEngine.Object
        where T1 : MonoBehaviour {

    }

    [Serializable]
    public class Data<T0, T1> : Core.Data, IData<T0, T1>
        where T0 : UnityEngine.Object
        where T1 : MonoBehaviour {
        [SerializeField] protected T0 _asset;
        public virtual T0 asset { get => _asset; set => _asset = value; }
        [SerializeField] protected T1 _component;
        public virtual T1 component { get => _component; set => _component = value; }
        [SerializeField] protected string _path;
        public virtual string path { get => _path; set => _path = value; }

    }

}