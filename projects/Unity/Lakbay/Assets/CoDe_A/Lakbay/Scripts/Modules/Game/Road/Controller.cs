/*
 * Date Created: Tuesday, July 20, 2021 7:05 PM
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

namespace CoDe_A.Lakbay.Modules.Game.Road {
    using Event = Utilities.Event;
    using Input = Utilities.Input;


    public interface IController {
        TextAsset dataTextAsset { get; set; }
        Data data { get; set; }
        
    }

    public class Controller : MonoBehaviour, IController {
        [ContextMenuItem("Use", "UseDataTextAsset")]
        [SerializeField]
        protected TextAsset _dataTextAsset;
        public TextAsset dataTextAsset { get => _dataTextAsset; set => _dataTextAsset = value; }
        [SerializeField]
        protected Data _data;
        public Data data { get => _data; set => _data = value; }


        public virtual void UseDataTextAsset() {
            try {
                data = dataTextAsset.Parse<Data>();
                print("Successfully Parsed!");

            } catch(Exception e) {
                print("Failed to Parse!");
                print(e);

            }

        }

    }

}