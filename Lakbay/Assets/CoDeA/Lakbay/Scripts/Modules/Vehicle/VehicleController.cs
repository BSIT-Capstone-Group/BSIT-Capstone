using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.Vehicle {
    public class VehicleController : Utilities.ExtendedMonoBehaviour {
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

        public bool infiniteFuel = false;
        public float fuel = 100.0f;
        public float maxFuel = 100.0f;
        public float fuelPerDistance = 0.15f;
        [Tooltip("Maximum Speed in km/hr")]
        public float maxSpeed = 200.0f;
        public UI.UIController uiController;
        public List<Wheel.WheelController> wheels = new List<Wheel.WheelController>();

        private void Start() {
            this.refillFuel(this.fuel, this.maxFuel);
            this.lastPosition = this.transform.position;
            // this.initialPosition = new Vector3(0.0f, 1.3f, -10.0f);
            this.initialPosition = this.transform.position;

        }

        private void FixedUpdate() {
            this.recordDistanceCovered();
            float distance = this.recordFuelDistanceCovered();
            this.fuelDistanceAccelerated = distance;
            this.update();
                
            // this._lastPosition = this.transform.position;
            this.lastPosition = this.GetComponent<Rigidbody>().position;

        }

        public void steer(float steerAngleFactor) {
            this.steerAngleFactor = steerAngleFactor;

            foreach(Wheel.WheelController wheel in this.wheels) {
                float maxSteerAngle = wheel.properties.maxSteerAngle;

                wheel.steer(this.steerAngleFactor * maxSteerAngle * this.timeScale);

            }

            this.updateWheelModels();

        }

        public void accelerate(float motorTorqueFactor) {
            this.canRecordFuelDistanceCovered = true;

            if((!this.infiniteFuel && this.fuel == 0.0f)) {
                motorTorqueFactor = 0.0f;
                this.canRecordDistanceCovered = false;

            }

            if(motorTorqueFactor == 0.0f) this.canRecordFuelDistanceCovered = false;

            this.motorTorqueFactor = motorTorqueFactor;

            foreach(Wheel.WheelController wheel in this.wheels) {
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
            // print($"Speed: {speed}, {distance}");

        }

        public void brake(float brakeTorqueFactor) {
            this.brakeTorqueFactor = brakeTorqueFactor;

            foreach(Wheel.WheelController wheel in this.wheels) {
                float maxBrakeTorque = wheel.properties.maxBrakeTorque;

                wheel.brake(this.brakeTorqueFactor * maxBrakeTorque * this.timeScale);

            }

            this.updateWheelModels();

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
            // slider.maxValue = this.maxFuel;
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

            // print($"{accelerate}, {raccelerate}, {value}");

            return value;

        }

        public float getSteerAngleFactor() {
            return SimpleInput.GetAxis("SteeringWheel");

        }

        public float getBrakeTorqueFactor() {
            float value = Mathf.Max(SimpleInput.GetAxis("AccelerateAndBrake"), 0.0f);

            return value;
            // return Input.GetAxis("Jump");

        }

        public void updateWheelModels() {
            foreach(Wheel.WheelController wheel in this.wheels) {
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
