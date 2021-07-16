/*
 * Date Created: Monday, July 12, 2021 6:12 PM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Interactable {
    using Event = Utilities.Event;


    public interface IController : Input.IController, IPropertyEvent {

        
    }

    public class Controller : Input.Controller, IController {
        public new const string BoxGroupName = "Interactable.Controller";

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Information.Data _information;
        public virtual Information.Data information {
            get => _information;
            set => Helper.SetInvoke(this, ref _information, value, onInformationChange, OnInformationChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Asset.Image.Data _image;
        public virtual Asset.Image.Data image {
            get => _image;
            set => Helper.SetInvoke(this, ref _image, value, onImageChange, OnImageChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Highlight.Data _highlight;
        public virtual Highlight.Data highlight {
            get => _highlight;
            set => Helper.SetInvoke(this, ref _highlight, value, onHighlightChange, OnHighlightChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Content.Data _tutorialContent;
        public virtual Content.Data tutorialContent {
            get => _tutorialContent;
            set => Helper.SetInvoke(this, ref _tutorialContent, value, onTutorialContentChange, OnTutorialContentChange);

        }
        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Collider.Data _collider;
        public virtual new Collider.Data collider {
            get => _collider;
            set => Helper.SetInvoke(this, ref _collider, value, onColliderChange, OnColliderChange);

        }

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, Information.Data> _onInformationChange = new Event.OnValueChange<IEvent, Information.Data>();
        public virtual Event.OnValueChange<IEvent, Information.Data> onInformationChange => _onInformationChange;

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, Asset.Image.Data> _onImageChange = new Event.OnValueChange<IEvent, Asset.Image.Data>();
        public virtual Event.OnValueChange<IEvent, Asset.Image.Data> onImageChange => _onImageChange;

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, Highlight.Data> _onHighlightChange = new Event.OnValueChange<IEvent, Highlight.Data>();
        public virtual Event.OnValueChange<IEvent, Highlight.Data> onHighlightChange => _onHighlightChange;

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, Content.Data> _onTutorialContentChange = new Event.OnValueChange<IEvent, Content.Data>();
        public virtual Event.OnValueChange<IEvent, Content.Data> onTutorialContentChange => _onTutorialContentChange;

        [BoxGroup(BoxGroupName)]
        [SerializeField] protected Event.OnValueChange<IEvent, Collider.Data> _onColliderChange = new Event.OnValueChange<IEvent, Collider.Data>();
        public virtual Event.OnValueChange<IEvent, Collider.Data> onColliderChange => _onColliderChange;

        public virtual void OnColliderChange(Collider.Data old, Collider.Data @new) {


        }

        public virtual void OnInformationChange(Information.Data old, Information.Data @new) {


        }

        public virtual void OnHighlightChange(Highlight.Data old, Highlight.Data @new) {


        }

        public virtual void OnImageChange(Asset.Image.Data old, Asset.Image.Data @new) {


        }

        public virtual void OnTutorialContentChange(Content.Data old, Content.Data @new) {


        }
    }

}