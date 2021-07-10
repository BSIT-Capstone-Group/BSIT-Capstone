/*
 * Date Created: Monday, June 28, 2021 2:01 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using NaughtyAttributes;

using Ph.Com.CoDe_A.Lakbay.Utilities;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Game.LinearPlay.Tutorial {
    public enum Type {
        Vehicle,
        Lane,
        Obstacle,
        PowerUp

    }

    public interface IController : Input.IController {


    }

    public class Controller : Input.Controller, IController {
        public static readonly Dictionary<System.Type, Type> TypeMap = new Dictionary<System.Type, Type>() {
            {typeof(Player.Controller), Type.Vehicle},
            {typeof(Lane.Group.Controller), Type.Lane}

        };

        public List<Type> finishedTypes = new List<Type>();

        public Player.Controller playerController;

        public override void OnCollide(Collider other) {
            base.OnCollide(other);
            if(other.TryGetComponent<Core.Controller>(out Core.Controller c)) OnCollideController(c);

        }

        public virtual void OnCollideController(Core.Controller controller) {
            // print(controller.controllerName);
            if(
                TypeMap.TryGetValue(controller.GetType(), out Type type) &&
                !finishedTypes.Contains(type)
                
            ) {
                controller.Focus();
                float time = 7.0f;

                switch(type) {
                    case(Type.Vehicle): {
                        
                        break;

                    } case(Type.Lane): {

                        break;

                    } case(Type.Obstacle): {

                        break;

                    } case(Type.PowerUp): {

                        break;

                    }
                    default: break;
                }
            
                Helper.DoOver(this, time, controller.Unfocus);
                finishedTypes.Add(type);

            }

        }

    }

}