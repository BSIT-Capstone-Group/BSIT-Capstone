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
using CoDe_A.Lakbay.Modules.Core;

namespace CoDe_A.Lakbay.Modules.Game.LinearPlay.Controllers.Spawners {
    using Event = Utilities.Event;
    using Input = Utilities.Input;
    using RowList = List<List2D<List2D<string>>>;
    using Spawns;


    public interface IRoadController : ISpawnerController {
        int length { get; set; }
        int safeZoneLength { get; set; }
        int examItemCount { get; set; }
        float obstacleChance { get; set; }
        float powerUpChance { get; set; }
        Vector3[] columnPositions { get; }
        
    }

    public class RoadController : SpawnerController, IRoadController {
        [SerializeField]
        protected int _length = 200;
        public virtual int length { get => _length; set => _length = value; }
        [SerializeField]
        protected int _safeZoneLength = 5;
        public virtual int safeZoneLength { get => _safeZoneLength; set => _safeZoneLength = value; }
        [SerializeField]
        protected int _examItemCount = 20;
        public virtual int examItemCount { get => _examItemCount; set => _examItemCount = value; }
        [SerializeField]
        protected float _obstacleChance = 0.75f;
        public virtual float obstacleChance { get => _obstacleChance; set => _obstacleChance = value; }
        [SerializeField]
        protected float _powerUpChance = 0.12f;
        public virtual float powerUpChance { get => _powerUpChance; set => _powerUpChance = value; }
        public virtual Vector3[] columnPositions {
            get {
                return columns.Select(
                    (c) => (from i in Enumerable.Range(0, 3)
                        select c.Average((g) => g.transform.position[i])).AsVector3()
                ).ToArray();
            }
        }

        public virtual AxisInfo playableColumn => new AxisInfo(
            startIndex: 1, endIndex: 3
        ); 

        public virtual AxisInfo playableRow => new AxisInfo(
            startIndex: safeZoneLength, endIndex: length + safeZoneLength
        ); 

        public virtual ExamItemController examItemController {
            get => spawnControllers.Find((sc) => sc.GetType() == typeof(ExamItemController))
                as ExamItemController;

        }

        public virtual GroundController groundController {
            get => spawnControllers.Find((sc) => sc.GetType() == typeof(GroundController))
                as GroundController;

        }

        public virtual ObstacleController obstacleController {
            get => spawnControllers.Find((sc) => sc.GetType() == typeof(ObstacleController))
                as ObstacleController;

        }

        public virtual PowerUpController powerUpController {
            get => spawnControllers.Find((sc) => sc.GetType() == typeof(PowerUpController))
                as PowerUpController;

        }

        public virtual TerrainController terrainController {
            get => spawnControllers.Find((sc) => sc.GetType() == typeof(TerrainController))
                as TerrainController;

        }

        
        public override void Update() {
            base.Update();

            examItemController.column = playableColumn;
            examItemController.row = new AxisInfo(
                maxCount: 1,
                indexInterval: length / examItemCount,
                startIndex: playableRow.startIndex,
                endIndex: playableRow.endIndex
            );

            groundController.column = playableColumn;

            obstacleController.chance = obstacleChance;
            obstacleController.column = playableColumn;
            obstacleController.row = new AxisInfo(
                maxCount: 2,
                indexInterval: 3,
                startIndex: playableRow.startIndex,
                endIndex: playableRow.endIndex
            );

            powerUpController.chance = powerUpChance;
            powerUpController.column = playableColumn;
            powerUpController.row = new AxisInfo(
                maxCount: 2,
                indexInterval: 8,
                startIndex: playableRow.startIndex,
                endIndex: playableRow.endIndex
            );

            terrainController.column = new AxisInfo(
                indexRange: Helper.AsList(0, 1)
            );

        }

    }

}