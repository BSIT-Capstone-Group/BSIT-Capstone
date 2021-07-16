/*
 * Date Created: Wednesday, July 7, 2021 8:24 AM
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

namespace CoDe_A.Lakbay.ModulesOld3.Core.Interactable {
    using Event = Utilities.Event;

    public interface IEvent : Core.IEvent {
        Event.OnValueChange<IEvent, Information.Data> onInformationChange { get; }
        Event.OnValueChange<IEvent, Asset.Image.Data> onImageChange { get; }
        Event.OnValueChange<IEvent, Highlight.Data> onHighlightChange { get; }
        Event.OnValueChange<IEvent, Content.Data> onTutorialContentChange { get; }
        Event.OnValueChange<IEvent, Collider.Data> onColliderChange { get; }

        void OnInformationChange(Information.Data old, Information.Data @new);
        void OnImageChange(Asset.Image.Data old, Asset.Image.Data @new);
        void OnHighlightChange(Highlight.Data old, Highlight.Data @new);
        void OnTutorialContentChange(Content.Data old, Content.Data @new);
        void OnColliderChange(Collider.Data old, Collider.Data @new);

    }

    public interface IProperty : Core.IProperty {
        Information.Data information { get; set; }
        Asset.Image.Data image { get; set; }
        Highlight.Data highlight { get; set; }
        Content.Data tutorialContent { get; set; }
        Collider.Data collider { get; set; }

    }

    public interface IPropertyEvent : IProperty, IEvent {}

    public interface IData : Core.IData, IProperty {

    }

    [Serializable]
    public class Data : Core.Data, IData {
        [SerializeField] protected Information.Data _information;
        public virtual Information.Data information { get => _information; set => _information = value; }
        [SerializeField] protected Asset.Image.Data _image;
        public virtual Asset.Image.Data image { get => _image; set => _image = value; }
        [SerializeField] protected Highlight.Data _highlight;
        public virtual Highlight.Data highlight { get => _highlight; set => _highlight = value; }
        [SerializeField] protected Content.Data _tutorialContent;
        public virtual Content.Data tutorialContent { get => _tutorialContent; set => _tutorialContent = value; }
        [SerializeField] protected Collider.Data _collider;
        public virtual Collider.Data collider { get => _collider; set => _collider = value; }

    }

}