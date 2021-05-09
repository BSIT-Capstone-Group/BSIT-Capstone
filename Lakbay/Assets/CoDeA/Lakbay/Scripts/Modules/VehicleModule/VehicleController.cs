using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;
using System.Linq;
using CoDeA.Lakbay.Modules.GameModule;

namespace CoDeA.Lakbay.Modules.VehicleModule {
    [System.Serializable]
    public class Vehicle {
		public bool hasInfiniteFuel = false;
        public float fuelDeduction = 0.04f;
        public float fuel = 100.0f;
        public float maxFuel = 100.0f;
        public float maxSpeed = 160.0f;
        
		[Tooltip("Maximum steering angle of the wheels")]
		public float maxSteerAngle = 30.0f;
		[Tooltip("Maximum torque applied to the driving wheels")]
		public float maxMotorTorque = 700.0f;
		[Tooltip("Maximum brake torque applied to the driving wheels")]
		public float maxBrakeTorque = 30000.0f;

		[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
		public float criticalSpeed = 5f;
		[Tooltip("Simulation sub-steps when the speed is above critical.")]
		public int stepsBelow = 5;
		[Tooltip("Simulation sub-steps when the speed is below critical.")]
		public int stepsAbove = 1;

    }

	public class VehicleController : MonoBehaviour {
        private Coroutine _unflipCoroutine = null;
        private bool _flipped = false;
        public bool flipped { get { return this._flipped; } }
		public bool sleeping {
			get {
				Rigidbody rb = this.GetComponent<Rigidbody>();
				return rb && rb.isKinematic;

			}

		}

		[HideInInspector]
		public Vector3 initialPosition = Vector3.zero;
		public Vector3 initialRotation = Vector3.zero;
		
		public TextAsset vehicleFile;
		public Vehicle vehicle;

		[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
		public GameObject wheelModel;

		public List<WheelController> wheelControllers = new List<WheelController>();


		public UnityEvent<VehicleController, float> onSteer = new UnityEvent<VehicleController, float>();
        public UnityEvent<VehicleController, float> onAccelerate = new UnityEvent<VehicleController, float>();
        public UnityEvent<VehicleController, float> onBrake = new UnityEvent<VehicleController, float>();
        public UnityEvent<VehicleController, float> onFuelChange = new UnityEvent<VehicleController, float>();

		// Find all the WheelColliders down in the hierarchy.
		private void Start() {
			this.initialPosition = this.transform.position;
			this.initialRotation = this.transform.rotation.eulerAngles;

			if(GameController.currentMode != null) this.setUpVehicle(
				GameController.currentMode.linearPlay.vehicle
			);
			else if(this.vehicleFile) this.setUpVehicle(this.vehicleFile);

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
			this.updateWheelModel();
			this.updateFuel();

        }

		public void setUpWheelControllers() {
            foreach(WheelController wc in wheelControllers) {
                if(this.wheelModel) wc.model = this.wheelModel;

                wc.maxSteerAngle = this.vehicle.maxSteerAngle;
                wc.maxMotorTorque = this.vehicle.maxMotorTorque;
                wc.maxBrakeTorque = this.vehicle.maxBrakeTorque;
                wc.vehicleController = this;
                wc.setUp();

            }

		}

		public void configureVehicleSubsteps(
			WheelController wc,
			float speedThreshold,
			int stepsBelowThreshold,
			int stepsAboveThreshold
		) {
			wc.collider.ConfigureVehicleSubsteps(
				speedThreshold, 
				stepsBelowThreshold, 
				stepsAboveThreshold
			);

		}

		public void configureVehicleSubsteps() {
			foreach(WheelController wc in this.wheelControllers) {
				this.configureVehicleSubsteps(
					wc,
					this.vehicle.criticalSpeed, 
					this.vehicle.stepsBelow, 
					this.vehicle.stepsAbove
				);

			}

		}

		public void steer(WheelController wc, float factor) {
			wc.steer(factor);

		}

		public void steer() {
			float[] factors = this.wheelControllers.Select<WheelController, float>(
				(wc) => wc.getSteerAngleFactor()
			).ToArray();
			float factor = factors.Average();

			foreach(WheelController wc in this.wheelControllers) {
				// float factor = wc.getSteerAngleFactor();

				this.steer(
					wc,
					factor
				);

			}

			if(factor != 0.0f) this.onSteer.Invoke(this, factor);

		}

		public void accelerate(WheelController wc, float factor) {
			wc.accelerate(factor);

		}

		public void accelerate() {
			float[] factors = this.wheelControllers.Select<WheelController, float>(
				(wc) => wc.getMotorTorqueFactor()
			).ToArray();
			float factor = factors.Average();

			if(!this.vehicle.hasInfiniteFuel && !this.sleeping) {
				if(factor != 0) {
					float ffactor = factor;

					if(ffactor < 0.0f) ffactor *= -1;

					if(this.vehicle.fuel != 0.0f) {
						this.vehicle.fuel = Mathf.Max(
							this.vehicle.fuel - (this.vehicle.fuelDeduction * ffactor),
							0.0f
						);

					} else {
						factor = 0.0f;

					}

				}

			}

			foreach(WheelController wc in this.wheelControllers) {
                // float factor = wc.getMotorTorqueFactor();

				this.accelerate(
					wc,
					factor
				);

			}

			if(factor != 0.0f) this.onAccelerate.Invoke(this, factor);

		}

		public void brake(WheelController wc, float factor) {
			wc.brake(factor);

		}

		public void brake() {
			float[] factors = this.wheelControllers.Select<WheelController, float>(
				(wc) => wc.getBrakeTorqueFactor()
			).ToArray();
			float factor = factors.Average();

			foreach(WheelController wc in this.wheelControllers) {
                // float factor = wc.getBrakeTorqueFactor();

				this.brake(
					wc,
					factor
				);

			}

			if(factor != 0.0f) this.onBrake.Invoke(this, factor);

		}

		public void updateWheelModel(WheelController wc) {
			wc.updateModel();

		}

		public void updateWheelModel() {
			foreach(WheelController wc in this.wheelControllers) {
				this.updateWheelModel(wc);

			}

		}

		public void updateFuel() {
			this.onFuelChange.Invoke(this, this.vehicle.fuel);

		}

		public void setFuel(float value) {
			this.vehicle.fuel = value;
			this.onFuelChange.Invoke(this, this.vehicle.fuel);

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
			Vector3 position = this.transform.position;
            position = position + (Vector3.up * 2.0f);
            this.respawn(position);

        }

		public void sleep() {
			Rigidbody rigidbody = this.GetComponent<Rigidbody>();
			this.GetComponent<Rigidbody>().isKinematic = true;

		}

		public void wakeUp() {
			Rigidbody rigidbody = this.GetComponent<Rigidbody>();
			this.GetComponent<Rigidbody>().isKinematic = false;

		}

		public void setUpVehicle(TextAsset vehicleFile) {
			this.setUpVehicle(Utilities.Helper.parseYAML<Vehicle>(vehicleFile.text));

		}

		public void setUpVehicle(Vehicle vehicle) {
			this.vehicle = vehicle;
			this.setUpWheelControllers();

		}

	}

}