using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.SpawnerModule.VehicleSpawnModule {
    public class VehicleSpawnController : SpawnController {
        public VehicleController vehicleController;

        public float coinReduction = 5.0f;

        public override void move(Vector3 position) {
            // Vector3 pos = this.GetComponent<Rigidbody>().position;
            // pos.x = this.vehicleController.initialPosition.x;

            // this.transform.position = pos;
            // this.transform.rotation = Quaternion.Euler(Vector3.zero);

        }

        public override void getHit(Vector3 position) {
            if(this.playerController.player.coin != 0.0f) {
                this.playerController.setCoin(
                    Mathf.Max(this.playerController.player.coin - this.coinReduction, 0.0f)
                );

            }

            if(this.playerController.player.lifeIntegrity != 0.0f) {
                this.playerController.setLifeIntegrity(
                    Mathf.Max(this.playerController.player.lifeIntegrity - 1.0f, 0.0f)
                );

            }
            
            if(this.playerController.player.lifeIntegrity == 0.0f) {
                this.spawnerController.stopSpawning();
                this.playerController.respawn();
                this.playerController.useLife();
                this.playerController.setLifeIntegrity(3.0f);
                this.spawnerController.startSpawning();

            }

            this.onHit.Invoke(this, position);

        }

    }

}
