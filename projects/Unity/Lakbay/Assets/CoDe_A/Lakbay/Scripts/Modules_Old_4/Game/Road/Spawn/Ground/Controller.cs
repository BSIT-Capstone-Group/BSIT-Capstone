/*
 * Date Created: Monday, July 19, 2021 10:26 PM
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

namespace CoDe_A.Lakbay.ModulesOld4.Game.Road.Spawn.Ground {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using Base = Spawn;


    public interface IController : Base.IController<Data> {

        
    }

    public class Controller : Base.Controller<Data>, IController {
        public new const string BoxGroupName = "Ground.Controller";
        public override string[] keys => new string[] {"Ground"};


        public override GameObject OnSpawn(Vector2Int point) {
            GameObject inner = null, outer = null;
            if(gameObjects != null && gameObjects.GetCount() > 0) {
                outer = gameObjects[0];
                if(gameObjects.GetCount() > 1) inner = gameObjects[1];
                
                // if(point.x.Either(data.column.range.First(), data.column.range.Last())) {
                //     return outer;

                // } else return inner;

            }

            return base.OnSpawn(point);

        }

    }

}