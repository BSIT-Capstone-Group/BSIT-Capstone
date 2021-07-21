/*
 * Date Created: Wednesday, July 21, 2021 9:17 AM
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


    /// <summary>Represents objects that can be interacted within the game.</summary>>
    public interface IInteractable : IFocusable, IPlayable, ISaveable, ITutorial {
        
    }

}