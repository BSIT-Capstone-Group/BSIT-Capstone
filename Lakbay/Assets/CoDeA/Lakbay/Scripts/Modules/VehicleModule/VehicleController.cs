using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
// using SimpleInput = UnityEngine.Input;

namespace CoDeA.Lakbay.Modules.VehicleModule {
    [System.Serializable]
    public class Vehicle {
        public float fuelPerDistance = 0.15f;
        public float fuel = 100.0f;
        public float maxFuel = 100.0f;
        public float maxSpeed = 200.0f;
        public float maxSteerAngle = 30.0f;
        public float maxMotorTorque = 1500.0f;
        public float maxBrakeTorque = 1500.0f;

    }

    public class VehicleController : Utilities.ExtendedMonoBehaviour {
        private bool _accelerating = false;
        private bool _steering = false;
        private bool _braking = false;

        public bool accelerating { get => this._accelerating; }
        public bool steering { get => this._steering; }
        public bool braking { get => this._braking; }

        [HideInInspector]
        public Vector3 initialPosition = Vector3.zero;
        [HideInInspector]
        public Vector3 lastPosition = Vector3.zero;
        [HideInInspector]
        public float distanceCovered = 0.0f;
        [HideInInspector]
        public bool canRecordDistanceCovered = true;

        [HideInInspector]
        public float fuelDistanceCovered = 0.0f;
        // private float _refilledFuel = 0.0f;
        [HideInInspector]
        public bool canRecordFuelDistanceCovered = true;

        [HideInInspector]
        public float fuelDistanceAccelerated = 0.0f;

        [HideInInspector]
        public float motorTorqueFactor = 0.0f;
        [HideInInspector]
        public float steerAngleFactor = 0.0f;
        [HideInInspector]
        public float brakeTorqueFactor = 0.0f;
        // [HideInInspector]
        // public float speed = 0.0f;

        public TextAsset vehicleFile;

        public bool canSteer = true;
        public bool canAccelerate = true;
        public bool canBrake = true;
        public bool infiniteFuel = false;
        public UIModule.UIController uiController;
        public List<WheelModule.WheelController> wheels = new List<WheelModule.WheelController>();
        public Vehicle vehicle;

        public UnityEvent<VehicleController, float> onAccelerate = new UnityEvent<VehicleController, float>();
        public UnityEvent<VehicleController, float> onBrake = new UnityEvent<VehicleController, float>();
        public UnityEvent<VehicleController, float> onSteer = new UnityEvent<VehicleController, float>();
        public UnityEvent<VehicleController, float> onFuelChange = new UnityEvent<VehicleController, float>();

        private void Start() {
            this.refillFuel(this.vehicle.fuel, this.vehicle.maxFuel);
            this.lastPosition = this.transform.position;
            this.initialPosition = this.GetComponent<Rigidbody>().position;

        }

        private void FixedUpdate() {
            this.recordDistanceCovered();
            float distance = this.recordFuelDistanceCovered();
            this.fuelDistanceAccelerated = distance;
            this.update();

            this.lastPosition = this.GetComponent<Rigidbody>().position;

        }

        public void setUpVehicle() {
            if(this.vehicleFile) {
                this.vehicle = Utilities.Helper.parseYAML<Vehicle>(this.vehicleFile.ToString());

            }

            this.vehicle = Game.modeData.vehicle;

            foreach(WheelModule.WheelController wc in this.wheels) {
                wc.properties.maxBrakeTorque = this.vehicle.maxBrakeTorque;
                wc.properties.maxMotorTorque = this.vehicle.maxMotorTorque;
                wc.properties.maxSteerAngle = this.vehicle.maxSteerAngle;

            }

        }

        public void steer(float steerAngleFactor) {
            if(!this.canSteer) return;

            this._steering = steerAngleFactor != 0 ? true : false;
            this.steerAngleFactor = steerAngleFactor;

            List<float> steerAngles = new List<float>();
            foreach(WheelModule.WheelController wheel in this.wheels) {
                float maxSteerAngle = wheel.properties.maxSteerAngle;
                float steerAngle = this.steerAngleFactor * maxSteerAngle * this.timeScale;

                wheel.steer(steerAngle);
                steerAngles.Add(steerAngle);

            }

            this.updateWheelModels();
            this._steering = false;

            if(steerAngleFactor != 0.0f) {
                float avg = Queryable.Average(steerAngles.AsQueryable());
                this.onSteer.Invoke(this, avg);

            }

        }

        public void accelerate(float motorTorqueFactor) {
            if(!this.canAccelerate) return;

            this._accelerating = motorTorqueFactor != 0 ? true : false;
            this.canRecordFuelDistanceCovered = true;

            if((!this.infiniteFuel && this.vehicle.fuel == 0.0f)) {
                motorTorqueFactor = 0.0f;
                this.canRecordDistanceCovered = false;

            }

            if(motorTorqueFactor == 0.0f) this.canRecordFuelDistanceCovered = false;

            this.motorTorqueFactor = motorTorqueFactor;

            List<float> motorTorques = new List<float>();
            foreach(WheelModule.WheelController wheel in this.wheels) {
                float maxMotorTorque = wheel.properties.maxMotorTorque;
                float motorTorque = this.motorTorqueFactor * maxMotorTorque * this.timeScale;

                wheel.accelerate(motorTorque);
                motorTorques.Add(motorTorque);

            }

            if(motorTorqueFactor != 0.0f) {
                if(!this.infiniteFuel) {
                    this.updateFuel();

                }

                this.capSpeed();

            }

            this.updateWheelModels();
            this.canRecordDistanceCovered = false;

            float speed = this.speed * 3.6f;
            float distance = this.fuelDistanceCovered / 1000.0f;

            this._accelerating = false;

            if(motorTorqueFactor != 0.0f) {
                float avg = Queryable.Average(motorTorques.AsQueryable());
                this.onAccelerate.Invoke(this, avg);
                
            }

        }

        public void brake(float brakeTorqueFactor) {
            if(!this.canBrake) return;

            this._braking = brakeTorqueFactor != 0 ? true : false;
            this.brakeTorqueFactor = brakeTorqueFactor;

            List<float> brakeTorques = new List<float>();
            foreach(WheelModule.WheelController wheel in this.wheels) {
                float maxBrakeTorque = wheel.properties.maxBrakeTorque;
                float brakeTorque = this.brakeTorqueFactor * maxBrakeTorque * this.timeScale;

                wheel.brake(brakeTorque);
                brakeTorques.Add(brakeTorque);

            }

            this.updateWheelModels();
            this._braking = false;

            if(brakeTorqueFactor != 0.0f) {
                float avg = Queryable.Average(brakeTorques.AsQueryable());
                this.onBrake.Invoke(this, avg);

            }

        }

        public void refillFuel(float value) {
            this.refillFuel(value, value);

        }

        public void refillFuel(float fuel, float maxFuel) {
            this.fuelDistanceCovered = 0.0f;
            this.setFuel(fuel);
            this.vehicle.maxFuel = maxFuel;

            this.updateFuel(0.0f);

        }

        public void capSpeed() {
            if((this.speed * 3.6f) >= this.vehicle.maxSpeed) {
                this.speed = this.vehicle.maxSpeed / 3.6f;

            }

        }

        public void setFuel(float value) {
            // if(value == this.fuel) return;

            this.vehicle.fuel = value;
            this.onFuelChange.Invoke(this, value);

        }

        public void updateFuel(float distance) {
            if(this.infiniteFuel) return;

            float consumedFuel = distance * this.vehicle.fuelPerDistance;
            float currentFuel = this.vehicle.fuel - consumedFuel;

            this.setFuel(Mathf.Max(currentFuel, 0.0f));

        }

        public void updateFuel() {
            this.updateFuel(this.fuelDistanceAccelerated);

        }

        public float recordDistanceCovered() {
            float distanceCovered = float.Parse(Vector3.Distance(this.GetComponent<Rigidbody>().position, this.lastPosition).ToString("F2"));

            if(this.canRecordDistanceCovered) {
                this.distanceCovered += distanceCovered;

            }

            return distanceCovered;
        }

        public float recordFuelDistanceCovered() {
            float distanceCovered = float.Parse(Vector3.Distance(this.GetComponent<Rigidbody>().position, this.lastPosition).ToString("F2"));

            if(this.canRecordFuelDistanceCovered) {
                this.fuelDistanceCovered += distanceCovered;

            }

            return distanceCovered;

        }
        
        public float getMotorTorqueFactor() {
            float accelerate = Mathf.Min(SimpleInput.GetAxis("AccelerateAndBrake"), 0.0f) * -1;
            float raccelerate = Mathf.Min(SimpleInput.GetAxis("ReverseAcceleration"), 0.0f);
            float value = accelerate + raccelerate;
            value = value == 0.0f ? SimpleInput.GetAxis("Vertical") : value;

            return value;

        }

        public float getSteerAngleFactor() {
            float value = SimpleInput.GetAxis("SteeringWheel");
            value = value == 0.0f ? SimpleInput.GetAxis("Horizontal") : value;

            return value;

        }

        public float getBrakeTorqueFactor() {
            float value = Mathf.Max(SimpleInput.GetAxis("AccelerateAndBrake"), 0.0f);
            value = value == 0.0f ? SimpleInput.GetAxis("Jump") : value;

            return value;

        }

        public void updateWheelModels() {
            foreach(WheelModule.WheelController wheel in this.wheels) {
                wheel.updateModel();

            }

        }

        public void update() {
            float motorTorqueFactor = this.getMotorTorqueFactor();
            float steerAngleFactor = this.getSteerAngleFactor();
            float brakeTorqueFactor = this.getBrakeTorqueFactor();

            this.accelerate(motorTorqueFactor);
            this.steer(steerAngleFactor);
            this.brake(brakeTorqueFactor);
            
        }

    }

}
