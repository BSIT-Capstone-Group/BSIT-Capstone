using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.NPCModule.VehicleModule {
    public class WheelController : Modules.VehicleModule.WheelController {
        public new float getSteerAngleFactor() => 0.0f;
        public new float getBrakeTorqueFactor() => 0.0f;
        public new float getMotorTorqueFactor() => 1.0f;

    }

}
