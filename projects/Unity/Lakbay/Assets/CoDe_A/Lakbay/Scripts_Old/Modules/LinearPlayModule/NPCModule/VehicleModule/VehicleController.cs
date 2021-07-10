using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDe_A_Old.Lakbay.Modules.LinearPlayModule.NPCModule.VehicleModule {
    public class VehicleController : Modules.VehicleModule.VehicleController {
        public GameObject particleHolder;
        public ParticleSystem despawnParticle;
        public bool isHit = false;
        public float coinReduction = 0.0f;

        public PlayerModule.PlayerController playerController;
        public GameObject startingLineModel;

        private void OnCollisionEnter(Collision collider) {
            if(collider.transform == this.playerController.transform) {
                if(this.playerController.player.lifeIntegrity != 0.0f) {
                    this.playerController.setLifeIntegrity(
                        this.playerController.player.lifeIntegrity - 1.0f
                    );

                }

                if(this.playerController.player.lifeIntegrity == 0.0f) {
                    this.playerController.respawn();
                    this.playerController.useLife();

                }

                this.hit();
                this.despawn();

            } else if(collider.transform == startingLineModel.transform) {
                this.playHitParticle();
                this.despawn();

            }
            
        }

        public void despawn() {
            Destroy(this.gameObject);

        }

        public void hit() {
            if(this.isHit) return;

            this.playHitParticle();
            this.sleep();
            this.isHit = true;

        }

        public void playHitParticle() {
            if(despawnParticle) {
                GameObject despawnObject = Instantiate<GameObject>(this.despawnParticle.gameObject, this.particleHolder.transform);
                despawnObject.transform.position = this.transform.position;
                Destroy(despawnObject, this.despawnParticle.main.duration);

            }

        }

    }

}
