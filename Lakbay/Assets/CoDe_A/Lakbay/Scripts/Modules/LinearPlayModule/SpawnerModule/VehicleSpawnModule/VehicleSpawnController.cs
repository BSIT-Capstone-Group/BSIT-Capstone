using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.SpawnerModule.VehicleSpawnModule {
    public class VehicleSpawnController : SpawnController {
        public VehicleController vehicleController;

        public new void move(Vector3 position) {
            // Vector3 pos = this.GetComponent<Rigidbody>().position;
            // pos.x = this.vehicleController.initialPosition.x;

            // this.transform.position = pos;
            // this.transform.rotation = Quaternion.Euler(Vector3.zero);

        }

    }

}
