using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace CoDeA.Lakbay.Modules.VehicleModule {
	[Serializable]
	public enum DriveType
	{
		RearWheelDrive,
		FrontWheelDrive,
		AllWheelDrive
	}

	public class VehicleController : MonoBehaviour {
        private Coroutine _unflipCoroutine = null;
        private bool _flipped = false;
        public bool flipped { get { return this._flipped; } }

		[Tooltip("Maximum steering angle of the wheels")]
		public float maxSteerAngle = 35.0f;
		[Tooltip("Maximum torque applied to the driving wheels")]
		public float maxMotorTorque = 15000.0f;
		[Tooltip("Maximum brake torque applied to the driving wheels")]
		public float maxBrakeTorque = 30000.0f;
		[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
		public GameObject wheelModel;

		[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
		public float criticalSpeed = 5f;
		[Tooltip("Simulation sub-steps when the speed is above critical.")]
		public int stepsBelow = 5;
		[Tooltip("Simulation sub-steps when the speed is below critical.")]
		public int stepsAbove = 1;

		[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
		public DriveType driveType;

		public List<WheelController> wheelControllers = new List<WheelController>();

		// Find all the WheelColliders down in the hierarchy.
		private void Start() {
            foreach(WheelController wc in wheelControllers) {
                if(this.wheelModel) wc.model = this.wheelModel;

                wc.maxSteerAngle = this.maxSteerAngle;
                wc.maxMotorTorque = this.maxMotorTorque;
                wc.maxBrakeTorque = this.maxBrakeTorque;
                wc.vehicleController = this;
                wc.setUp();

            }

		}

        private void FixedUpdate() {
            foreach (WheelController wc in wheelControllers) wc.updateModel();
            this._flipped = Vector3.Dot(this.transform.up, Vector3.down) > 0;

            if(this.flipped && this._unflipCoroutine == null) {
                this._unflipCoroutine = this.StartCoroutine(this.unflip());

            }

        }

		// This is a really simple approach to updating wheels.
		// We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
		// This helps us to figure our which wheels are front ones and which are rear.
		private void Update() {
			// wheelControllers[0].collider.ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

			foreach (WheelController wc in wheelControllers) {
                wc.collider.ConfigureVehicleSubsteps(criticalSpeed, stepsBelow, stepsAbove);

                float steerAngleFactor = wc.getSteerAxisFactor();
                float motorTorqueFactor = wc.getAccelerateAxisFactor();
                float brakeTorqueFactor = wc.getBrakeAxisFactor();

				// A simple car where front wheels steer while rear ones drive.
				wc.steer(steerAngleFactor);
				wc.accelerate(motorTorqueFactor);
				wc.brake(brakeTorqueFactor);

                wc.updateModel();

			}
		}

        public IEnumerator unflip() {
            yield return new WaitForSeconds(2.0f);

            if(this.flipped) this.respawn();
            this.StopCoroutine(this._unflipCoroutine);
            this._unflipCoroutine = null;

        }

        public void respawn(Vector3 position, Vector3 rotation) {
            this.transform.position = position;
            this.transform.rotation = Quaternion.Euler(rotation);

        }

        public void respawn(Vector3 position) {
            this.respawn(position, Vector3.zero);

        }

        public void respawn() {
            Vector3 position = Vector3.zero + this.transform.forward + (Vector3.up * 2.0f);
            this.respawn(position);

        }

	}

}