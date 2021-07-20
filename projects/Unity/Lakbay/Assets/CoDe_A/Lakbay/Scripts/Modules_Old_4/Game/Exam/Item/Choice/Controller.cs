/*
 * Date Created: Tuesday, July 13, 2021 9:41 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Exam.Item.Choice {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIController = Core.Interactable.IController<Data>;
    using BaseController = Core.Interactable.Controller<Data>;


    public interface IController : BaseIController {
        void OnCorrectChange(bool old, bool @new);
        void OnEntryChange(Core.Content.Entry.Data old, Core.Content.Entry.Data @new);
        
    }

    public class Controller : BaseController, IController {
        public new const string BoxGroupName = "Choice.Controller";

        public virtual void OnCorrectChange(bool old, bool @new) {
            

        }

        public virtual void OnEntryChange(Core.Content.Entry.Data old, Core.Content.Entry.Data @new) {

            
        }

    }

}