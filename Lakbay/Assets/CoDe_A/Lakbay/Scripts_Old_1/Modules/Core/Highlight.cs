/*
 * Date Created: Sunday, June 27, 2021 12:02 PM
 * Author: Nommel Isanar Lavapie Amolat (NI.L.A)
 * 
 * Copyright © 2021 CoDe_A. All Rights Reserved.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

using Ph.Com.CoDe_A.Lakbay.Utilities;
using Outline_ = Ph.Com.CoDe_A.Lakbay.Utilities.Outline;

namespace Ph.Com.CoDe_A.Lakbay.Modules.Core {
    public interface IHighlight {
        bool isUI { get; set; }
        bool showing { get; set; }
        Outline_ outline { get; set; }

    }

    public class Highlight : MonoBehaviour, IHighlight {
        [SerializeField]
        protected bool _isUI = false;
        public bool isUI {
            get => _isUI;
            set => _isUI = value;

        }
        public bool isMesh => !isUI;

        [SerializeField, Label("Showing")]
        private bool __showing = false;
        protected bool _showing = false;
        public bool showing {
            get => _showing;
            set {
                if(value == showing) return;
                _showing = value;
                __showing = value;

                if(isUI) gameObject.SetActive(showing);
                else outline.enabled = showing;
                
            }

        }

        [SerializeField, Label("Outline"), ShowIf("isMesh")]
        private Outline_ __outline;
        protected Outline_ _outline;        
        public Outline_ outline {
            get => _outline;
            set {
                if(value == outline) return;
                _outline = value;
                __outline = value;

            }

        }

        public virtual void OnValidate() {
            showing = __showing;
            outline = __outline;

        }

    }

}