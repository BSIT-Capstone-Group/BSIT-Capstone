/*
 * Date Created: Saturday, July 3, 2021 2:05 PM
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

namespace CoDe_A.Lakbay.ModulesOld.Core.Isolated {
    public interface IData {
        IData Create();

    }

    public interface ISerializable<T> where T : IData {
        void SetData(TextAsset textAsset);
        void SetData(T data);
        T GetData();

    }

    public interface IAssetData : IData {
        string path { get; set; }

    }

    public interface ITextData : IAssetData {
        string value { get; set; }

    }

    public interface IEntryData : IData {
        ITextData text { get; set; }
        List<IImageData> images { get; set; }

    }

    public interface IImageData : IAssetData, IDescribableData {

    }

    public interface IInteractableData : IData {
        bool highlighted { get; set; }
        IContent tutorialContent { get; set; }
        
    }

    public interface IDescribableData : IData {
        string label { get; set; }
        string description { get; set; }

    }

    public interface IContent : IData {
        List<IEntryData> entries { get; set; }

    }

    public class Data : IInteractableData
    {
        public bool highlighted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IContent tutorialContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IData Create()
        {
            throw new NotImplementedException();
        }
    }

    public class Controller : IInteractableData, ISerializable<IInteractableData>
    {
        public bool highlighted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IContent tutorialContent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IData Create()
        {
            throw new NotImplementedException();
        }

        public IInteractableData GetData()
        {
            throw new NotImplementedException();
        }

        public void SetData(IInteractableData data)
        {
            throw new NotImplementedException();
        }

        public void SetData(TextAsset textAsset)
        {
            throw new NotImplementedException();
        }
    }

}