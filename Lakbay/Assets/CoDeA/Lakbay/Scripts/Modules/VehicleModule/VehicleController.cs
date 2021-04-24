using System.Collections;
using System.Collections.Generic;
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

        public bool canSteer = true;
        public bool canAccelerate = true;
        public bool canBrake = true;
        public bool infiniteFuel = false;
        public float fuel = 100.0f;
        public float maxFuel = 100.0f;
        public float fuelPerDistance = 0.15f;
        [Tooltip("Maximum Speed in km/hr")]
        public float maxSpeed = 200.0f;
        public UIModule.UIController uiController;
        public List<WheelModule.WheelController> wheels = new List<WheelModule.WheelController>();

        private void Start() {
            this.refillFuel(this.fuel, this.maxFuel);
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

        public void steer(float steerAngleFactor) {
            if(!this.canSteer) return;

            this._steering = steerAngleFactor != 0 ? true : false;
            this.steerAngleFactor = steerAngleFactor;

            foreach(WheelModule.WheelController wheel in this.wheels) {
                float maxSteerAngle = wheel.properties.maxSteerAngle;

                wheel.steer(this.steerAngleFactor * maxSteerAngle * this.timeScale);

            }

            this.updateWheelModels();
            this._steering = false;

        }

        public void accelerate(float motorTorqueFactor) {
            if(!this.canAccelerate) return;

            this._accelerating = motorTorqueFactor != 0 ? true : false;
            this.canRecordFuelDistanceCovered = true;

            if((!this.infiniteFuel && this.fuel == 0.0f)) {
                motorTorqueFactor = 0.0f;
                this.canRecordDistanceCovered = false;

            }

            if(motorTorqueFactor == 0.0f) this.canRecordFuelDistanceCovered = false;

            this.motorTorqueFactor = motorTorqueFactor;

            foreach(WheelModule.WheelController wheel in this.wheels) {
                float maxMotorTorque = wheel.properties.maxMotorTorque;

                wheel.accelerate(this.motorTorqueFactor * maxMotorTorque * this.timeScale);

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

        }

        public void brake(float brakeTorqueFactor) {
            if(!this.canBrake) return;

            this._braking = brakeTorqueFactor != 0 ? true : false;
            this.brakeTorqueFactor = brakeTorqueFactor;

            foreach(WheelModule.WheelController wheel in this.wheels) {
                float maxBrakeTorque = wheel.properties.maxBrakeTorque;

                wheel.brake(this.brakeTorqueFactor * maxBrakeTorque * this.timeScale);

            }

            this.updateWheelModels();
            this._braking = false;

        }

        public void refillFuel(float value) {
            this.refillFuel(value, value);

        }

        public void refillFuel(float fuel, float maxFuel) {
            this.fuelDistanceCovered = 0.0f;
            this.fuel = fuel;
            this.maxFuel = maxFuel;

            this.updateFuel(0.0f);

        }

        public void capSpeed() {
            if((this.speed * 3.6f) >= this.maxSpeed) {
                this.speed = this.maxSpeed / 3.6f;

            }

        }

        public void updateFuel(float distance) {
            if(this.infiniteFuel) return;

            float consumedFuel = distance * this.fuelPerDistance;
            float currentFuel = this.fuel - consumedFuel;

            this.fuel = Mathf.Max(currentFuel, 0.0f);

        }

        public void updateFuel() {
            this.updateFuel(this.fuelDistanceAccelerated);

        }

        public void updateSlider() {
            Slider slider = this.uiController.fuelBar;
            slider.value = Mathf.SmoothStep(slider.value, this.fuel / this.maxFuel, 0.5f);

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

            this.updateSlider();
            
        }

    }

}
