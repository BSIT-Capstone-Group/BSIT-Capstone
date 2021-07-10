/*
 * Date Created: Wednesday, July 7, 2021 12:42 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Move {
    using Event = Utilities.Event;

    public interface IEvent : Core.Transition.IEvent<Transform> {

    }

    public interface IProperty : Core.Transition.IProperty<Transform> {

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.Transition.IData<Transform>, IProperty {

    }

    [Serializable]
    public class Data : Core.Transition.Data<Transform>, IData {

    }

}