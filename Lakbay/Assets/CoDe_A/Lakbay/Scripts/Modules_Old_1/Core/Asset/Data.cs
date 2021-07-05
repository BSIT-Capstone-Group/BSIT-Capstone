/*
 * Date Created: Saturday, July 3, 2021 4:57 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core.Asset {
    public interface IData<T> : Core.IData where T : UnityEngine.Object {
        string path { get; set; }

        T asset { get; set; }

    }

    [Serializable]
    public class Data<T> : Core.Data, IData<T> where T : UnityEngine.Object {
        protected string _path;
        public virtual string path { get => _path; set => _path = value; }

        [YamlIgnore]
        public virtual T asset {
            get {
                return default;

            }
            set {}

        }

        
        public Data() : this("") {}

        public Data(
            string path="",
            Core.IData data=null
        ) : base(data ?? new Core.Data()) {
            this.path = path;

        }

        public Data(IData<T> data) : this(
            data.path,
            data
        ) {}

        public Data(TextAsset textAsset) : this(textAsset.Parse<IData<T>>()) {}

    }

}