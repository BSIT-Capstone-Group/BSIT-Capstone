using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Modules.LinearPlayModule.SpawnerModule.VehicleSpawnModule {
    public class WheelController : Modules.VehicleModule.WheelController {
        public override float getSteerAngleFactor() => 0.0f;
        public override float getBrakeTorqueFactor() => 0.0f;
        public override float getMotorTorqueFactor() => this.canAccelerate ? 1.0f : 0.0f;

    }

}