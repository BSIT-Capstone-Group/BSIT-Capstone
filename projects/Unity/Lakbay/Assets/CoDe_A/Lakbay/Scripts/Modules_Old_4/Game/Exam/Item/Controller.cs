/*
 * Date Created: Tuesday, July 13, 2021 9:36 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Exam.Item {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using BaseIController = Core.Interactable.IController<Data>;
    using BaseController = Core.Interactable.Controller<Data>;


    public interface IController : BaseIController {
        void OnMissedChange(bool old, bool @new);
        void OnCorrectChange(bool old, bool @new);
        void OnQuestionChange(Core.Content.Data old, Core.Content.Data @new);
        void OnChoicesChange(List<Choice.Data> old, List<Choice.Data> @new);
        
    }

    public class Controller : BaseController, IController {
        public new const string BoxGroupName = "Item.Controller";

        public override void OnCollision(Collider collider) {
            base.OnCollision(collider);
            
        }

        public virtual void OnMissedChange(bool old, bool @new) {

            
        }

        public virtual void OnCorrectChange(bool old, bool @new) {


        }

        public virtual void OnQuestionChange(Core.Content.Data old, Core.Content.Data @new) {


        }

        public virtual void OnChoicesChange(List<Choice.Data> old, List<Choice.Data> @new) {
            

        }

        // public virtua

    }

}