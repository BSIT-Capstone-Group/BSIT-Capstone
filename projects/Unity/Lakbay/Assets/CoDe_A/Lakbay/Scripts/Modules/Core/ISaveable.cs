/*
 * Date Created: Tuesday, July 20, 2021 7:55 PM
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
using UnityEngine.InputSystem;

using NaughtyAttributes;
using TMPro;
using YamlDotNet.Serialization;

using CoDe_A.Lakbay.Utilities;

namespace CoDe_A.Lakbay.Modules.Core {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    /// <summary>Provides Serialization and Deserialization Functionalities.</summary>
    public interface ISaveable {
        TextAsset OnSave();
        void OnLoad(TextAsset textAsset);
        
    }

    /// <summary>Provides Serialization and Deserialization Functionalities.</summary>
    public interface ISaveable<T> : ISaveable {
        void OnLoad(T data);

    }

}