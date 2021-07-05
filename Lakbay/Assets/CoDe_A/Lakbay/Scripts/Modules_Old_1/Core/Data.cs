/*
 * Date Created: Saturday, July 3, 2021 3:15 PM
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

namespace CoDe_A.Lakbay.ModulesOld1.Core {
    public interface IData {


    }

    [Serializable]
    public class Data : IData {
        public Data() {}

        public Data(IData data) {}

        public Data(TextAsset textAsset) {}

    }

}