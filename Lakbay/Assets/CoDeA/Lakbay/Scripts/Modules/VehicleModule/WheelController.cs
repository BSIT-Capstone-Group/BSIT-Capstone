using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoDeA.Lakbay.Modules.VehicleModule {
    public class WheelController : MonoBehaviour {
        [HideInInspector]
        public new WheelCollider collider;

        public string steerAxis = "Wheel Controller - Horizontal";
        public string accelerateAxis = "Wheel Controller - Vertical";
        public string brakeAxis = "Wheel Controller - Jump";

        public bool canSteer = false;
        public bool canAccelerate = false;
        public bool canBrake = false;

        public float maxSteerAngle = 35.0f;
        public float maxMotorTorque = 15000.0f;
        public float maxBrakeTorque = 30000.0f;

        public VehicleController vehicleController;
        public GameObject model;
        public Vector3 modelRotation = Vector3.zero;

        private void Awake() {
            this.setUp();

        }

        private void FixedUpdate() {
            if(this.vehicleController) return;

            this.steer(this.getSteerAxisFactor());
            this.accelerate(this.getAccelerateAxisFactor());
            this.brake(this.getBrakeAxisFactor());

            this.updateModel();

        }

        public void setUp() {
            if(!this.collider) this.collider = this.transform.GetComponent<WheelCollider>();
            if(!this.collider) this.collider = this.gameObject.AddComponent<WheelCollider>();

            GameObject go = this.model;

            if(this.transform.childCount == 0 && this.model) {
                go = GameObject.Instantiate<GameObject>(this.model, this.transform);
                go.transform.localScale = Vector3.one;
                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.Euler(Vector3.zero + this.modelRotation);

            } else {
                go = this.transform.GetChild(0).gameObject;

            }

            this.model = go;

        }
        
        public void steer(float factor) {
            if(!this.canSteer) return;

            this.collider.steerAngle = this.maxSteerAngle * factor;

        }
        
        public void accelerate(float factor) {
            if(!this.canAccelerate) return;

            this.collider.motorTorque = this.maxMotorTorque * factor;

        }
        
        public void brake(float factor) {
            if(!this.canBrake) return;

            this.collider.brakeTorque = this.maxBrakeTorque * factor;

        }

        public float getSteerAxisFactor() {
            float factor = SimpleInput.GetAxis(this.steerAxis);
            factor = factor == 0.0f ? SimpleInput.GetAxis("Horizontal") : factor;

            return factor;

        }

        public float getAccelerateAxisFactor() {
            float factor = SimpleInput.GetAxis(this.accelerateAxis);
            factor = factor == 0.0f ? SimpleInput.GetAxis("Vertical") : factor;

            return factor;

        }

        public float getBrakeAxisFactor() {
            float factor = SimpleInput.GetAxis(this.brakeAxis);
            factor = factor == 0.0f ? SimpleInput.GetAxis("Jump") : factor;

            return factor;

        }

        public void updateModel() {
            if(this.model) {
                Quaternion q;
                Vector3 p;
                this.collider.GetWorldPose(out p, out q);

                this.model.transform.position = p;
                this.model.transform.rotation = q * Quaternion.Euler(this.modelRotation);
                this.model.transform.localScale = Vector3.one;
                
            }

        }

    }

}
