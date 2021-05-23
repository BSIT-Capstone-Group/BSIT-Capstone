using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CoDe_A.Lakbay.Modules.LinearPlayModule.SpawnerModule.VehicleSpawnModule {
    public class VehicleController : VehicleModule.VehicleController {
        private void Start() {
			this.initialPosition = this.transform.position;
			this.initialRotation = this.transform.rotation.eulerAngles;

			this.setUpVehicle(this.vehicle);

		}

        private void FixedUpdate() {
            foreach (WheelController wc in wheelControllers) wc.updateModel();
            this._flipped = Vector3.Dot(this.transform.up, Vector3.down) > 0;

            if(this.flipped && this._unflipCoroutine == null) {
                this._unflipCoroutine = this.StartCoroutine(this.unflip());

            }

			this.configureVehicleSubsteps();
			this.steer();
			this.accelerate();
			this.brake();
			this.decelerate();
			this.updateWheelModel();
			this.updateFuel();

        }

        public new void accelerate() {
            if(this.speed > this.vehicle.maxSpeed) return;
            
            foreach(WheelController wc in this.wheelControllers) {
				this.accelerate(
					wc,
					this.speed > this.vehicle.maxSpeed ? 0.0f : wc.getMotorTorqueFactor()
				);

			}

			// if(factor != 0.0f) this.onAccelerate.Invoke(this, factor);

        }

        public new void steer() {}
        public new void brake() {}

    }

    

}
